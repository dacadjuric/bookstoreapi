﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Application.DataTransfer
{
    public class ImageDTO
    {
        public int Id { get; set; }
        public string Src { get; set; }
        public string Alt { get; set; }
        public int BookId { get; set; }
    }
}
