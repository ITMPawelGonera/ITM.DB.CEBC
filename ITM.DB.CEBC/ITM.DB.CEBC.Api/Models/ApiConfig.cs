using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ITM.DB.CEBC.Api.Models
{
    public class ApiConfig
    {
        private decimal annualInterestRate = 5.00m;
        private string interestCalculatedInSpan = "Monthly";
        private decimal administrationFeePercent = 1.00m;
        private decimal administrationFeeAmount = 10000m;

        public decimal AnnualInterestRate { get => annualInterestRate; set => annualInterestRate = value; }
        public string InterestCalculatedInSpan { get => interestCalculatedInSpan; set => interestCalculatedInSpan = value; }

        public decimal AdministrationFeePercent { get => administrationFeePercent; set => administrationFeePercent = value; }
        public decimal AdministrationFeeAmount { get => administrationFeeAmount; set => administrationFeeAmount = value; }

    }
}
