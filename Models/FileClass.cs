namespace JCMdotNet.Models
{
    public class FileClass
    {
        public int FileId { set; get; } = 0;
        public string Name { set; get; } = "";
        public string Path { set; get; } = "";

        public List<FileClass> Files { set; get; } = new List<FileClass>();




    }
}
