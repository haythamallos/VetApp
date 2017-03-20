using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Net;


namespace Vetapp.Engine.Common
{
    public class UtilsImage
    {
        private bool _hasError = false;
        private string _errorMessage = null;
        private string _errorStacktrace = null;

        public bool HasError
        {
            get { return _hasError; }
            set { _hasError = value; }
        }

        public string ErrorStacktrace
        {
            get { return _errorStacktrace; }
        }

        public string ErrorMessage
        {
            get { return _errorMessage; }
        }

        public UtilsImage()
        { }

        /// <summary>
        /// Function to download Image from website
        /// </summary>
        /// <param name="_URL">URL address to download image</param>
        /// <returns>Image</returns>
        public static Image DownloadImage(string _URL)
        {
            Image _tmpImage = null;
            // Open a connection
            System.Net.HttpWebRequest _HttpWebRequest = (System.Net.HttpWebRequest)System.Net.HttpWebRequest.Create(_URL);

            _HttpWebRequest.AllowWriteStreamBuffering = true;

            // You can also specify additional header values like the user agent or the referer: (Optional)
            //_HttpWebRequest.UserAgent = "Mozilla/4.0 (compatible; MSIE 7.0; Windows NT 5.1)";
            //_HttpWebRequest.Referer = "http://www.google.com/";

            // set timeout for 20 seconds (Optional)
            //_HttpWebRequest.Timeout = 20000;

            // Request response:
            System.Net.WebResponse _WebResponse = _HttpWebRequest.GetResponse();

            // Open data stream:
            System.IO.Stream _WebStream = _WebResponse.GetResponseStream();

            // convert webstream to image
            _tmpImage = Image.FromStream(_WebStream);

            // Cleanup
            _WebResponse.Close();
            _WebResponse.Close();

            return _tmpImage;
        }

        public static Image ByteArrayToImage(byte[] myByteArray)
        {
            MemoryStream ms = new MemoryStream(myByteArray, 0, myByteArray.Length);
            ms.Write(myByteArray, 0, myByteArray.Length);
            return Image.FromStream(ms);
        }

        public static byte[] imageToByteArray(System.Drawing.Image imageIn)
        {
            MemoryStream ms = new MemoryStream();
            //imageIn.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
            imageIn.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
            return ms.ToArray();
        }

        public static Image byteArrayToImage(byte[] byteArrayIn)
        {
            MemoryStream ms = new MemoryStream(byteArrayIn);
            Image returnImage = Image.FromStream(ms);
            return returnImage;
        }

        public static byte[] ImageToByte(Image img)
        {
            ImageConverter converter = new ImageConverter();

            return (byte[])converter.ConvertTo(img, typeof(byte[]));
        }

        public static Stream ImageToStream(string pStrImagePath)
        {
            Image image = Image.FromFile(pStrImagePath);
            MemoryStream stream = new MemoryStream();
            // Save image to stream.
            image.Save(stream, ImageFormat.Bmp);
            return stream;
        }

        public static Image ImagePathToObject(string pStrImagePath)
        {
            Image image = Image.FromFile(pStrImagePath);
            return image;
        }

        public static string ResizeImage(Stream pOriginalFileContent, string pStrUploadDirectory, int pIntSize)
        {
            string filename = null;

            // Create a bitmap of the content of the fileUpload control in memory
            Bitmap originalBMP = new Bitmap(pOriginalFileContent);

            int thumbnailSize = pIntSize;
            int newWidth, newHeight;
            if (originalBMP.Width > originalBMP.Height)
            {
                newWidth = thumbnailSize;
                newHeight = originalBMP.Height * thumbnailSize / originalBMP.Width;
            }
            else
            {
                newWidth = originalBMP.Width * thumbnailSize / originalBMP.Height;
                newHeight = thumbnailSize;
            }

            // Create a new bitmap which will hold the previous resized bitmap
            Bitmap newBMP = new Bitmap(originalBMP, newWidth, newWidth);

            // Create a graphic based on the new bitmap
            Graphics oGraphics = Graphics.FromImage(newBMP);
            // Set the properties for the new graphic file
            oGraphics.SmoothingMode = SmoothingMode.AntiAlias;
            oGraphics.InterpolationMode = InterpolationMode.HighQualityBicubic;

            // Draw the new graphic based on the resized bitmap
            oGraphics.DrawImage(originalBMP, 0, 0, newWidth, newWidth);
            // Save the new graphic file to the server
            Guid g = Guid.NewGuid();
            filename = g.ToString() + "_" + newWidth + "x" + newWidth + ".bmp";

            newBMP.Save(pStrUploadDirectory + Path.DirectorySeparatorChar + filename);

            // Once finished with the bitmap objects, we deallocate them.
            originalBMP.Dispose();
            newBMP.Dispose();
            oGraphics.Dispose();
            return filename;
        }

