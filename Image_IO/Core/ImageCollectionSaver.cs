using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Image_IO.Core
{
    public class ImageCollectionSaver : I_ImageCollectionSaver
    {
        private ImageCodecInfo GetEncoderInfo(ImageFormat format)
        {
            return ImageCodecInfo.GetImageDecoders().SingleOrDefault(c => c.FormatID == format.Guid);
        }

        private EncoderParameters GetEncoderParameters()
        {
            Encoder encoder = Encoder.Quality;

            EncoderParameters encoderParameters = new EncoderParameters(1);

            EncoderParameter encoderParameter = new EncoderParameter(encoder, 80l);
            encoderParameters.Param[0] = encoderParameter;

            return encoderParameters;
        } 

        public List<string> Save(SaveModel _saveModel)
        {
            List<string> result = new List<string>(6);

            ImageCodecInfo imageCodecInfo = GetEncoderInfo(ImageFormat.Jpeg);
            EncoderParameters encoderParameters = GetEncoderParameters();

            string originalSavePath = Path.Combine(Directory.GetCurrentDirectory(), _saveModel.Directory, _saveModel.FileName);

            if (!Directory.Exists(Path.Combine(Directory.GetCurrentDirectory(), _saveModel.Directory)))
            {
                Directory.CreateDirectory(Path.Combine(Directory.GetCurrentDirectory(), _saveModel.Directory));
            }

            _saveModel.OriginalImage.Save(originalSavePath);
            result.Add(Path.Combine(_saveModel.Directory, _saveModel.FileName).Replace("\\", "/"));

            foreach (var item in _saveModel.Images)
            {
                string eachFileName = Path.GetFileNameWithoutExtension(_saveModel.FileName) + "_" + item.Width + Path.GetExtension(_saveModel.FileName);
                string eachSavePath = Path.Combine(Directory.GetCurrentDirectory(), _saveModel.Directory, eachFileName);

                item.Save(eachSavePath, imageCodecInfo, encoderParameters);
                result.Add(Path.Combine(_saveModel.Directory, eachFileName).Replace("\\", "/"));
            }


            return result;
        }
         
    }
}
