using System;

namespace backend.Models.Api
{
    public class TransactionDto
    {
        public Guid TransactionId { get; set; }

        public Guid AccountId { get; set; }

        public decimal Amount { get; set; }

        public DateTimeOffset CreatedAt { get; set; }
        
    }
}