        public static void ResizeImage(string OrigFile, string NewFile, int NewWidth, int MaxHeight, bool ResizeIfWider)
        {
            System.Drawing.Image FullSizeImage = System.Drawing.Image.FromFile(OrigFile);
            // Ensure the generated thumbnail is not being used by rotating it 360 degrees
            FullSizeImage.RotateFlip(System.Drawing.RotateFlipType.Rotate180FlipNone);
            FullSizeImage.RotateFlip(System.Drawing.RotateFlipType.Rotate180FlipNone);
            if (ResizeIfWider)
            {
                if (FullSizeImage.Width <= NewWidth)
                {
                    NewWidth = FullSizeImage.Width;
                }
            }
            int NewHeight = FullSizeImage.Height * NewWidth / FullSizeImage.Width;
            if (NewHeight > MaxHeight) // Height resize if necessary
            {
                NewWidth = FullSizeImage.Width * MaxHeight / FullSizeImage.Height;
                NewHeight = MaxHeight;
            }
            // Create the new image with the sizes we've calculated
            System.Drawing.Image NewImage = FullSizeImage.GetThumbnailImage(NewWidth, NewHeight, null, IntPtr.Zero);
            FullSizeImage.Dispose();
            NewImage.Save(NewFile);
        }

        public static string SaveImage(Stream pOriginalFileContent, string pStrUploadDirectory)
        {
            string filename = null;

            // Create a bitmap of the content of the fileUpload control in memory
            Bitmap originalBMP = new Bitmap(pOriginalFileContent);

            // Save the new graphic file to the server
            Guid g = Guid.NewGuid();
            filename = g.ToString() + ".bmp";

            originalBMP.Save(pStrUploadDirectory + Path.DirectorySeparatorChar + filename);

            // Once finished with the bitmap objects, we deallocate them.
            originalBMP.Dispose();
            originalBMP.Dispose();
            return filename;
        }

        public static string SaveImage(Bitmap originalBMP, string pStrUploadDirectory)
        {
            string filename = null;
            // Save the new graphic file to the server
            Guid g = Guid.NewGuid();
            filename = g.ToString() + ".bmp";

            originalBMP.Save(pStrUploadDirectory + Path.DirectorySeparatorChar + filename);

            // Once finished with the bitmap objects, we deallocate them.
            originalBMP.Dispose();
            originalBMP.Dispose();
            return filename;
        }

        // New

        //set a working directory
        //string WorkingDirectory = @"C:\Projects\Tutorials\ImageResize";

        ////create a image object containing a verticel photograph
        //Image imgPhotoVert = Image.FromFile(WorkingDirectory + @"\imageresize_vert.jpg");
        //Image imgPhotoHoriz = Image.FromFile(WorkingDirectory + @"\imageresize_horiz.jpg");
        //Image imgPhoto = null;

        //imgPhoto = ScaleByPercent(imgPhotoVert, 50);
        //imgPhoto.Save(WorkingDirectory + @"\images\imageresize_1.jpg", ImageFormat.Jpeg);
        //imgPhoto.Dispose();

        //imgPhoto = ConstrainProportions(imgPhotoVert, 200, Dimensions.Width);
        //imgPhoto.Save(WorkingDirectory + @"\images\imageresize_2.jpg", ImageFormat.Jpeg);
        //imgPhoto.Dispose();

        //imgPhoto = FixedSize(imgPhotoVert, 200, 200);
        //imgPhoto.Save(WorkingDirectory + @"\images\imageresize_3.jpg", ImageFormat.Jpeg);
        //imgPhoto.Dispose();

