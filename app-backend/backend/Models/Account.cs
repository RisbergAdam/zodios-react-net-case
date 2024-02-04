using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;

namespace backend.Models
{
    [PrimaryKey(nameof(Id))]
    public class Account
    {
        public Guid Id { get; set; }

        public decimal Balance { get; set; }

        public ICollection<Transaction> Transactions { get; set; }

    }
}
