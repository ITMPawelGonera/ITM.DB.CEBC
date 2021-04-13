using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ITM.DB.CEBC.Api.Models
{
    public class LoanOutputModel
    {
        public decimal MonthlyCost { get; set; }
        public decimal TotalInterest { get; set; }
        public decimal TotalFees { get; set; }
        public decimal APR { get; set; }
    }
}
