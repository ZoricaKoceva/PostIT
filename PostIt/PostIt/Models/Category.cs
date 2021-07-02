using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PostIt.Models
{
    public class Category
    {
        [Key]
        public int Id { get; set; }
        [Display(Name = "Име на категорија")]
        [Required]
        public string Name { get; set; }
        [Required]
        [Display(Name = "Image src")]
        public string Image { get; set; }
        [Display(Name = "FontAwesome ICON")]
        public string Icon { get; set; }
        [Display(Name = "Опис")]
        public string Description { get; set; }
    }
}