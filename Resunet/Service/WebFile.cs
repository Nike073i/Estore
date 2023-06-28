using System.Security.Cryptography;
using System.Text;

namespace Resunet.Service
{
    public class WebFile
    {
        private const string FolderPrefix = "./wwwroot";

        public string GetWebFileName(string fileName)
        {
            var dir = GetWebFileFolder(fileName);
            CreateFolder(FolderPrefix + dir);

            return dir + "/" + Path.GetFileNameWithoutExtension(fileName) + ".jpg";
        }

        public string GetWebFileFolder(string fileName)
        {
            var md5hash = MD5.Create();
            byte[] inputBytes = Encoding.ASCII.GetBytes(fileName);
            byte[] hashBytes = md5hash.ComputeHash(inputBytes);
            string hash = Convert.ToHexString(hashBytes);

            return string.Concat("/images/", hash.AsSpan(0, 2), "/", hash.AsSpan(0, 4));
        }

        public void CreateFolder(string dir)
        {
            if (!Directory.Exists(dir))
                Directory.CreateDirectory(dir);
        }

        public async Task UploadAndResiseImage(Stream fileStream, string fileName, int newWidth, int newHeight)
        {
            using (var image = await Image.LoadAsync(fileStream))
            {
                int aspectWidth = newWidth;
                int aspectHeight = newHeight;
                if (image.Width / (image.Height / (float)newHeight) > newWidth)
                    aspectHeight = (int)(image.Height / (image.Width / (float)newWidth));
                else
                    aspectWidth = (int)(image.Width / (image.Height / (float)newHeight));
                image.Mutate(x => x.Resize(aspectWidth, aspectHeight, KnownResamplers.Lanczos3));
                await image.SaveAsJpegAsync(FolderPrefix + fileName);
            }
        }
    }
}
