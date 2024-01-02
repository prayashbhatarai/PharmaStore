namespace PharmaStore.Modules.Helpers.Interface
{
    public interface IFileHelper
    {
        public string? SaveFile(string path, IFormFile file);
        public bool DeleteFile(string path, string filename);
        public List<string>? SaveFiles(string path, List<IFormFile> files);
        public bool DeleteFiles(string path, List<string> filenames);
    }
}
