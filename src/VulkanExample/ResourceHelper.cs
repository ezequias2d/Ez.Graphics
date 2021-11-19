using Ez.Graphics;
using Ez.Graphics.StbImageSharp;
using System.IO;

namespace VulkanExample
{
    public static class ResourceHelper
    {
        public static Image LoadTexture(byte[] data)
        {
            using var stream = new MemoryStream(data);
            return LoadTexture(stream);
        }

        public static Image LoadTexture(Stream stream)
        {
            return StbImageLoader.FromStream(stream);
        }
    }
}