        //imgPhoto = Crop(imgPhotoVert, 200, 200, AnchorPosition.Center);
        //imgPhoto.Save(WorkingDirectory + @"\images\imageresize_4.jpg", ImageFormat.Jpeg);
        //imgPhoto.Dispose();

        //imgPhoto = Crop(imgPhotoHoriz, 200, 200, AnchorPosition.Center);
        //imgPhoto.Save(WorkingDirectory + @"\images\imageresize_5.jpg", ImageFormat.Jpeg);
        //imgPhoto.Dispose();

        public enum Dimensions
        {
            Width,
            Height
        }

        public enum AnchorPosition
        {
            Top,
            Center,
            Bottom,
            Left,
            Right
        }

        public static Image ScaleByPercent(Image imgPhoto, int Percent)
        {
            float nPercent = ((float)Percent / 100);

            int sourceWidth = imgPhoto.Width;
            int sourceHeight = imgPhoto.Height;
            int sourceX = 0;
            int sourceY = 0;

            int destX = 0;
            int destY = 0;
            int destWidth = (int)(sourceWidth * nPercent);
            int destHeight = (int)(sourceHeight * nPercent);

            Bitmap bmPhoto = new Bitmap(destWidth, destHeight, PixelFormat.Format24bppRgb);
            bmPhoto.SetResolution(imgPhoto.HorizontalResolution, imgPhoto.VerticalResolution);

            Graphics grPhoto = Graphics.FromImage(bmPhoto);
            grPhoto.InterpolationMode = InterpolationMode.HighQualityBicubic;

            grPhoto.DrawImage(imgPhoto,
                new Rectangle(destX, destY, destWidth, destHeight),
                new Rectangle(sourceX, sourceY, sourceWidth, sourceHeight),
                GraphicsUnit.Pixel);

            grPhoto.Dispose();
            return bmPhoto;
        }

        public static Image ConstrainProportions(Image imgPhoto, int Size, Dimensions Dimension)
        {
            int sourceWidth = imgPhoto.Width;
            int sourceHeight = imgPhoto.Height;
            int sourceX = 0;
            int sourceY = 0;
            int destX = 0;
            int destY = 0;
            float nPercent = 0;

            switch (Dimension)
            {
                case Dimensions.Width:
                    nPercent = ((float)Size / (float)sourceWidth);
                    break;

                default:
                    nPercent = ((float)Size / (float)sourceHeight);
                    break;
            }

            int destWidth = (int)(sourceWidth * nPercent);
            int destHeight = (int)(sourceHeight * nPercent);

            Bitmap bmPhoto = new Bitmap(destWidth, destHeight, PixelFormat.Format24bppRgb);
            bmPhoto.SetResolution(imgPhoto.HorizontalResolution, imgPhoto.VerticalResolution);

            Graphics grPhoto = Graphics.FromImage(bmPhoto);
            grPhoto.InterpolationMode = InterpolationMode.HighQualityBicubic;

            grPhoto.DrawImage(imgPhoto,
            new Rectangle(destX, destY, destWidth, destHeight),
            new Rectangle(sourceX, sourceY, sourceWidth, sourceHeight),
            GraphicsUnit.Pixel);

            grPhoto.Dispose();
            return bmPhoto;
        }

        public static Image FixedSize(Image imgPhoto, int Width, int Height)
        {
            int sourceWidth = imgPhoto.Width;
            int sourceHeight = imgPhoto.Height;
            int sourceX = 0;
            int sourceY = 0;
            int destX = 0;
            int destY = 0;

            float nPercent = 0;
            float nPercentW = 0;
            float nPercentH = 0;

            nPercentW = ((float)Width / (float)sourceWidth);
            nPercentH = ((float)Height / (float)sourceHeight);

            //if we have to pad the height pad both the top and the bottom
            //with the difference between the scaled height and the desired height
            if (nPercentH < nPercentW)
            {
                nPercent = nPercentH;
                destX = (int)((Width - (sourceWidth * nPercent)) / 2);
            }
            else
            {
                nPercent = nPercentW;
                destY = (int)((Height - (sourceHeight * nPercent)) / 2);
            }

            int destWidth = (int)(sourceWidth * nPercent);
            int destHeight = (int)(sourceHeight * nPercent);

            Bitmap bmPhoto = new Bitmap(Width, Height, PixelFormat.Format24bppRgb);
            bmPhoto.SetResolution(imgPhoto.HorizontalResolution, imgPhoto.VerticalResolution);

            Graphics grPhoto = Graphics.FromImage(bmPhoto);
            grPhoto.Clear(Color.Red);
            grPhoto.InterpolationMode = InterpolationMode.HighQualityBicubic;

            grPhoto.DrawImage(imgPhoto,
                new Rectangle(destX, destY, destWidth, destHeight),
                new Rectangle(sourceX, sourceY, sourceWidth, sourceHeight),
                GraphicsUnit.Pixel);

            grPhoto.Dispose();
            return bmPhoto;
        }

