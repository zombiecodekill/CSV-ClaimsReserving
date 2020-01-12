using System.IO;

namespace ClaimsReserving.Services
{
    public class DirectoryFinder : IDirectoryFinder
    {
        public bool Exists(string path)
        {
            return Directory.Exists(path);
        }
    }
}
