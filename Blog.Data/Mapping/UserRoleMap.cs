using Blog.Entity.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Data.Mapping
{
    public class UserRoleMap : IEntityTypeConfiguration<AppUserRole>
    {
        public void Configure(EntityTypeBuilder<AppUserRole> builder)
        {
            // Primary key
            builder.HasKey(r => new { r.UserId, r.RoleId });

            // Maps to the AspNetUserRoles table
            builder.ToTable("AspNetUserRoles");

            builder.HasData(new AppUserRole
            {
                UserId = Guid.Parse("0DD20017-ED70-471F-9701-926A8F764EF2"),
                RoleId = Guid.Parse("99173599-18B0-4271-B2C9-1267003053A7"),
            },
            new AppUserRole
            {
                UserId = Guid.Parse("5F04DF46-AA62-4381-B723-B80DED53A3A0"),
                RoleId = Guid.Parse("C10FA6B4-A853-40FA-BA1A-1A84E25577D3"),
            });
        }
    }
}
