using System;
using System.Collections.Generic;

namespace Domain.Models
{
    public partial class Product
    {
        public Product()
        {
            Images = new HashSet<Image>();
            OrderItems = new HashSet<OrderItem>();
            PriceHistories = new HashSet<PriceHistory>();
            ProductAttributes = new HashSet<ProductAttribute>();
            Reviews = new HashSet<Review>();
        }

        public int ProductId { get; set; }
        public string ProductName { get; set; } = null!;
        public string Description { get; set; } = null!;
        public decimal Price { get; set; }
        public int CategoryId { get; set; }
        public int SellerId { get; set; }
        public bool IsDeleted { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public int ModifiedBy { get; set; }
        public DateTime ModifiedDate { get; set; } = DateTime.Now;
        public int? DeletedBy { get; set; }
        public DateTime? DeletedDate { get; set; }

        public virtual Category Category { get; set; } = null!;
        public virtual User Seller { get; set; } = null!;
        public virtual ICollection<Image> Images { get; set; }
        public virtual ICollection<OrderItem> OrderItems { get; set; }
        public virtual ICollection<PriceHistory> PriceHistories { get; set; }
        public virtual ICollection<ProductAttribute> ProductAttributes { get; set; }
        public virtual ICollection<Review> Reviews { get; set; }
    }
}