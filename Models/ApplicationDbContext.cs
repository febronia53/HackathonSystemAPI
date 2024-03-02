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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configure relationships for TeamRegisteration entity
            modelBuilder.Entity<TeamRegisteration>()
                .HasOne(tr => tr.ChallengeTitle)
                .WithMany()
                .HasForeignKey(tr => tr.ChallengeTitleId);

            modelBuilder.Entity<TeamRegisteration>()
                .HasOne(tr => tr.Hackathon)
                .WithMany(h => h.TeamRegisterations)
                .HasForeignKey(tr => tr.HackathonID);

            // Configure relationships for TeamMember entity
            modelBuilder.Entity<TeamMember>()
                .HasOne(tm => tm.TeamRegisteration)
                .WithMany(tr => tr.TeamMembers)
                .HasForeignKey(tm => tm.TeamRegisterationId);

            // Configure relationships for Hackathon entity
            modelBuilder.Entity<Hackathon>()
                .HasMany(h => h.ChallengeTitles)
                .WithMany()
                .UsingEntity(j => j.ToTable("HackathonChallengeTitles"));

            base.OnModelCreating(modelBuilder);
        }

    }
}
