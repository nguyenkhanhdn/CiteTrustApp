using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CiteTrustApp.Models
{
    public class Category
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "Loại được để trống")]
        [Display(Name = "Loại")]
        public string Name { get; set; }
        [Display(Name = "Mô tả")]
        [Required(ErrorMessage = "Mô tả được để trống")]
        public string Description { get; set; }
        [Display(Name = "Hình ảnh")]
        [Required(ErrorMessage = "Hình ảnh được để trống")]
        public string ImageUrl { get; set; }

        //Collection of Evidences
        public virtual ICollection<Evidence> Evidences { get; set; }

    }
}