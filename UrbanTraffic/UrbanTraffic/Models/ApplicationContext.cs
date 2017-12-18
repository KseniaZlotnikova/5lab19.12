using UrbanTraffic;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using  UrbanTraffic.Models;

namespace UrbanTraffic.Models
{
    public class ApplicationContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationContext()
        { }


        public ApplicationContext(DbContextOptions<ApplicationContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Employees> Employees { get; set; }
        public virtual DbSet<Routes> Routes { get; set; }
        public virtual DbSet<Schedule> Schedule { get; set; }
        public virtual DbSet<Stopping> Stopping { get; set; }
        public virtual DbSet<TransportService> TransportService { get; set; }
        public virtual DbSet<TypeOfTransport> TypeOfTransport { get; set; }
        public DbSet<UrbanTraffic.Models.ApplicationUser> ApplicationUser { get; set; }
    }
}
