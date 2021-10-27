using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BlogocomApiV2.Models
{
    public class UserAvatar
    {
        [ForeignKey("PictureId")]
        [Required]
        public long PictureId { get; set; }

        [ForeignKey("UserId")]
        [Required]
        public long UserId { get; set; }
    }
}
