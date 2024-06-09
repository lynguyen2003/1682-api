using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Entities
{
    public class Users
    {
        public int id { get; set; }
        public string email { get; set; }
        public int phone_number { get; set; }
        public string password_hash { get; set; }
        public string? display_name { get; set; }
        public string? bio_description { get; set; }
        public string? avatar_url { get; set; }
        public int? follower_count { get; set; }
        public int? following_count { get; set; }
        public int? likes_count { get; set; }
        public int? video_count { get; set; }
        public bool is_verify { get; set; }
        public DateTime? Timestamp { get; set; }
        public ICollection<Videos> Videos { get; set; }
        public ICollection<Comments> Comments { get; set; }
        public ICollection<Likes> Likes { get; set; }
        public ICollection<Followers> Followers { get; set; }
        public ICollection<Followers> Followings { get; set; }
        public ICollection<Favourites> Favourites { get; set; }

    }
}
