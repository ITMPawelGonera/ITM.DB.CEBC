using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ITM.DB.CEBC.Api
{
    public class Consts
    {
        public const decimal APPCFG_AnnualInterestRate_DEFAULT = 5.00m;
        public const string APPCFG_InterestCalculatedInSpan_DEFAULT = APPCFG_InterestCalculatedInSpan_MONTHLY;
        public const decimal APPCFG_AdministrationFeePercent_DEFAULT = 1.00m;
        public const decimal APPCFG_AdministrationFeeAmount_DEFAULT = 10000m;

        public const string APPCFG_InterestCalculatedInSpan_YEARLY = "Yearly";
        public const string APPCFG_InterestCalculatedInSpan_MONTHLY = "Monthly";
    }
}
