using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PaymentAssignment.DAL.Entity
{
   public class PaymentState
    {
        [Required]
        [Key]
        public long PaymentStateId { get; set; }
        [Required]
        public string State { get; set; }
        [Required]
        [Column(nameof(CreatedDate), TypeName = "datetime")]
        public DateTime CreatedDate { get; set; }
        public long PaymentId { get; set; }
        [ForeignKey(nameof(PaymentId))]
        public Payment Payment { get; set; }
    }
}
