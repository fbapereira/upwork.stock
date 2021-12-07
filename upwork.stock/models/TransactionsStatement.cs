using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace upwork.stock
{
    /// <summary>
    /// Transaction statement
    /// </summary>
    public class TransactionsStatement
    {
        [Required]
        public string? scheme { get; set; }

        [Required]
        public string? scrip { get; set; }

        [Required]
        public Double? netRate { get; set; }

        [Required]
        public DateTime? statementDate { get; set; } = DateTime.Now;

        [Required]
        public Double? quantity { get; set; }

        [Required]
        public Double? amount { get; set; }

        [Required]
        public Double? realisedGain { get; set; }

        public bool isAvailable { get; set; }

        public bool isBuy { get; set; }
    }
}