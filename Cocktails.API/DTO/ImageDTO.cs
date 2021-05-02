using System;
using System.Collections.Generic;

namespace Cocktails.API.DTO
{
    public class ImageDTO
    {
        public List<string> EncodedImages { get; set; }
        public List<string> Extensions { get; set; }
    }
}
