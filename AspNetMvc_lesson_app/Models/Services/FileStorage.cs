//using Microsoft.AspNetCore;

//namespace AspNetMvc_lesson_app.Models.Services
//{
//    public class FileStorage(IWebHostEnvironment environment)
//    {

//        public async Task<ImageFiles> SaveAsync(IFormFile file)
//        {
//            var extensions = Path.GetExtension(file.Name);
//            var filename = Guid.NewGuid().ToString() + extensions;

//            var fullFilename = Path.Combine(environment.WebRootPath, "Uploads", filename.Image.FileName);
//            using var fs = new FileStream(filename, FileMode.Create);
//            await file.CopyToAsync(fs);


//            return new ImageFiles
//            {
//                Filename = filename,
//                OriginalFilename = file.Name
//            };
//        }

//        public void Delete(ImageFiles files)
//        {
//            var fullFilename = Path.Combine(environment.WebRootPath, "Uploads", file.file)
//        }
//    }


//}
