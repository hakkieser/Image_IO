using Grpc.Core;
using Image_IO.Core;
using Image_IO.Utility;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Image_IO
{
    public class ImageUploadService : ImageUploader.ImageUploaderBase
    {
        private readonly I_ImageCollectionSaver imageCollectionSaver = null;

        public ImageUploadService(I_ImageCollectionSaver _imageCollectionSaver)
        {
            imageCollectionSaver = _imageCollectionSaver;
        }
        public override Task<UploadReply> Upload(UploadRequest request, ServerCallContext context)
        {
            UploadReply result = new UploadReply();

            try
            {
                Stream imageStream = new MemoryStream(request.File.ToByteArray());
                Image originalImage = Image.FromStream(imageStream);
                List<Image> images = ImageFactory.CreateNewSizeImages(originalImage);

                SaveModel saveModel = new SaveModel()
                {
                    Directory = request.Directory,
                    FileName = request.FileName,
                    Images = images,
                    OriginalImage = originalImage
                };

                var saveList = imageCollectionSaver.Save(saveModel);

                result.UploadPath = "upload/" + request.Directory + request.FileName;
            }
            catch (Exception ex)
            {
                result = new UploadReply() { ErrorMessage = ex.Message, HasError = true };
            }

            return Task.FromResult(result);
        }

          
    }
}
