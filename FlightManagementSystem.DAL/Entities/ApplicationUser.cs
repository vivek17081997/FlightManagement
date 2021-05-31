using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace FlightManagementSystem.DAL.Entities
{
    public class ApplicationUser : IdentityUser<long>
    {
        public override long Id { get; set; }
        public override string UserName { get; set; }
        public override string Email { get; set; }
        public override string PasswordHash { get; set; }
        public string PasswordSalt { get; set; }
        public override string PhoneNumber { get; set; }
        public long CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public long? ModifyBy { get; set; }
        public DateTime? Modifydate { get; set; }
        public long? DeletedBy { get; set; }
        public DateTime? DeletedDate { get; set; }
        public bool? IsDeleted { get; set; }
        public bool IsActive { get; set; }
    }
}
