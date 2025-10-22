using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CiteTrustApp.Models
{
    public class Source
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "Tiêu đề được để trống")]
        [Display(Name = "Tiêu đề")]
        public string Title { get; set; }
        [Display(Name = "Tác giả")]
        [Required(ErrorMessage = "Tác giả được để trống")]
        public string Author { get; set; }
        [Display(Name = "Năm")]
        [Required(ErrorMessage = "Năm được để trống")]
        public int Year { get; set; }
        [Display(Name = "Nhà xuất bản")]
        [Required(ErrorMessage = "Nhà xuất bản được để trống")]
        public int Publisher { get; set; }
        [Display(Name = "URL")]
        [Required(ErrorMessage = "URL được để trống")]
        public string Url { get; set; }
        [Display(Name = "Điểm tin cậy")]
        [Required(ErrorMessage = "Điểm tin cậy được để trống")]
        public int Credi_score { get; set; }

        //Collection of Evidences
        public virtual ICollection<Evidence> Evidences { get; set; }


    }
}