        public static Image Crop(Image imgPhoto, int Width, int Height, AnchorPosition Anchor)
        {
            int sourceWidth = imgPhoto.Width;
            int sourceHeight = imgPhoto.Height;
            int sourceX = 0;
            int sourceY = 0;
            int destX = 0;
            int destY = 0;

            float nPercent = 0;
            float nPercentW = 0;
            float nPercentH = 0;

            nPercentW = ((float)Width / (float)sourceWidth);
            nPercentH = ((float)Height / (float)sourceHeight);

            if (nPercentH < nPercentW)
            {
                nPercent = nPercentW;
                switch (Anchor)
                {
                    case AnchorPosition.Top:
                        destY = 0;
                        break;

                    case AnchorPosition.Bottom:
                        destY = (int)(Height - (sourceHeight * nPercent));
                        break;

                    default:
                        destY = (int)((Height - (sourceHeight * nPercent)) / 2);
                        break;
                }
            }
            else
            {
                nPercent = nPercentH;
                switch (Anchor)
                {
                    case AnchorPosition.Left:
                        destX = 0;
                        break;

                    case AnchorPosition.Right:
                        destX = (int)(Width - (sourceWidth * nPercent));
                        break;

                    default:
                        destX = (int)((Width - (sourceWidth * nPercent)) / 2);
                        break;
                }
            }

            int destWidth = (int)(sourceWidth * nPercent);
            int destHeight = (int)(sourceHeight * nPercent);

            Bitmap bmPhoto = new Bitmap(Width, Height, PixelFormat.Format24bppRgb);
            bmPhoto.SetResolution(imgPhoto.HorizontalResolution, imgPhoto.VerticalResolution);

            Graphics grPhoto = Graphics.FromImage(bmPhoto);
            grPhoto.InterpolationMode = InterpolationMode.HighQualityBicubic;

            grPhoto.DrawImage(imgPhoto,
                new Rectangle(destX, destY, destWidth, destHeight),
                new Rectangle(sourceX, sourceY, sourceWidth, sourceHeight),
                GraphicsUnit.Pixel);

            grPhoto.Dispose();
            return bmPhoto;
        }

        public static void CompressImageSize(string imagePath, Image img, int quality)
        {
            EncoderParameter encoderParameter = new EncoderParameter(Encoder.Quality, quality);
            // Use the jpeg image codec
            ImageCodecInfo imageCodecInfo = GetEncoderInfo("image/jpeg");
            EncoderParameters encoderParameters = new EncoderParameters(1);
            encoderParameters.Param[0] = encoderParameter;
            // Compress the size of the image
            img.Save(imagePath, imageCodecInfo, encoderParameters);
        }

        public static ImageCodecInfo GetEncoderInfo(string mimeType)
        {
            // Get image codecs for all image formats
            ImageCodecInfo[] imageCodecInfo = ImageCodecInfo.GetImageEncoders();
            // Find the correct image codec and return the image codec
            // with the given mime type
            for (int i = 0; i < imageCodecInfo.Length; i++)
                if (imageCodecInfo[i].MimeType == mimeType)
                    return imageCodecInfo[i];
            return null;
        }

