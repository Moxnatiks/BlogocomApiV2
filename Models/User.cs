using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BlogocomApiV2.Models
{
    public class User : BaseModel
    {
        [StringLength(30)]
        [MaxLength(30)]
        public string? FirstName { get; set; }

        [StringLength(20)]
        [MaxLength(20)]
        public string Phone { get; set; }
        [StringLength(40)]
        [MaxLength(40)]
        public string? Email { get; set; }
        public bool IsAccess { get; set; }
        public string Password { get; set; }
        public byte[] StoredSalt { get; set; }

        public virtual ICollection<UserChat> UserChats { get; set; }
        public virtual ICollection<Chat> Chats { get; set; }
    }
}
