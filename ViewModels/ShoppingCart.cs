﻿using System;
using CodeFuseAI_Shared.Models;

namespace CodeFuseAI_BlogCart.ViewModels
{
    public class ShoppingCart
    {

        public int ProductId { get; set; }

        public ProductDTO Product { get; set; }

        public int ProductPriceId { get; set; }

        public ProductPriceDTO ProductPrice { get; set; }

        public int Count { get; set; }

    }
}

