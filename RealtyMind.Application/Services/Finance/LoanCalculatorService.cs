using RealtyMind.Application.DTOs.Finance;
using System;
using System.Collections.Generic;
using System.Text;

namespace RealtyMind.Application.Services.Finance
{

    public class LoanCalculatorService
    {
        // Compute monthly EMI (or per period based on PaymentsPerYear)
        public MortgageCalcResponse CalculateMortgage(MortgageCalcRequest req, DateTime? startDate = null)
        {
            if (req.TermMonths <= 0) throw new ArgumentException("TermMonths must be > 0");
            if (req.Principal <= 0) throw new ArgumentException("Principal must be > 0");
            if (req.PaymentsPerYear <= 0) req.PaymentsPerYear = 12;

            var monthlyRateDecimal = (req.AnnualRatePercent / 100m) / req.PaymentsPerYear;
            var n = req.TermMonths; // total payments
                                    // Standard annuity formula: P * r / (1 - (1+r)^-n)
            decimal payment = 0m;

            if (monthlyRateDecimal == 0)
            {
                payment = req.Principal / n;
            }
            else
            {
                var r = monthlyRateDecimal;
                var numerator = req.Principal * r;
                var denom = 1 - (decimal)Math.Pow(1 + (double)r, -n);
                payment = numerator / denom;
            }

            var schedule = new List<AmortizationEntry>();
            var balance = req.Principal;
            var paymentDate = startDate ?? DateTime.UtcNow.Date;
            decimal totalInterest = 0m;
            decimal totalPayments = 0m;

            for (int i = 1; i <= n && balance > 0; i++)
            {
                var interest = balance * monthlyRateDecimal;
                var principalPaid = payment - interest;
                var extra = req.ExtraMonthlyPayment;
                if (principalPaid + extra > balance)
                {
                    principalPaid = balance;
                    extra = 0m;
                }

                var scheduledPayment = principalPaid + interest;
                var ending = balance - principalPaid - extra;
                if (ending < 0) ending = 0;

                schedule.Add(new AmortizationEntry
                {
                    PaymentNumber = i,
                    PaymentDate = paymentDate.AddMonths(i - 1),
                    BeginningBalance = Math.Round(balance, 2),
                    ScheduledPayment = Math.Round(scheduledPayment + extra, 2),
                    PrincipalPaid = Math.Round(principalPaid + extra, 2),
                    InterestPaid = Math.Round(interest, 2),
                    ExtraPayment = Math.Round(extra, 2),
                    EndingBalance = Math.Round(ending, 2)
                });

                totalInterest += interest;
                totalPayments += (scheduledPayment + extra);
                balance = ending;
            }

            return new MortgageCalcResponse
            {
                MonthlyPayment = Math.Round(payment, 2),
                TotalPayments = Math.Round(totalPayments, 2),
                TotalInterest = Math.Round(totalInterest, 2),
                TermMonths = n,
                AmortizationSchedule = schedule
            };
        }
    }
}
