
namespace Wpm.Web.Services;

public interface IStorageService
{
    Task<string> UploadAsync(Stream stream, string fileName);
}