﻿using Blog.Core.Entities;

namespace Blog.Entity.Entities
{
    public class Article : EntityBase
    {
        //makale entity propertyleri
        public string Title { get; set; }
        public string Content { get; set; }  // içerik
        public int ViewCount { get; set; }  //görüntüleme

        public Guid CategoryId { get; set; }
        public Category Category { get; set; }

        public Guid ImageId { get; set; }
        public Image Image { get; set; }


    }
}
