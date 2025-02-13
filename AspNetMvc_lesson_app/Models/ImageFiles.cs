namespace AspNetMvc_lesson_app.Models
{
    public class ImageFiles
    {
        public int Id { get; set; }
        public string OriginalFilename { get; set; }
        public string Filename { get; set; }

        public string Src => "/Uploads" + Filename;
    }
}
