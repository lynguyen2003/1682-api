using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Entities
{
    public class RefreshToken
    {
        public int id { get; set; }
        [ForeignKey("Users")]
        public int user_id { get; set; }
        public string token { get; set; }
        public string jwt_id { get; set; }
        public bool is_used { get; set; }
        public bool is_revoked { get; set; }
        public DateTime added_date { get; set; }
        public DateTime expiry_date { get; set; }
        public Users Users { get; set; }
    }
}
