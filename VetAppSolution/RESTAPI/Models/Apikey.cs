using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RESTAPI.Models
{
    [Table("Apikey")]
    public class Apikey
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ApikeyId { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public DateTime? ExpirationDate { get; set; }
        public bool? IsDisabled { get; set; }
        public string Token { get; set; }
        public string Notes { get; set; }

    }

    public class ApikeyContext : DbContext
    {
        private string ConnectionString { get; set; }
        public ApikeyContext(string pStrConnectionString)
        {
            ConnectionString = pStrConnectionString;
        }
        public virtual DbSet<Apikey> Apikeys { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(ConnectionString);
        }

    }
}
