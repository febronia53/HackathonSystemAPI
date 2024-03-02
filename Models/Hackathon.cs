using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class Hackathon
    {
        public int HackathonID { get; set; }
        public string Name { get; set; }
        public string Theme { get; set; }
        public DateTime RegistrationStartDate { get; set; }
        public DateTime RegistrationEndDate { get; set; }
        public DateTime EventDate { get; set; }
        public int MaxTeamSize { get; set; }
        public int MaxTeams { get; set; }

        public virtual ICollection<ChallengeTitle>? ChallengeTitles { get; set; }

        //navigationProperty
        public virtual ICollection<TeamRegisteration>? TeamRegisterations { get; set; } = new List<TeamRegisteration>();


    }
}
