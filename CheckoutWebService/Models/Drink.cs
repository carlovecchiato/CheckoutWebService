using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CheckoutWebService.Models
{
    public class Drink
    {
        public string Name { get; set; }
        [Range(0, 999)]
        public int Quantity { get; set; }
    }
}