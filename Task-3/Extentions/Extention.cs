using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Task_3.Extentions
{
    public static class Extention
    {
        public static bool CheckTypeImage(this IFormFile file ,string type)
        {
            if(file.ContentType.Contains(type))
            {
                return true;
            }
            return false;
        }
        public static async Task<string>SaveFile(this IFormFile file, string path)
        {
            string fileUrl = Guid.NewGuid().ToString() + file.FileName;
            string filepath = Path.Combine(path, fileUrl);
            using (FileStream fs =new FileStream(filepath,FileMode.Create))
            {
                await file.CopyToAsync(fs);
            }
            return fileUrl;
        }
        public static bool CheckImageLength(this IFormFile file, int mb)
        {
            if (file.Length/1024/1024>=mb)
            {
                return true;
            }
            return false;
        }
        public static void Delete(string path)
        {
            if (File.Exists(path))
            {
                File.Delete(path);
            }
        }
    }
}
