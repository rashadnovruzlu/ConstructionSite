using Microsoft.AspNetCore.Http;

namespace ConstructionSite.Extensions.Images
{
    public static class IsImageContentType
    {
        public static bool IsImage(this IFormFile file)
        {
            return file.ContentType == "image/jpeg" ||
                   file.ContentType == "image/jpg" ||
                   file.ContentType == "image/png" ||
                   file.ContentType == "image/x-png" ||
                   file.ContentType == "image/gif";
        }
    }
}