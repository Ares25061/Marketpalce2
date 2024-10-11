using System;
using System.Collections.Generic;

namespace Domain.Models
{
    public partial class AttributeValue
    {
        public AttributeValue()
        {
            ProductAttributes = new HashSet<ProductAttribute>();
        }

        public int ValueId { get; set; }
        public int AttributeId { get; set; }
        public string ValueName { get; set; } = null!;
        public string Description { get; set; } = null!;
        public bool IsDeleted { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public int? DeletedBy { get; set; }
        public DateTime? DeletedDate { get; set; }

        public virtual Attribute Attribute { get; set; } = null!;
        public virtual ICollection<ProductAttribute> ProductAttributes { get; set; }
    }
}