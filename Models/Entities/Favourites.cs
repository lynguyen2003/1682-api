using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Entities
{
    public class Favourites
    {
        public int id { get; set; }
        [ForeignKey("Users")]
        public int user_id { get; set; }
        [ForeignKey("Videos")]
        public int video_id { get; set; }
        public Users Users { get; set; }
        public Videos Videos { get; set; }
    }
}
