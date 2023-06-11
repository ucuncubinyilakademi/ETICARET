using ETICARET.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ETICARET.Models
{
    public class Cart
    {
        public List<CartItem> CartItems { get; set; }

        public Cart()
        {
            CartItems = new List<CartItem>();
        }
        public void AddToProduct(Product product, int quantity)
        {
            var item = CartItems.FirstOrDefault(i => i.Product.Id == product.Id);
            if (item == null)
            {
                CartItems.Add(new CartItem() { Product = product, Quantity=quantity });
            }
            else
            {
                item.Quantity += quantity;
            }
        }

        public void DeleteProduct(Product product)
        {
            CartItems.RemoveAll(i => i.Product.Id == product.Id);
        }

        public double Total()
        {
            return CartItems.Sum(i => i.Product.Price * i.Quantity);
        }

        public void Clear()
        {
            CartItems.Clear();
        }
    }

    public class CartItem
    {
        public Product Product { get; set; }
        public int Quantity { get; set; }
    }
}