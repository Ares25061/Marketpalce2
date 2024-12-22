namespace BlazorClient
{
    public class User
    {
        public int UserId { get; set; }
        public string UserName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public bool? IsActive { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
        public int ModifiedBy { get; set; }
        public DateTime ModifiedDate { get; set; }
        public int? DeletedBy { get; set; }
        public DateTime? DeletedDate { get; set; }
        public int RoleId { get; set; }
        public DateTime Created { get; set; } = DateTime.UtcNow;
        public DateTime Updated { get; set; }
        public bool AcceptTerms { get; set; }
        public string? VerificationToken { get; set; }
        public DateTime? Verified { get; set; }
        public bool IsVerified { get; set; }
        public DateTime? PasswordReset { get; set; }
        public string? ResetToken { get; set; }
        public DateTime? ResetTokenExpires { get; set; }
    }
}