using System;
using System.Collections.Generic;
using System.Text;

namespace RealtyMind.Application.DTOs.Finance
{
    public class MortgageCalcResponse
    {
        public decimal MonthlyPayment { get; set; }
        public decimal TotalPayments { get; set; }
        public decimal TotalInterest { get; set; }
        public int TermMonths { get; set; }
        public List<AmortizationEntry> AmortizationSchedule { get; set; } = new();
    }
}
