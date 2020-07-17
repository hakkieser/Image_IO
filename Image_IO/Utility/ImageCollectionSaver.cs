using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Image_IO.Utility
{
    public class ImageCollectionSaver
    {
        private static ImageCodecInfo GetEncoderInfo(ImageFormat format)
        {
            return ImageCodecInfo.GetImageDecoders().SingleOrDefault(c => c.FormatID == format.Guid);
        }

        private static EncoderParameters GetEncoderParameters()
        {
            Encoder encoder = Encoder.Quality; 

            EncoderParameters encoderParameters = new EncoderParameters(1);
             
            EncoderParameter encoderParameter = new EncoderParameter(encoder, 80l);
            encoderParameters.Param[0] = encoderParameter;

            return encoderParameters;
        }

        public static List<string> Save(Image _originalImage, List<Image> _images, string _directory, string _fileName)
        {
            List<string> result = new List<string>(6);

            ImageCodecInfo imageCodecInfo = GetEncoderInfo(ImageFormat.Jpeg);
            EncoderParameters encoderParameters = GetEncoderParameters();

            string originalSavePath = Path.Combine(Directory.GetCurrentDirectory(), _directory, _fileName);

            if (!Directory.Exists(Path.Combine(Directory.GetCurrentDirectory(), _directory)))
            {
                Directory.CreateDirectory(Path.Combine(Directory.GetCurrentDirectory(), _directory));
            }

            _originalImage.Save(originalSavePath);
            result.Add(Path.Combine(_directory, _fileName).Replace("\\", "/"));

            foreach (var item in _images)
            {
                string eachFileName = Path.GetFileNameWithoutExtension(_fileName) + "_" + item.Width + Path.GetExtension(_fileName);
                string eachSavePath = Path.Combine(Directory.GetCurrentDirectory(), _directory, eachFileName);

                item.Save(eachSavePath, imageCodecInfo, encoderParameters);
                result.Add(Path.Combine(_directory, eachFileName).Replace("\\", "/"));
            }


            return result;
        }

    }
}
