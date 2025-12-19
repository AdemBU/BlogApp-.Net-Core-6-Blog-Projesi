using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Entity.Entities
{
    public class AppUser : IdentityUser<Guid>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public Guid ImageId { get; set; } = Guid.Parse("487F2D55-22DE-46DD-BA51-44920C3BEC7A");
        public Image Image { get; set; }
        public ICollection<Article> Articles { get; set; }
    }
}
