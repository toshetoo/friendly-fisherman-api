using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace FriendlyFisherman.SharedKernel.Helpers
{
    public class FileHelper
    {
        public static string LoadTemplate(string emailsTemplatesFolder, string template)
        {
            var file = Path.Combine(emailsTemplatesFolder, template);
            return File.ReadAllText(file);
        }

        public static string BuildFilePath(string directory, string filePath)
        {
            if (!Directory.Exists(directory))
                Directory.CreateDirectory(directory);

            return Path.Combine(directory, filePath);
        }

        public static void CreateFile(string base64String, string filePath)
        {
            byte[] imageBytes = Convert.FromBase64String(base64String);
            File.WriteAllBytes(filePath, imageBytes);
        }

        public static FileStream OpenFile(string filePath)
        {
            if (!File.Exists(filePath))
                return null;

            return File.OpenRead(filePath);
        }
    }

}
