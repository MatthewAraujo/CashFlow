using CashFlow.Domain.Enums;

namespace CashFlow.Domain.Entities
{
    public class Expense
    {
        public long Id { get; set; }
        public string Title { get; set; }  = string.Empty;

        public string? Description { get; set; }

        public DateTime DateTime { get; set; }

        public decimal Amout { get; set; }

        public PaymentType PaymentType { get; set; }    
    }
}
