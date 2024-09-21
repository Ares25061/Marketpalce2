using System;
using System.Collections.Generic;

namespace DataAccess.Models
{
    public partial class User
    {
        public User()
        {
            Addresses = new HashSet<Address>();
            ChatParticipants = new HashSet<ChatParticipant>();
            FilePermissions = new HashSet<FilePermission>();
            Messages = new HashSet<Message>();
            Notifications = new HashSet<Notification>();
            Orders = new HashSet<Order>();
            PaymentUsers = new HashSet<PaymentUser>();
            Products = new HashSet<Product>();
            Reviews = new HashSet<Review>();
            SearchHistories = new HashSet<SearchHistory>();
            UserDiscounts = new HashSet<UserDiscount>();
            UserFiles = new HashSet<UserFile>();
            UserRoles = new HashSet<UserRole>();
        }

        public int UserId { get; set; }
        public string Username { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public bool? IsActive { get; set; }
        public bool? IsDeleted { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int? ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public int? DeletedBy { get; set; }
        public DateTime? DeletedDate { get; set; }

        public virtual ICollection<Address> Addresses { get; set; }
        public virtual ICollection<ChatParticipant> ChatParticipants { get; set; }
        public virtual ICollection<FilePermission> FilePermissions { get; set; }
        public virtual ICollection<Message> Messages { get; set; }
        public virtual ICollection<Notification> Notifications { get; set; }
        public virtual ICollection<Order> Orders { get; set; }
        public virtual ICollection<PaymentUser> PaymentUsers { get; set; }
        public virtual ICollection<Product> Products { get; set; }
        public virtual ICollection<Review> Reviews { get; set; }
        public virtual ICollection<SearchHistory> SearchHistories { get; set; }
        public virtual ICollection<UserDiscount> UserDiscounts { get; set; }
        public virtual ICollection<UserFile> UserFiles { get; set; }
        public virtual ICollection<UserRole> UserRoles { get; set; }
    }
}
