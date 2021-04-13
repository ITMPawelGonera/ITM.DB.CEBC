using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ITM.DB.CEBC.Api.Models
{
    public class LoanModel
    {
        public decimal Amount { get; set; }
        public int DurationYears { get; set; }
        public int DurationMonths { get; set; }

        public int DurationTotalMonths { 
            get
            {
                return DurationYears * 12 + DurationMonths;
            }
            set
            {
                DurationMonths = value % 12;
                DurationYears = Convert.ToInt32(Math.Floor(value / 12m));
            }
        }

        public decimal MonthlyCost { 
            get
            {
                return (Amount + TotalFee + TotalInterest) / DurationTotalMonths;
            }
        }

        public decimal TotalInterest { get; set; }
        public decimal TotalFee { get; set; }
        public decimal APR { get; set; }
    }
}
