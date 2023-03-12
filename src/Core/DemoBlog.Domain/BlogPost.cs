using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;


namespace DemoBlog.Domain
{
    public class BlogPost
    {
        public int Id { get; set; }
        public int AuthorId { get; set; }

        [Required]
        [StringLength(160)]
        public string Slug { get; set; }

        [Required]
        [StringLength(160)]
        public string Title { get; set; }

        [StringLength(240)]
        public string SubTitle { get; set; }
        
        [Required]
        [StringLength(450)]
        public string Description { get; set; }
        
        [Required]
        public string Content { get; set; }
    }
}
