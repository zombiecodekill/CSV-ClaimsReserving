namespace ClaimsReserving.Services
{
    public interface IDirectoryFinder
    {
        bool Exists(string path);
    }
}
