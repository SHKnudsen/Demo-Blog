﻿using System.ComponentModel.DataAnnotations;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace DemoBlog.Contracts
{
    public class CreateBlogPostDto
    {
        [Required(ErrorMessage = "Slug is required")]
        [StringLength(160, ErrorMessage = "Slug can't be longer than 160 characters")]
        public string Slug { get; set; }

        [Required(ErrorMessage = "Post title is required")]
        [StringLength(160, ErrorMessage = "Post title can't be longer than 160 characters")]
        public string Title { get; set; }

        [StringLength(240, ErrorMessage = "Post sub-title can't be longer than 240 characters")]
        public string SubTitle { get; set; }

        [Required(ErrorMessage = "Post title is required")]
        [StringLength(450, ErrorMessage = "Post title can't be longer than 450 characters")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Post content is required")]
        public string Content { get; set; }
    }
}