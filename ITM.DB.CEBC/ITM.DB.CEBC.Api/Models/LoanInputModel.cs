using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ITM.DB.CEBC.Api.Models
{
    public class LoanInputModel
    {
        public decimal LoanAmount { get; set; }
        public int LoanDurationYears { get; set; }
        public int LoanDurationMonths { get; set; }
    }
}
