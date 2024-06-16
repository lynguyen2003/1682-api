using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.DTO.Video
{
    public class VideoUpdateDTO
    {
        [Required]
        [StringLength(100)]
        public string title { get; set; }

        [StringLength(500)]
        public string description { get; set; }

        [Url]
        public string thumbnail_url { get; set; }
    }
}