        public static byte[] ConvertImageQuality(System.Drawing.Image image, int pIntQuality, string savePath)
        {
            byte[] btImage = null;
            // We will store the correct image codec in this object
            ImageCodecInfo iciJpegCodec = null;
            // This will specify the image quality to the encoder
            EncoderParameter epQuality = new EncoderParameter(System.Drawing.Imaging.Encoder.Quality, pIntQuality);
            // Get all image codecs that are available
            ImageCodecInfo[] iciCodecs = ImageCodecInfo.GetImageEncoders();
            // Store the quality parameter in the list of encoder parameters
            EncoderParameters epParameters = new EncoderParameters(1);
            epParameters.Param[0] = epQuality;
            // Loop through all the image codecs
            for (int i = 0; i < iciCodecs.Length; i++)
            {
                // Until the one that we are interested in is found, which is image/jpeg
                if (iciCodecs[i].MimeType == "image/jpeg")
                {
                    iciJpegCodec = iciCodecs[i];
                    break;
                }
            }

            image.Save(savePath, iciJpegCodec, epParameters);
            System.Drawing.Image newImage = System.Drawing.Image.FromFile(savePath);
            btImage = imageToByteArray(newImage);
            return btImage;
        }

        // uses memory stream to save instead of disk
        public static System.Drawing.Image ConvertImageQuality(System.Drawing.Image image, int pIntQuality)
        {
            System.Drawing.Image myImage = image;
            byte[] btImage = null;
            // We will store the correct image codec in this object
            ImageCodecInfo iciJpegCodec = null;
            // This will specify the image quality to the encoder
            EncoderParameter epQuality = new EncoderParameter(System.Drawing.Imaging.Encoder.Quality, pIntQuality);
            // Get all image codecs that are available
            ImageCodecInfo[] iciCodecs = ImageCodecInfo.GetImageEncoders();
            // Store the quality parameter in the list of encoder parameters
            EncoderParameters epParameters = new EncoderParameters(1);
            epParameters.Param[0] = epQuality;
            // Loop through all the image codecs
            for (int i = 0; i < iciCodecs.Length; i++)
            {
                // Until the one that we are interested in is found, which is image/jpeg
                if (iciCodecs[i].MimeType == "image/jpeg")
                {
                    iciJpegCodec = iciCodecs[i];
                    break;
                }
            }

            using (MemoryStream m = new MemoryStream())
            {
                image.Save(m, iciJpegCodec, epParameters);
                btImage = m.ToArray();
            }

            //myImage = byteArrayToImage(btImage);
            MemoryStream ms = new MemoryStream(btImage);
            myImage = Image.FromStream(ms);
            return myImage;
        }

        // uses memory stream to save instead of disk
        public static byte[] ConvertImageQuality2(System.Drawing.Image image, int pIntQuality)
        {
            System.Drawing.Image myImage = image;
            byte[] btImage = null;
            // We will store the correct image codec in this object
            ImageCodecInfo iciJpegCodec = null;
            // This will specify the image quality to the encoder
            EncoderParameter epQuality = new EncoderParameter(System.Drawing.Imaging.Encoder.Quality, pIntQuality);
            // Get all image codecs that are available
            ImageCodecInfo[] iciCodecs = ImageCodecInfo.GetImageEncoders();
            // Store the quality parameter in the list of encoder parameters
            EncoderParameters epParameters = new EncoderParameters(1);
            epParameters.Param[0] = epQuality;
            // Loop through all the image codecs
            for (int i = 0; i < iciCodecs.Length; i++)
            {
                // Until the one that we are interested in is found, which is image/jpeg
                if (iciCodecs[i].MimeType == "image/jpeg")
                {
                    iciJpegCodec = iciCodecs[i];
                    break;
                }
            }

            using (MemoryStream m = new MemoryStream())
            {
                image.Save(m, iciJpegCodec, epParameters);
                btImage = m.ToArray();
            }

            return btImage;
        }

        // this works
        public static System.Drawing.Image resizeImage(System.Drawing.Image image, Size size,
            bool preserveAspectRatio = true)
        {
            int newWidth;
            int newHeight;
            if (preserveAspectRatio)
            {
                int originalWidth = image.Width;
                int originalHeight = image.Height;
                float percentWidth = (float)size.Width / (float)originalWidth;
                float percentHeight = (float)size.Height / (float)originalHeight;
                float percent = percentHeight < percentWidth ? percentHeight : percentWidth;
                newWidth = (int)(originalWidth * percent);
                newHeight = (int)(originalHeight * percent);
            }
            else
            {
                newWidth = size.Width;
                newHeight = size.Height;
            }
            Image newImage = new Bitmap(newWidth, newHeight);
            using (Graphics graphicsHandle = Graphics.FromImage(newImage))
            {
                graphicsHandle.InterpolationMode = InterpolationMode.HighQualityBicubic;
                graphicsHandle.DrawImage(image, 0, 0, newWidth, newHeight);
            }
            return newImage;
        }

