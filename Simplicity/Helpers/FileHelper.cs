using Simplicity.Helpers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Simplicity
{
    public class FileHelper
    {
        public void CreateDirectory(string filepath)
        {

        }

        public string SaveFile(byte[] fileContent, string username, string fileName)
        {
            var mainFolder = Directory.GetCurrentDirectory();
            var userFolder = username;
            var path = Path.Combine(mainFolder, "images", userFolder);

            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            var filePath = Path.Combine(path, fileName);
            File.WriteAllBytes(filePath, fileContent);

            return filePath;
            
        }

        public byte[] GetFile()
        {
            return null;
        }
    }
}
