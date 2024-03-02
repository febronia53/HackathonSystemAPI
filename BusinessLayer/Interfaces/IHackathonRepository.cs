using BusinessLayer.Shared;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Interfaces
{
    public interface IHackathonRepository
    {
        Task<IEnumerable<Hackathon>> GetAllHackathons();
        Task<Hackathon> GetHackathonById(int id);
        Task<BaseResult> AddHackathon(Hackathon hackathon);

        Task<BaseResult> UpdateHackathon(Hackathon hackathonToUpdate);

        Task<BaseResult> DeleteHackathon(int id);
        Task<BaseResult> RegisterInHackathon(TeamRegisteration teamRegistration);

    }
}
