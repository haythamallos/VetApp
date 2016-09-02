using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace RESTAPI.Models
{

    public class Apikey : DbContext
    {
        public long ApikeyId { get; set; }
        public string DateCreated { get; }
        public string DateModified { get; }
        public string DateExpiration { get; }
        public bool IsDisabled { get; set; }
        public string ApiauthToken { get; set; }
        public string Notes { get; set; }

        public Apikey(DbContextOptions<Apikey> options)
            : base(options)
        { }
    }

    public class ApikeyContext : DbContext
    {
        public DbSet<Apikey> Apikeys { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            AppSettings appSettings = new AppSettings();
            
            optionsBuilder.UseSqlServer(appSettings.DefaultConnection);
        }
    }
}
