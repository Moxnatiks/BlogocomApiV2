using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BlogocomApiV2.Models
{
    public class Chat : BaseModel
    {
        public Chat ()
        {
            IsPrivate = true;
        }

        [StringLength(30)]
        [MaxLength(30)]
        public string? Name { get; set; }

        public bool IsPrivate { get; set; }

        [Required]
        public long UserId { get; set; }

        public virtual ICollection<UserChat> UserChats { get; set; }
        public virtual ICollection<Message> Messages { get; set; }
        public virtual ICollection<User> Users { get; set; }


    }
}
