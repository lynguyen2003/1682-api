﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.DTO.Video
{
    public class CommentDTO
    {
        public int UserId { get; set; }
        public int VideoId { get; set; }
        public string Comment { get; set; }
    }
}
