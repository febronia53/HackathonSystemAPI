using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class TeamRegisteration
    {
        public int Id { get; set; }
        public string TeamName { get; set; }
        public int ChallengeTitleId { get; set; } // Reference to ChallengeTitle
        public virtual ChallengeTitle ChallengeTitle { get; set; }
        public List<TeamMember> TeamMembers { get; set; }
        public int HackathonID { get; set; }
        public Hackathon Hackathon { get; set; }

    }
}
