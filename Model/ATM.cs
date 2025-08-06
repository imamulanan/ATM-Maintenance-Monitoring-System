using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ATMMonitoringSystem.Models
{
    public class ATM
    {
        [Key]
        public int ATMId { get; set; }

        public string Location { get; set; }
        public string Status { get; set; }
        public DateTime LastMaintenanceDate { get; set; }
        public string Notes { get; set; }

        public virtual ICollection<MaintenanceLog> MaintenanceLogs { get; set; }
    }
}
