using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Studentregistrering;

namespace Studentregistrering.DataAccess.Data
{
    public class StudentDbContext : DbContext
    {

        public DbSet<Student> Students { get; set; } = null!;
        public DbSet<Course> Courses { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            
            optionsBuilder.UseSqlServer(new ConfigurationBuilder()
                .AddJsonFile("appSettings.json")
                .Build()
                .GetSection("ConnectionStrings")["LocalDb"]);
        }
    }
}
