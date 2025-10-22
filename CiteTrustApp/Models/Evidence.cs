using System;
using System.ComponentModel.DataAnnotations;

namespace CiteTrustApp.Models
{
    public class Evidence
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Vui lòng nhập nội dung bằng chứng.")]
        [Display(Name ="Nội dung bằng chứng")]
        public string Content { get; set; }
        [Required(ErrorMessage = "Vui lòng nhập loại bằng chứng.")]
        [Display(Name = "Loại bằng chứng")]
        public string Type { get; set; }
        [Required(ErrorMessage = "Vui lòng chọn nguồn bằng chứng.")]
        [Display(Name = "Nguồn bằng chứng")]

        public int SourceId { get; set; }
        public virtual Source Source { get; set; }
        [Required(ErrorMessage = "Vui lòng chọn loại bằng chứng.")]
        [Display(Name = "Loại bằng chứng")]
        public int CategoryId { get; set; }
        public virtual Category Category { get; set; }
        [Required(ErrorMessage = "Vui lòng nhập thẻ cho bằng chứng.")]
        [Display(Name = "Tags")]
        public string Tags { get; set; }
        [Display(Name = "Ngày tạo")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy HH:mm:ss}")]
        public DateTime Created { get; set; } = DateTime.Now;
    }
}