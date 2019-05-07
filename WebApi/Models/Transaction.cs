using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WebApi.Models
{
    public class Transaction
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string TransactionId { get; set; }

        public string UserId { get; set; }

        public string ShopId { get; set; }

        public double TotalBill { get; set; }

        public DateTime? TransactionDate { get; set; }

        public string TransactionNumber { get; set; }

        [ForeignKey("UserId")]
        public virtual ApplicationUser User { get; set; }

        [ForeignKey("ShopId")]
        public virtual Shop Shop { get; set; }

        public virtual ICollection<Item> Items{ get; set; }
    }
}