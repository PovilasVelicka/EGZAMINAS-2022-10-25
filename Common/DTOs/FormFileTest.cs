using System.Drawing.Imaging;
using System.Drawing;
using Microsoft.AspNetCore.Http;
using System.Runtime.Versioning;

namespace RegistrationSystem.Controllers.DTOs
{
    [SupportedOSPlatform("windows")]
    public class FormFileTest : IFormFile
    {
        public string ContentType => "jpg";

        public string ContentDisposition => "";

        public IHeaderDictionary Headers => throw new NotImplementedException( );

        public long Length => 100;

        public string Name => "test.jpg";

        public string FileName => "test.jpg";

        public void CopyTo (Stream target)
        {         
            var image = new Bitmap(300, 300);
            image.Save(target, ImageFormat.Png);
        }

        public Task CopyToAsync (Stream target, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException( );
        }

        public Stream OpenReadStream ( )
        {
            throw new NotImplementedException( );
        }
    }
}
