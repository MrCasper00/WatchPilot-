using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using WatchPilot.Logic.Interfaces;

namespace WatchPilot.Logic.Logic
{
    public class ImageLogic : IImageLogic
    {



        public async Task<String> UploadImage(byte[] fileData, string fileName)
        {
            DateTime now = DateTime.UtcNow;
            string time = now.ToString("yyyyMMddHHmmss");
            string invalidChars = Regex.Escape(new string(Path.GetInvalidFileNameChars()));
            string invalidRegStr = string.Format(@"([{0}]*\.+$)|([{0}]+)", invalidChars);
            fileName = Regex.Replace(fileName, invalidRegStr, "_");
            fileName = time + fileName;

            string imageDirectory = Path.Combine(Directory.GetCurrentDirectory(), "Images");
            if (!Directory.Exists(imageDirectory))
            {
                Directory.CreateDirectory(imageDirectory);
            }

            string imageFilePath = Path.Combine(imageDirectory, fileName);

            await File.WriteAllBytesAsync(imageFilePath, fileData);

            imageFilePath = imageFilePath.Replace(Directory.GetCurrentDirectory(), "");
             

            return imageFilePath;
        }
    }
}
