using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BlogocomApiV2.Models
{
    public class MessageFile
    {

        [ForeignKey("MessageId")]
        [Required]
        public long MessageId { get; set; }

        [ForeignKey("FileId")]
        [Required]
        public long FileId { get; set; }

    }
}
