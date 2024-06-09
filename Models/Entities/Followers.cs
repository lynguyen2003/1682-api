using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Entities
{
    public class Followers
    {
        public int id { get; set; }
        [ForeignKey("Follower")]
        public int follower_id { get; set; }
        [ForeignKey("Following")]
        public int following_id { get; set; }
        public Users Follower { get; set; }
        public Users Following { get; set; }
    }
}
