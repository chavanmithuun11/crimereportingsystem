using CrimeReportingSystem.Models;
using Microsoft.EntityFrameworkCore;

namespace CrimeReportingSystem.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<CrimeReport> CrimeReports { get; set; }
    }
}
