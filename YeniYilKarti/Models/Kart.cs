using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace YeniYilKarti.Models
{
    [Table("Kartlar")]
    public class Kart
    {
        public int Id { get; set; }

        [Display(Name = "Başlık")]
        [Required(ErrorMessage ="{0} alanı zorunludur")]
        [MaxLength(50,ErrorMessage ="{0} en fazla {1} karekter içerebilir.")]
        public string Baslik { get; set; }

        [Display(Name = "Gönderen")]
        [Required(ErrorMessage = "{0} alanı zorunludur")]
        [MaxLength(30, ErrorMessage = "{0} en fazla {1} karakter içerebilir.")]
        public string GonderenAd { get; set; }

        [Display(Name = "Alıcı")]
        [Required(ErrorMessage ="{0} alanı zorunludur")]
        [MaxLength(50,ErrorMessage ="{0} en fazla {1} karakter içerebilir.")]
        public string AliciAd { get; set; }

        [Display(Name = "Mesaj Satırı 1")]
        [Required(ErrorMessage = "{0} alanı zorunludur")]
        [MaxLength(400, ErrorMessage = "{0} en fazla {1} karekter içerebilir.")]
        public string Mesaj { get; set; }
        public string ResimAd { get; set; }

        [ForeignKey("User")]
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }
    }
}