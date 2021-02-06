using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PaymentAssignment.DAL.Entity
{
    [Table(name: "Payment")]
    public class Payment
    {
        [Key]
        public long PaymentId { get; set; }

        [Required]
        public string CreditCardNumber { get; set; }


        [Required]
        public string CardHolder { get; set; }

        [Required]
        public DateTime ExpirationDate { get; set; }


        [Column(nameof(SecurityCode), TypeName = "nvarchar(3)")]
        public string SecurityCode { get; set; }

        [Required]
        public decimal Amount { get; set; }

        [InverseProperty(nameof(PaymentState.Payment))]
        public virtual ICollection<PaymentState> PaymentStates { get; set; }


    }

}
