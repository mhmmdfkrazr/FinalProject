using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PaymentApp.Models
{
    public class PaymentItem
    {
        [Key] public int paymentDetailId { get; set; }
        public string CardOwnerName { get; set; }
        public string CardNumber { get; set; }
        public DateTime ExpirationDate { get; set; }
        public string SecurityCode { get; set; }
    }
}