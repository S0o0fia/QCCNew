using System.Data.Entity;

namespace AspnetMvcDemo.Models
{
    public class QCC_DB : DbContext
    {
        public DbSet<Factory> UserAuthentication { get; set; }
    }
}