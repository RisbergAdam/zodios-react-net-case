using System;
using System.ComponentModel.DataAnnotations;

namespace backend.Models.Api
{
    public class CreateTransactionRequestDto
    {

        [Required]
        public Guid AccountId { get; set; }

        [Required]
        public int Amount { get; set; }

    }
}
