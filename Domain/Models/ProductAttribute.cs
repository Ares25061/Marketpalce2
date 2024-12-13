using System;
using System.Collections.Generic;

namespace Domain.Models
{
    public partial class ProductAttribute
    {
        public int ProductAttributeId { get; set; }
        public int ProductId { get; set; }
        public int AttributeId { get; set; }
        public int ValueId { get; set; }

        public virtual Attribute Attribute { get; set; } = null!;
        public virtual Product Product { get; set; } = null!;
        public virtual AttributeValue Value { get; set; } = null!;
    }
}