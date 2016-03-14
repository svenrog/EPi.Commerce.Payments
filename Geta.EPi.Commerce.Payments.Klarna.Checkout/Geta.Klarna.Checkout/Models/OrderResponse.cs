﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Geta.Klarna.Checkout.Models
{
    public class OrderResponse
    {
        public string Snippet { get; set; }
        public int TotalCost { get; set; }
        public string CustomerName { get; set; }
        
        public OrderResponse(string snippet, int totalCost, string customerName)
        {
            Snippet = snippet;
            TotalCost = totalCost;
            CustomerName = customerName;
        }
    }
}
