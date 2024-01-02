using PharmaStore.Modules.Helpers.Interface;

namespace PharmaStore.Modules.Helpers.Implementation
{
    public class FileHelper : IFileHelper
    {
        private readonly IWebHostEnvironment _webHostEnvironment;

        public FileHelper(IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment ?? throw new ArgumentNullException(nameof(webHostEnvironment));
        }

        public string? SaveFile(string path, IFormFile file)
        {
            if (file == null || string.IsNullOrEmpty(path))
            {
                return null;
            }
            try
            {
                string uniqueName = Guid.NewGuid().ToString() + DateTime.Now.ToString("yyyy-MM-dd HH_mm_ss") + file.FileName;
                string serverFolder = Path.Combine(_webHostEnvironment.WebRootPath, Path.Combine(path, uniqueName));
                using (var stream = new FileStream(serverFolder, FileMode.Create))
                {
                    file.CopyTo(stream);
                }
                return uniqueName;
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error saving file: {ex.Message}");
                return null;
            }
        }

        public bool DeleteFile(string path, string filename)
        {
            if (string.IsNullOrEmpty(path) || string.IsNullOrEmpty(filename))
            {
                return false;
            }
            try
            {
                var filePath = Path.Combine(_webHostEnvironment.WebRootPath, path, filename);
                if (File.Exists(filePath))
                {
                    File.Delete(filePath);
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error deleting file: {ex.Message}");
                return false;
            }
        }

        public List<string>? SaveFiles(string path, List<IFormFile> files)
        {
            if (files == null || files.Count == 0 || string.IsNullOrEmpty(path))
            {
                return null;
            }
            var savedFileNames = new List<string>();
            try
            {
                foreach (var file in files)
                {
                    string uniqueName = Guid.NewGuid().ToString() + DateTime.Now.ToString("yyyy-MM-dd HH_mm_ss") + file.FileName;
                    string serverFolder = Path.Combine(_webHostEnvironment.WebRootPath, Path.Combine(path, uniqueName));
                    using (var stream = new FileStream(serverFolder, FileMode.Create))
                    {
                        file.CopyTo(stream);
                    }
                    savedFileNames.Add(uniqueName);
                }
                return savedFileNames;
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error saving files: {ex.Message}");
                return null;
            }
        }

        public bool DeleteFiles(string path, List<string> filenames)
        {
            if (filenames == null || filenames.Count == 0 || string.IsNullOrEmpty(path))
            {
                return false;
            }
            try
            {
                foreach (var filename in filenames)
                {
                    var filePath = Path.Combine(_webHostEnvironment.WebRootPath, path, filename);
                    if (File.Exists(filePath))
                    {
                        File.Delete(filePath);
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error deleting files: {ex.Message}");
                return false;
            }
        }
    }
}
