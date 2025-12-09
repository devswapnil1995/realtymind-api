using System;
using System.Collections.Generic;
using System.Text;

namespace RealtyMind.Application.DTOs.Finance
{
    public class AmortizationEntry
    {
        public int PaymentNumber { get; set; }
        public DateTime PaymentDate { get; set; }
        public decimal BeginningBalance { get; set; }
        public decimal ScheduledPayment { get; set; }
        public decimal PrincipalPaid { get; set; }
        public decimal InterestPaid { get; set; }
        public decimal ExtraPayment { get; set; }
        public decimal EndingBalance { get; set; }
    }
}
