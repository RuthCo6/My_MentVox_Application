using MentVox.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MentVox.Core.Models.ConversationModels;

namespace MentVox.Data
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Conversation> Conversations { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source = DESKTOP-P6FFS03; Initial Catalog = MentVox.Application; Integrated Security = true; Trusted_Connection = SSPI; MultipleActiveResultSets = true; TrustServerCertificate = true;",
                options =>
                options.MigrationsAssembly("MentVox.Application"));
        }
    }
}

