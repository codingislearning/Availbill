using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WebApi.Models
{
    public class Shop
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string ShopId { get; set; }

        public ShopDetail ShopDetails { get; set; }

        [JsonIgnore]
        public virtual ICollection<Transaction> Transactions { get; set; }

        [JsonIgnore]
        public virtual ICollection<ApplicationUser> Users { get; set; }
    }
}