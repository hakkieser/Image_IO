using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;

namespace Image_IO.Core
{
    public class SaveModel
    {
        public Image OriginalImage { get; set; }
        public List<Image> Images { get; set; }
        public string Directory { get; set; }
        public string FileName { get; set; } 
    }
}
