using Microsoft.AspNetCore.Http;

namespace Repository.Implementations.ProductRepo
{
    public class ProductImage
    {
        public string SaveImageAsync(IFormFile imageFile)
        {
            string imageName = new String(Path.GetFileNameWithoutExtension(imageFile.FileName).Take(10).ToArray()).Replace(' ', '-');

            imageName = imageName + DateTime.Now.ToString("yymmssfff") + Path.GetExtension(imageFile.FileName);

            var imagePath = Path.Combine("C:/DistributedProject/IlisStoreSln/StoreAdminMVC/wwwroot/", "Images/", imageName);

            var imagePathStore = Path.Combine("C:/DistributedProject/IlisStoreSln/StoreMVC/wwwroot/", "Images/", imageName);


            using (var fileStream = new FileStream(imagePath, FileMode.Create))
            {
                imageFile.CopyTo(fileStream);
            }

            using (var fileStream = new FileStream(imagePathStore, FileMode.Create))
            {
                imageFile.CopyTo(fileStream);
            }


            return imageName;
        }

        public string UpdateImage(IFormFile imageFile, string oldImarePath)
        {
            Parallel.Invoke(
                () =>
                {
                    if (File.Exists(@$"C:/DistributedProject/IlisStoreSln/StoreAdminMVC/wwwroot/Images/{oldImarePath}"))
                    {
                        File.Delete(@$"C:/DistributedProject/IlisStoreSln/StoreAdminMVC/wwwroot/Images/{oldImarePath}");
                    }
                },
                () =>
                {
                    if (File.Exists(@$"C:/DistributedProject/IlisStoreSln/StoreMVC/wwwroot/Images/{oldImarePath}"))
                    {
                        File.Delete(@$"C:/DistributedProject/IlisStoreSln/StoreMVC/wwwroot/Images/{oldImarePath}");
                    }
                });


            string imageName = new String(Path.GetFileNameWithoutExtension(imageFile.FileName).Take(10).ToArray()).Replace(' ', '-');

            imageName = imageName + DateTime.Now.ToString("yymmssfff") + Path.GetExtension(imageFile.FileName);

            var imagePath = Path.Combine("C:/DistributedProject/IlisStoreSln/StoreAdminMVC/wwwroot/", "Images/", imageName);

            var imagePathStore = Path.Combine("C:/DistributedProject/IlisStoreSln/StoreMVC/wwwroot/", "Images/", imageName);


            using (var fileStream = new FileStream(imagePath, FileMode.Create))
            {
                imageFile.CopyTo(fileStream);
            }

            using (var fileStream = new FileStream(imagePathStore, FileMode.Create))
            {
                imageFile.CopyTo(fileStream);
            }


            return imageName;
        }
    }
}
