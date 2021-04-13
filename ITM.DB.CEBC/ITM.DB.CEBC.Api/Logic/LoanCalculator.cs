using ITM.DB.CEBC.Api.Models;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ITM.DB.CEBC.Api.Logic
{
    public class LoanCalculator
    {
        ApiConfig config;
        public LoanCalculator(IOptions<ApiConfig> options)
        {
            config = options.Value;
        }
        public LoanModel CalcLoan(decimal amount, int totalDurationInMonths)
        {
            LoanModel m = new LoanModel() {
                Amount = amount,
                DurationTotalMonths = totalDurationInMonths,
                TotalFee = CalcFee(amount),
            };
            m.TotalInterest = CalcInterest(m);
            m.APR = CalcAPR(m);
            
            return m;
        }

        public decimal CalcAPR(LoanModel loan)
        {
            return CalcAPR(new APRParams()
            {
                TotalFee = loan.TotalFee,
                TotalInterest = loan.TotalInterest,
                DurationInMonths = loan.DurationTotalMonths,
                Amount = loan.Amount
            });
        }

        public LoanModel CalcLoan(LoanInputModel postModel)
        {
            return CalcLoan(postModel.LoanAmount, postModel.LoanDurationYears * 12 + postModel.LoanDurationMonths);
        }

        public decimal CalcFee(decimal amount)
        {
            decimal feeAmount = config.AdministrationFeeAmount;
            decimal feePercent = config.AdministrationFeePercent / 100m;
            decimal feePercentValue = amount * feePercent;
            return Math.Min(feeAmount, feePercentValue);
        }

        internal decimal CalcInterest(LoanModel loan)
        { 
            decimal yearlyInterestRate = config.AnnualInterestRate / 100m;
            decimal monthlyInterestRate = yearlyInterestRate / 12m;
            decimal outstandingBalance = loan.Amount;
            decimal totalInterest = 0m;
            while (outstandingBalance > 0m)
            {
                decimal monthlyInterestValue = outstandingBalance * monthlyInterestRate;
                decimal monthlyPrincipalValue = (loan.Amount/ loan.DurationTotalMonths) - monthlyInterestValue;
                outstandingBalance -= monthlyPrincipalValue;
                totalInterest += monthlyInterestValue;
            }
            return totalInterest;
        }

        internal decimal CalcAPR(APRParams input)
        {
            decimal yearlyCost = (input.TotalFee + input.TotalInterest) / (input.DurationInMonths / 12m);
            return yearlyCost / input.Amount;
        }
    }

    public class APRParams
    {
        public decimal TotalFee { get; set; }
        public decimal TotalInterest { get; set; }
        public int DurationInMonths { get; internal set; }
        public decimal Amount { get; internal set; }
    }
}
