using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.DTO.Video
{
    public class VideoDTO
    {
        public int id { get; set; }
        public int user_id { get; set; }
        public string title { get; set; }
        public string description { get; set; }
        public string video_url { get; set; }
        public string thumbnail_url { get; set; }
        public int likes_count { get; set; }
        public int views_count { get; set; }

        public bool IsLikedByUser { get; set; } 
        public int CommentsCount { get; set; }

        public DateTime? Timestamp { get; set; }
    }
}