        public static byte[] ConvertJPEGImageQuality(byte[] btImage, int pIntQuality)
        {
            byte[] btImageOut = null;
            Bitmap loadedBitmapImage = null; ;
            using (MemoryStream ms = new MemoryStream(btImage))
            {
                Bitmap image = new Bitmap(ms);
                loadedBitmapImage = image;
            }
            using (MemoryStream outStream = SaveImage(loadedBitmapImage, loadedBitmapImage.Width, loadedBitmapImage.Height, pIntQuality))
            {
                btImageOut = outStream.ToArray();
            }
            return btImageOut;
        }

        public static byte[] ConvertJPEGImageQuality(byte[] btImage, int pIntQuality, int pIntImageWidth, int pIntImageHeight)
        {
            byte[] btImageOut = null;
            Bitmap loadedBitmapImage = null; ;
            using (MemoryStream ms = new MemoryStream(btImage))
            using (Bitmap image = new Bitmap(ms))
            {
                //Bitmap image = new Bitmap(ms);
                loadedBitmapImage = image;
            }
            using (MemoryStream outStream = SaveImage(loadedBitmapImage, loadedBitmapImage.Width, loadedBitmapImage.Height, pIntQuality))
            {
                btImageOut = outStream.ToArray();
            }
            return btImageOut;
        }

        public static Point GetJpegDimension(byte[] JpegByteArray)
        {
            var point = new Point();
            using (MemoryStream ms = new MemoryStream(JpegByteArray))
            {
                Bitmap image = new Bitmap(ms);
                point.X = image.Width;
                point.Y = image.Height;
            }

            return point;
        }

        /// <summary>
        /// Saves the image.
        /// </summary>
        /// <param name="image">The image.</param>
        /// <param name="maxWidth">The maximum width.</param>
        /// <param name="maxHeight">The maximum height.</param>
        /// <param name="quality">The image quality.</param>
        /// <returns>MemoryStream.</returns>
        public static MemoryStream SaveImage(Bitmap image, int maxWidth, int maxHeight, int quality)
        {
            // Get the image's original width and height
            int originalWidth = image.Width;
            int originalHeight = image.Height;

            // To preserve the aspect ratio
            float ratioX = (float)maxWidth / (float)originalWidth;
            float ratioY = (float)maxHeight / (float)originalHeight;
            float ratio = Math.Min(ratioX, ratioY);

            // New width and height based on aspect ratio
            int newWidth = (int)(originalWidth * ratio);
            int newHeight = (int)(originalHeight * ratio);

            // Convert other formats (including CMYK) to RGB.
            Bitmap newImage = new Bitmap(newWidth, newHeight, PixelFormat.Format24bppRgb);
            // Draws the image in the specified size with quality mode set to HighQuality
            using (Graphics graphics = Graphics.FromImage(newImage))
            {
                graphics.CompositingQuality = CompositingQuality.HighQuality;
                graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                graphics.SmoothingMode = SmoothingMode.HighQuality;
                graphics.DrawImage(image, 0, 0, newWidth, newHeight);
            }

            // Get an ImageCodecInfo object that represents the JPEG codec.
            ImageCodecInfo imageCodecInfo = GetEncoderInfo(ImageFormat.Jpeg);

            // Create an Encoder object for the Quality parameter.
            Encoder encoder = Encoder.Quality;

            // Create an EncoderParameters object.
            EncoderParameters encoderParameters = new EncoderParameters(1);

            // Save the image as a JPEG file with quality level.
            EncoderParameter encoderParameter = new EncoderParameter(encoder, quality);
            encoderParameters.Param[0] = encoderParameter;
            MemoryStream imageStream = new MemoryStream();

            newImage.Save(imageStream, imageCodecInfo, encoderParameters);

            return imageStream;
        }

