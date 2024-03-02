using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class ApplicationDbContext:IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
         : base(options)
        {
        }
        public DbSet<TeamMember> TeamMembers { get; set; }
        public DbSet<TeamRegisteration> TeamRegisterations { get; set; }
        public DbSet<Hackathon>Hackathons { get; set; }
        public DbSet<ChallengeTitle> ChallengeTitles { get; set; }

    }
}
