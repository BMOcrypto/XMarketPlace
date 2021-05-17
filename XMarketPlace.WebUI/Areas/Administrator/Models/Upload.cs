using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace XMarketPlace.WebUI.Areas.Administrator.Models
{
    public class Upload
    {
        
        public static string ImageUpload(List<IFormFile> files, IHostingEnvironment env, out bool result)
        {
            result = false;
            var uploads = Path.Combine(env.WebRootPath, "uploads"); // Path.Combine ile pathleri birleştirdik. Yani uygulamanızın adı ile resimleri yükleyeceğiniz klasörü birleşitirip tek parça haline getirdik. Örnek; XMarketPlace/XMarketPlace.WebUI/uploads

            foreach (var file in files)
            {
                if (file.ContentType.Contains("image"))
                {
                    if (file.Length <= 2097152) // Resim dosyası 2MB veya daha düşük bir boyutta ise yüklemeye geç
                    {
                        string uniqueName = $"{Guid.NewGuid().ToString().Replace("-", "_").ToLower()}.{file.ContentType.Split('/')[1]}"; // yüklenecek resim için benzersiz bir isim oluşturuldu. file.ContentType.Split('/')[1] ifadesi ile yüklenecek dosyanın uzantısını elde ediyoruz

                        var filePath = Path.Combine(uploads, uniqueName);


                        using (var fileStream = new FileStream(filePath, FileMode.Create))
                        {
                            file.CopyTo(fileStream); 
                            result = true;
                            return filePath.Substring(filePath.IndexOf("\\uploads\\")); 
                        }
                    }
                    else
                    {
                        return "2MB'dan büyük resim dosyası yüklenemez";
                    }
                }
                else
                {
                    return "Lütfen sadece resim dosyası yükleyin";
                }
            }

            return "";
        }
    }
}
