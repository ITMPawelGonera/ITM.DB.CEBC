using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ITM.DB.CEBC.Api.Models
{
    public class LoanModel
    {
        public decimal Amount { get; set; }
        public int DurationTotalMonths { get; set; }
        public decimal MonthlyCost { get; set; }
        public decimal TotalInterest { get; set; }
        public decimal TotalFee { get; set; }
        public decimal APR { get; set; }
    }
}