        /// <summary>
        /// Method to get encoder infor for given image format.
        /// </summary>
        /// <param name="format">Image format</param>
        /// <returns>image codec info.</returns>
        private static ImageCodecInfo GetEncoderInfo(ImageFormat format)
        {
            return ImageCodecInfo.GetImageDecoders().SingleOrDefault(c => c.FormatID == format.Guid);
        }

        public static Point GetJpegDimensionByURL(string pUrl, bool pAddRange = true)
        {
            var point = new Point(0, 0);
            HttpWebRequest httpWebRequest = (HttpWebRequest)HttpWebRequest.Create(pUrl);
            httpWebRequest.Timeout = 30000;
            if (pAddRange) { httpWebRequest.AddRange(0, 250); }
            using (HttpWebResponse httpWebReponse = (HttpWebResponse)httpWebRequest.GetResponse())
            {
                using (Stream stream = httpWebReponse.GetResponseStream())
                {
                    BinaryReader BR = new BinaryReader(stream);
                    ImageHelper IH = new ImageHelper();
                    Size s = new Size();

                    try { s = IH.GetDimensions(BR); }
                    catch
                    {
                        if (pAddRange)
                        {
                            stream.Close();
                            httpWebReponse.Close();
                            return GetJpegDimensionByURL(pUrl, false);
                        }
                        else { throw; }
                    }

                    point.X = s.Width;
                    point.Y = s.Height;
                }
            }

            return point;
        }

        public static void PdfToThumbnail(byte[] btImage)
        {
          
        }

    }

    public class ImageHelper
    {
        private static byte[] Markers = { 0xc0, 0xc1, 0xc2, 0xc3, 0xc5, 0xc6, 0xc7, 0xc9, 0xca, 0xcb, 0xcd, 0xce, 0xcf };

        public Size GetDimensions(BinaryReader binaryReader)
        {
            Dictionary<byte[], Func<BinaryReader, Size>> imageFormatDecoders = new Dictionary<byte[], Func<BinaryReader, Size>>()
            {
                { new byte[]{ 0xff, 0xd8 }, DecodeJfif },
            };

            int maxMagicBytesLength = imageFormatDecoders.Keys.OrderByDescending(x => x.Length).First().Length;

            byte[] magicBytes = new byte[maxMagicBytesLength];

            for (int i = 0; i < maxMagicBytesLength; i += 1)
            {
                magicBytes[i] = binaryReader.ReadByte();

                foreach (var kvPair in imageFormatDecoders)
                {
                    if (StartsWith(magicBytes, kvPair.Key))
                    {
                        return kvPair.Value(binaryReader);
                    }
                }
            }

            return new Size(0, 0);
        }

        private bool StartsWith(byte[] thisBytes, byte[] thatBytes)
        {
            for (int i = 0; i < thatBytes.Length; i += 1)
            {
                if (thisBytes[i] != thatBytes[i])
                {
                    return false;
                }
            }

            return true;
        }

        private short ReadLittleEndianInt16(BinaryReader binaryReader)
        {
            byte[] bytes = new byte[sizeof(short)];
            for (int i = 0; i < sizeof(short); i += 1)
            {
                bytes[sizeof(short) - 1 - i] = binaryReader.ReadByte();
            }

            return BitConverter.ToInt16(bytes, 0);
        }

        private Size DecodeJfif(BinaryReader binaryReader)
        {
            while (binaryReader.ReadByte() == 0xff)
            {
                byte marker = binaryReader.ReadByte();
                short chunkLength = ReadLittleEndianInt16(binaryReader);

                if (Markers.Contains(marker))
                {
                    binaryReader.ReadByte();
                    int height = ReadLittleEndianInt16(binaryReader);
                    int width = ReadLittleEndianInt16(binaryReader);
                    return new Size(width, height);
                }

                if (chunkLength < 0)
                {
                    ushort uchunkLength = (ushort)chunkLength;
                    binaryReader.ReadBytes(uchunkLength - 2);
                }
                else
                {
                    binaryReader.ReadBytes(chunkLength - 2);
                }
            }

            return new Size(0, 0);
        }
    }
}