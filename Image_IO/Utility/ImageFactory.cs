using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;

namespace Image_IO.Utility
{

    public class ImageFactory
    {
        public static List<Image> CreateNewSizeImages(Image _image)
        {
            List<Image> result = new List<Image>(5);
            List<int> widthList = new List<int>(5)
            {
                150, 
                200,
                250,
                350,
                600
            };

            for (int i = 0; i < widthList.Count; i++)
            {
                result.Add(ImageResizer.Resize(_image, widthList[i], 1080, true));
            }

            return result;
        }
    }
}
