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
    public class ArticleMap : IEntityTypeConfiguration<Article>
    {
        public void Configure(EntityTypeBuilder<Article> builder)
        {
            builder.HasData(new Article
            {
                Id = Guid.NewGuid(),
                Title = "Asp.Net Deneme Makalesi",
                Content = "Bu bir Asp.Net deneme makalesidir.",
                ViewCount = 15,
                CategoryId = Guid.Parse("6AF94D9A-243B-449D-A1A6-9831730AB109"),
                ImageId = Guid.Parse("487F2D55-22DE-46DD-BA51-44920C3BEC7A"),
                CreatedBy = "Admin Test",
                CreatedDate = DateTime.Now,
                IsDeleted = false
            },
            new Article
            {
                Id = Guid.NewGuid(),
                Title = "Visual Studio Deneme Makalesi",
                Content = "Bu bir Visual Studio deneme makalesidir.",
                ViewCount = 15,
                CategoryId = Guid.Parse("8F72866C-3AD0-44BB-A5B7-21E1EC62B163"),
                ImageId = Guid.Parse("A8C4CEBD-5798-4C87-9486-F88C129F267D"),
                CreatedBy = "Admin Test",
                CreatedDate = DateTime.Now,
                IsDeleted = false
            }
            );
        }
    }
}
