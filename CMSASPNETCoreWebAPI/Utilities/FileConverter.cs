using System.Drawing;

namespace CMSASPNETCoreWebAPI.Utilities;

public class FileConverter
{
    public static string ScaleAndConvertImage(IFormFile file, int maxWidth, int maxHeight)
    {
        using var image = Image.FromStream(file.OpenReadStream(), true, true);
        int newWidth, newHeight;
        if (image.Width > image.Height)
        {
            newWidth = maxWidth;
            newHeight = (int)(image.Height * ((float)maxWidth / image.Width));
        }
        else
        {
            newHeight = maxHeight;
            newWidth = (int)(image.Width * ((float)maxHeight / image.Height));
        }
        using var resizedImage = new Bitmap(newWidth, newHeight);
        using (var graphics = Graphics.FromImage(resizedImage))
        {
            graphics.DrawImage(image, 0, 0, newWidth, newHeight);
        }
        using var ms = new MemoryStream();
        resizedImage.Save(ms, image.RawFormat);
        var bytes = ms.ToArray();
        return Convert.ToBase64String(bytes);
    }
}