using System;
using System.ComponentModel.DataAnnotations;

namespace BlogocomApiV2.Models
{
    public class BaseModel
    {
        public BaseModel()
        {
            CreatedDate = DateTime.UtcNow;
        }

        [Key]
        public long Id { get; set; }
        public DateTimeOffset CreatedDate { get; set; }
    }
}
