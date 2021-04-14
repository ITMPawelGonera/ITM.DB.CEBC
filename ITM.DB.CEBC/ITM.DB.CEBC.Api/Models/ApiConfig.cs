using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ITM.DB.CEBC.Api.Models
{
    public class ApiConfig
    {
        private decimal annualInterestRate = Consts.APPCFG_AnnualInterestRate_DEFAULT;
        private string interestCalculatedInSpan = Consts.APPCFG_InterestCalculatedInSpan_DEFAULT;
        private decimal administrationFeePercent = Consts.APPCFG_AdministrationFeePercent_DEFAULT;
        private decimal administrationFeeAmount = Consts.APPCFG_AdministrationFeeAmount_DEFAULT;

        public decimal AnnualInterestRate
        {
            get => annualInterestRate; 
            set => annualInterestRate = value;
        }

        public string InterestCalculatedInSpan 
        { 
            get => interestCalculatedInSpan; 
            set => interestCalculatedInSpan = value; 
        }

        public decimal AdministrationFeePercent 
        { 
            get => administrationFeePercent; 
            set => administrationFeePercent = value; 
        }

        public decimal AdministrationFeeAmount 
        { 
            get => administrationFeeAmount; 
            set => administrationFeeAmount = value; 
        }
    }
}
