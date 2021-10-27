using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlogocomApiV2.Models.Configs
{
    public class UserAvatarConfig : IEntityTypeConfiguration<UserAvatar>
    {
        public void Configure(EntityTypeBuilder<UserAvatar> builder)
        {
            
            builder.HasKey(bc => new { bc.PictureId, bc.UserId });

            //Relations
        }
    }
}
