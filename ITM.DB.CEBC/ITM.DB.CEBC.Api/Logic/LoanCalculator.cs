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

        public LoanModel CalcLoan(LoanInputModel postModel)
        {
            return CalcLoan(postModel.LoanAmount, postModel.LoanDurationYears, postModel.LoanDurationMonths);
        }

        public LoanModel CalcLoan(decimal amount, int durationYears, int durationMonths)
        {
            LoanModel m = new LoanModel()
            {
                Amount = amount,
                DurationTotalMonths = CalcTotalDuration(durationYears, durationMonths),
                TotalFee = CalcFee(amount),
            };
            m.TotalInterest = CalcInterest(m);
            m.MonthlyCost = CalcMonthlyCost(m);
            m.APR = CalcAPR(m);

            return m;
        }

        private int CalcTotalDuration(int durationYears, int durationMonths)
        {
            return (durationYears * 12) + durationMonths;
        }

        public decimal CalcFee(decimal amount)
        {
            decimal feeAmount = config.AdministrationFeeAmount;
            decimal feePercent = config.AdministrationFeePercent / 100m;
            decimal feePercentValue = amount * feePercent;
            return Math.Min(feeAmount, feePercentValue);
        }

        public decimal CalcInterest(LoanModel loan)
        {
            decimal yearlyInterestRate = config.AnnualInterestRate / 100m;
            decimal monthlyInterestRate = yearlyInterestRate / 12m;
            decimal outstandingBalance = loan.Amount;
            decimal totalInterest = 0m;
            while (outstandingBalance > 0m)
            {
                decimal monthlyInterestValue = outstandingBalance * monthlyInterestRate;
                decimal monthlyPrincipalValue = (loan.Amount / loan.DurationTotalMonths) - monthlyInterestValue;
                outstandingBalance -= monthlyPrincipalValue;
                totalInterest += monthlyInterestValue;
            }
            return totalInterest;
        }
        public decimal CalcMonthlyCost(LoanModel loan)
        {
            return (loan.Amount + loan.TotalFee + loan.TotalInterest) / loan.DurationTotalMonths;
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

        public decimal CalcAPR(APRParams input)
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
