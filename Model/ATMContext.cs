using System.Data.Entity;

namespace ATMMonitoringSystem.Models
{
    public class ATMContext : DbContext
    {
        public ATMContext() : base("ATMContext") { }

        public DbSet<ATM> ATMs { get; set; }
        public DbSet<MaintenanceLog> MaintenanceLogs { get; set; }
    }
}
