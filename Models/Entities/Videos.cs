using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Entities
{
    public class Videos
    {
        public int id { get; set; }
        [ForeignKey("Users")]
        public int user_id { get; set; }
        public string? title { get; set; }
        public string? description { get; set; }
        public string video_url { get; set; }
        public string? thumbnail_url { get; set; }
        public int likes_count { get; set; }
        public int views_count { get; set; }
        public DateTime? Timestamp { get; set; }
        public Users Users { get; set; }
        public ICollection<Comments> Comments { get; set; }
        public ICollection<Likes> Likes { get; set; }
        public ICollection<Favourites> Favourites { get; set; }

    }
}
