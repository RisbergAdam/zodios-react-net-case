using Microsoft.EntityFrameworkCore;
using System;

namespace backend.Models
{
    [PrimaryKey(nameof(Id))]
    public class Transaction
    {
        public Guid Id { get; set; }

        public Account Account { get; set; }

        public decimal Amount { get; set; }

        public DateTimeOffset CreatedAt { get; set; }

    }
}
