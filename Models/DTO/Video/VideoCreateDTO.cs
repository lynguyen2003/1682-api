using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.DTO.Video
{
    public class VideoCreateDTO
    {
        [Required]
        public int user_id { get; set; }

        [Required]
        [StringLength(100)]
        public string title { get; set; }

        [StringLength(500)]
        public string description { get; set; }

        [Required]
        [Url]
        public string video_url { get; set; }

        [Url]
        public string thumbnail_url { get; set; }

    }
}
