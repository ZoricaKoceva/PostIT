using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PostIt.Models
{
    public class Product
    {
        [Key]
        public int Id { get; set; }
        /* The user who posted the product */
        public ApplicationUser User { get; set; }
        /* Title of product */
        [Required]
        [Display(Name = "Име на Продуктот")]
        public string Title { get; set; }
        /* Description of product */
        [Required]
        [Display(Name = "Опис на Продуктот")]
        public string Description { get; set; }
        /* Price of product */
        [Required]
        [Display(Name = "Цена")]
        public float Price { get; set; }
        /* Category of product */
        [Required]
        [Display(Name = "Категорија")]
        public Category Category { get; set; }
        /* Image of product */
        public string Image { get; set; }
        /* Date and time of posting the product */
        [Display(Name = "Огласен на:")]
        public DateTime DateTime { get; set; }
        public bool Approved { get; set; }
        /* Comments on this product */
        public string getImage()
        {
            if (String.IsNullOrEmpty(Image))
            {
                return "https://www.pngitem.com/pimgs/m/287-2876158_not-available-hd-png-download.png";
            }
            else
            {
                return Image;
            }
        }
        /* Initial Constructor */
        public Product()
        {
        }
    }
    public class CreateEditProductModel
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [Display(Name = "Име на Продуктот")]
        public string Title { get; set; }
        [Required]
        [Display(Name = "Опис на Продуктот")]
        public string Description { get; set; }
        [Required]
        [Display(Name = "Цена")]
        public float Price { get; set; }
        [Required]
        [Display(Name = "Категорија")]
        public int CategoryId { get; set; }
        public List<Category> Categories { get; set; }
        [Display(Name = "Фотографија:")]
        public string Image { get; set; }
    }
}