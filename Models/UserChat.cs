using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BlogocomApiV2.Models
{
    public class UserChat
    {
        public UserChat()
        {
            DateAdded = DateTime.UtcNow;
        }

        [ForeignKey("ChatId")]
        [Required]
        public long ChatId { get; set; }

        [ForeignKey("UserId")]
        [Required]
        public long UserId { get; set; }
        public string? Status { get; set; }
        public DateTimeOffset DateAdded { get; set; }

        public virtual Chat Chat { get; set; }
        public virtual User User { get; set; }
    }
}
