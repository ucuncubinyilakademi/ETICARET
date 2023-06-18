using ETICARET.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ETICARET.Models
{
    public class OrderDetailsModel
    {
        public int OrderId { get; set; }
        public string OrderNumber { get; set; }
        public double Total { get; set; }
        public DateTime OrderDate { get; set; }
        public EnumOrderState OrderState { get; set; }

        public string AdresBasligi { get; set; }
        public string Adres { get; set; }
        public string Sehir { get; set; }
        public string Semt { get; set; }
        public string Mahalle { get; set; }
        public string PostaKodu { get; set; }
        public virtual List<OrderItemModel> OrderItems { get; set; }

        public OrderDetailsModel()
        {
            OrderItems = new List<OrderItemModel>();
        }
    }
    public class OrderItemModel
    {     
        public int Quantity { get; set; }
        public double Price { get; set; }
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public string Image { get; set; }
        
    }
}