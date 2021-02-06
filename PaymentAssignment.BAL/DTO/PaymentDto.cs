using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace PaymentAssignment.BAL.DTO
{
   public class PaymentDto
    {
        [Required]
        [RegularExpression(@"^(?:4[0-9]{12}(?:[0-9]{3})?|[25][1 - 7][0 - 9]{ 14}| 6(?:011 | 5[0 - 9][0 - 9])[0 - 9]{ 12}| 3[47][0 - 9]{ 13}| 3(?:0[0 - 5] |[68][0 - 9])[0 - 9]{ 11}| (?: 2131 | 1800 | 35\d{ 3})\d{ 11})$"
                           , ErrorMessage = "Please enter valid CCN")]
        public string CreditCardNumber { get; set; }
        [Required]
        public string CardHolder { get; set; }
        [Required]
        public DateTime ExpirationDate { get; set; }

        [RegularExpression(@"^\d{3}$", ErrorMessage = "Please enter 3 digits")]
        public string SecurityCode { get; set; }

        [Required]
        [RegularExpression(@"^(?:[1-9]\d*(?:\.\d+)?|0\.0*[1-9]\d*)$", ErrorMessage = "You should enter valid amount")]

        public decimal Amount { get; set; }
    }
}
