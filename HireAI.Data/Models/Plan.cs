using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HireAI.Data.Models
{
    public class Plan
    {
        // Key (int identity)
        public int Id { get; set; }

        public string Title { get; set; } = string.Empty;

        // Keep monthly price (minimal)
        public decimal MonthlyPrice { get; set; }

        // Computed annual price (no extra input)
        public decimal AnnualPrice => Math.Round(MonthlyPrice * 12m * 0.8m, 2);
    }
}
