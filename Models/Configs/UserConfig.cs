using BlogocomApiV2.Settings;
using Bogus;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlogocomApiV2.Models.Configs
{
    public class UserConfig : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            HashSalt data = new HashPassword("secret123").EncryptPassword();

            //builder.Property(t => t.IsAccess).IsRequired(true);
            //builder.Property(t => t.Email).IsRequired(false);


            //Seeds
           /* var Ids = 2;
            var userFaker = new Faker<User>()

                .RuleFor(o => o.Id, f => Ids++)
                .RuleFor(o => o.FirstName, f => f.Name.FirstName())
                .RuleFor(o => o.IsAccess, true)
                .RuleFor(o => o.Phone, p => p.Phone.PhoneNumber())
                .RuleFor(o => o.Email, f => f.Internet.Email())
                .RuleFor(o => o.Password, data.Hash)
                .RuleFor(o => o.StoredSalt, data.Salt)
                .RuleFor(o => o.CreatedDate, DateTimeOffset.Now);*/

            //IList<User> users = userFaker.Generate(9);

            builder.HasData(

                    new User
                    {
                        Id = 1,
                        FirstName = new Faker ().Name.FirstName(),
                        IsAccess = true,
                        Phone = "+380994444333",
                        Email = new Faker().Internet.Email(),
                        Password = data.Hash,
                        StoredSalt = data.Salt,
                        CreatedDate = DateTimeOffset.Now
                    },
                    new User
                    {
                        Id = 2,
                        FirstName = new Faker().Name.FirstName(),
                        IsAccess = true,
                        Phone = "+380994444222",
                        Email = new Faker().Internet.Email(),
                        Password = data.Hash,
                        StoredSalt = data.Salt,
                        CreatedDate = DateTimeOffset.Now
                    },
                    new User
                    {
                        Id = 3,
                        FirstName = new Faker().Name.FirstName(),
                        IsAccess = true,
                        Phone = "+380994444111",
                        Email = new Faker().Internet.Email(),
                        Password = data.Hash,
                        StoredSalt = data.Salt,
                        CreatedDate = DateTimeOffset.Now
                    }
                );
            //builder.HasData( users);
        }
    }
}
