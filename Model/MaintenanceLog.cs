using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ATMMonitoringSystem.Models
{
    public class MaintenanceLog
    {
        [Key] // ✅ This is required
        public int LogId { get; set; }

        [ForeignKey("ATM")] // Optional but good
        public int ATMId { get; set; }

        public string TechnicianName { get; set; }
        public string Description { get; set; }
        public DateTime MaintenanceDate { get; set; }

        public virtual ATM ATM { get; set; }
    }
}
