using System;
using System.Collections.Generic;

namespace Domain.Models
{
    public partial class Attribute
    {
        public Attribute()
        {
            AttributeValues = new HashSet<AttributeValue>();
            CategoryAttributes = new HashSet<CategoryAttribute>();
            ProductAttributes = new HashSet<ProductAttribute>();
        }

        public int AttributeId { get; set; }
        public string AttributeName { get; set; } = null!;
        public string Description { get; set; } = null!;
        public int CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;

        public virtual ICollection<AttributeValue> AttributeValues { get; set; }
        public virtual ICollection<CategoryAttribute> CategoryAttributes { get; set; }
        public virtual ICollection<ProductAttribute> ProductAttributes { get; set; }
    }
}