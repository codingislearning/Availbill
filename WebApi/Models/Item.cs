using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WebApi.Models
{
    public class Item
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string ItemId { get; set; }

        public string TransactionId { get; set; }

        public ItemDetail Detail { get; set; }

        [ForeignKey("TransactionId")]
        public  virtual Transaction Transaction { get; set; }
    }
}