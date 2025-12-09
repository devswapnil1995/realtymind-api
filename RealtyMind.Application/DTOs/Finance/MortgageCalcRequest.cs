using System;
using System.Collections.Generic;
using System.Text;

namespace RealtyMind.Application.DTOs.Finance
{
    public class MortgageCalcRequest
    {
        public decimal Principal { get; set; }        // loan amount
        public decimal AnnualRatePercent { get; set; } // APR in percent (if 0, service may fetch a default)
        public int TermMonths { get; set; }           // total months (e.g., 240 for 20 years)
        public int PaymentsPerYear { get; set; } = 12; // monthly=12
        public decimal ExtraMonthlyPayment { get; set; } = 0m;
    }
}
