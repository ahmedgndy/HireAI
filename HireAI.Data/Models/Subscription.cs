using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HireAI.Data.Models
{
    public class Subscription
    {
        public int Id { get; set; }

        // Navigation only. The FK column will be a shadow property named "PlanId" in the DbContext mapping.
        public Plan? Plan { get; set; }

        // Snapshot of the plan title for convenience
        public string PlanTitle { get; set; } = string.Empty;

        // "Monthly" or "Yearly"
        public string BillingCycle { get; set; } = "Monthly";

        // Minimal status
        public string Status { get; set; } = "Active";
    }
}
