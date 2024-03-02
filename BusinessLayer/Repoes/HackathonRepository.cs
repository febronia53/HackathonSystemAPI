using BusinessLayer.Interfaces;
using BusinessLayer.Shared;
using Microsoft.EntityFrameworkCore;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Repoes
{
    public class HackathonRepository : IHackathonRepository
    {
        private readonly ApplicationDbContext _context;

        public HackathonRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<BaseResult> AddHackathon(Hackathon hackathon)
        {
           _context.Hackathons.Add(hackathon);
            var saved=await _context.SaveChangesAsync();
            if(saved>0)
            {
                return new BaseResult { IsSuccess = true, Message="The Item has been Added" };
            }
            else
            {
                return new BaseResult { IsSuccess = false, Message = "Something went wrong !" };
            }

        }

        public async Task<IEnumerable<Hackathon>> GetAllHackathons()
        {
            return await _context.Hackathons
                .Include(h=>h.ChallengeTitles)
               .Include(h => h.TeamRegisterations)
               .ToListAsync();
        }

        public async Task<Hackathon> GetHackathonById(int id)
        {
            var hackathon = _context.Hackathons
                .Include(h=>h.ChallengeTitles)
                .Include(h=>h.TeamRegisterations)              
                .FirstOrDefault(h => h.HackathonID == id);
            return hackathon;
           
        }
        public async Task<BaseResult> UpdateHackathon(Hackathon updatedHackathon)
        {
            var existingHackathon = await _context.Hackathons
                .Include(h => h.ChallengeTitles) // Include any related entities if needed
                .Include(h => h.TeamRegisterations)
                .FirstOrDefaultAsync(h => h.HackathonID == updatedHackathon.HackathonID);

            if (existingHackathon == null)
            {
                return new BaseResult { IsSuccess = false, Message = "Hackathon not found" };
            }

            // Update the properties of the existing hackathon
            existingHackathon.Name = updatedHackathon.Name;
            existingHackathon.Theme = updatedHackathon.Theme;
            existingHackathon.RegistrationStartDate = updatedHackathon.RegistrationStartDate;
            existingHackathon.RegistrationEndDate = updatedHackathon.RegistrationEndDate;
            existingHackathon.EventDate = updatedHackathon.EventDate;
            existingHackathon.MaxTeamSize = updatedHackathon.MaxTeamSize;
            existingHackathon.MaxTeams = updatedHackathon.MaxTeams;
            // Update other properties as needed

            // If ChallengeTitles is not null, update the list (you may need to handle additions and removals)
            if (updatedHackathon.ChallengeTitles != null)
            {
                existingHackathon.ChallengeTitles = updatedHackathon.ChallengeTitles;
            }

            // If TeamRegisterations is not null, update the list (you may need to handle additions and removals)
            if (updatedHackathon.TeamRegisterations != null)
            {
                existingHackathon.TeamRegisterations = updatedHackathon.TeamRegisterations;
            }

            var saved = await _context.SaveChangesAsync();
            if (saved > 0)
            {
                return new BaseResult { IsSuccess = true, Message = "Hackathon updated successfully" };
            }
            else
            {
                return new BaseResult { IsSuccess = false, Message = "Something went wrong while updating hackathon" };
            }
        }
        public async Task<BaseResult> DeleteHackathon(int id)
        {
            try
            {
                // Find the hackathon by its ID
                var hackathon = await _context.Hackathons
                    .Include(h => h.ChallengeTitles) // Include related challenge titles
                    .FirstOrDefaultAsync(h => h.HackathonID == id);

                if (hackathon == null)
                {
                    return new BaseResult { IsSuccess = false, Message = "Hackathon not found" };
                }

                // Remove any associated challenge titles
                if (hackathon.ChallengeTitles != null && hackathon.ChallengeTitles.Any())
                {
                    _context.ChallengeTitles.RemoveRange(hackathon.ChallengeTitles);
                }

                // Remove the hackathon itself
                _context.Hackathons.Remove(hackathon);

                // Save changes to the database
                var saved = await _context.SaveChangesAsync();

                // Check if deletion was successful
                if (saved > 0)
                {
                    return new BaseResult { IsSuccess = true, Message = "Hackathon deleted successfully" };
                }
                else
                {
                    return new BaseResult { IsSuccess = false, Message = "Something went wrong while deleting hackathon" };
                }
            }
            catch (Exception ex)
            {
                return new BaseResult { IsSuccess = false, Message = $"An error occurred while deleting hackathon: {ex.Message}" };
            }
        }
        public async Task<BaseResult> RegisterInHackathon(TeamRegisteration teamRegistration)
        {
            try
            {
                // Assuming teamRegistration object contains necessary information, including TeamMembers

                // Validate if the required fields are present
                if (string.IsNullOrEmpty(teamRegistration.TeamName) || teamRegistration.TeamMembers == null || !teamRegistration.TeamMembers.Any())
                {
                    return new BaseResult { IsSuccess = false, Message = "Invalid team registration data" };
                }

                // Check if each team member is already registered in another hackathon
                foreach (var teamMember in teamRegistration.TeamMembers)
                {
                    bool isMemberRegisteredInAnotherHackathon = await IsTeamMemberRegisteredInAnyHackathon(teamMember.TeamMemberID);
                    if (isMemberRegisteredInAnotherHackathon)
                    {
                        return new BaseResult { IsSuccess = false, Message = $"Team member '{teamMember.Name}' is already registered in another hackathon" };
                    }

                    bool isMemberAlreadyInTeam = await IsTeamMemberAlreadyInTeam(teamMember.TeamMemberID, teamRegistration.HackathonID);
                    if (isMemberAlreadyInTeam)
                    {
                        return new BaseResult { IsSuccess = false, Message = $"Team member '{teamMember.Name}' is already a member of another team in the same hackathon" };
                    }
                }

                // You can add more validation logic as needed

                // Add the team registration to the context
                _context.TeamRegisterations.Add(teamRegistration);

                // Save changes to the database
                var saved = await _context.SaveChangesAsync();

                // Check if the registration was successful
                if (saved > 0)
                {
                    return new BaseResult { IsSuccess = true, Message = "Team registered successfully in the hackathon" };
                }
                else
                {
                    return new BaseResult { IsSuccess = false, Message = "Something went wrong while registering the team in the hackathon" };
                }
            }
            catch (Exception ex)
            {
                return new BaseResult { IsSuccess = false, Message = $"An error occurred while registering the team in the hackathon: {ex.Message}" };
            }
        }

        private async Task<bool> IsTeamMemberRegisteredInAnyHackathon(int teamMemberId)
        {
            // Check if the team member is registered in any hackathon
            return await _context.TeamRegisterations
                .AnyAsync(tr => tr.TeamMembers.Any(tm => tm.TeamMemberID == teamMemberId));
        }

        private async Task<bool> IsTeamMemberAlreadyInTeam(int teamMemberId, int hackathonId)
        {
            // Check if the team member is already a member of another team in the same hackathon
            return await _context.TeamRegisterations
                .AnyAsync(tr => tr.HackathonID == hackathonId && tr.TeamMembers.Any(tm => tm.TeamMemberID == teamMemberId));
        }


    }
}
