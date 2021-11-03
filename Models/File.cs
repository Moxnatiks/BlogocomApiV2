using GraphQL;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BlogocomApiV2.Models
{
    public class File : BaseModel
    {
        
        public string OriginalName { get; set; }

        [StringLength(100)]
        [MaxLength(100)]
        public string  UniqueName { get; set; }
        public long Size { get; set; }

        [StringLength(10)]
        [MaxLength(10)]
        public string Type { get; set; }
        public string WebPath { get; set; }
        public string? PreviewVideoPicWebPart { get; set; }



    }
}
