using System;
using System.Collections.Generic;

namespace Hana.Models.DataModels
{
    public partial class Agent
    {
        public Agent()
        {
            RealEstate = new HashSet<RealEstate>();
            SocialLogin = new HashSet<SocialLogin>();
        }

        public int Id { get; set; }
        public string AgentName { get; set; }
        public string PhoneNumber { get; set; }
        public string? Email { get; set; }
        public string? Facebook { get; set; }
        public string? Zalo { get; set; }
        public string LoginName { get; set; }
        public string Password { get; set; }
        public string? ActiveKey { get; set; }
        public string? ResetPasswordKey { get; set; }
        public DateTime? LastLogin { get; set; }
        public bool IsActive { get; set; }
        public int LevelId { get; set; }
        public bool ConfirmPhoneNumber { get; set; }
        public bool ConfirmEmail { get; set; }
        //public string? VerificationCode { get; set; }

        public virtual Level Level { get; set; }
        public virtual ICollection<RealEstate> RealEstate { get; set; }
        public virtual ICollection<SocialLogin> SocialLogin { get; set; }
    }
}
