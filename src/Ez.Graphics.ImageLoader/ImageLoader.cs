using System;
using System.IO;
using System.Runtime.InteropServices;
using Ez.Memory;
using StbImageSharp;

using static StbImageSharp.StbImage;
namespace Ez.Graphics.ImageLoader
{
    public unsafe static class ImageLoader
    {
        public static Image FromStream(Stream stream)
        {
            return Load(stream);
        }

        public static Image FromFile(string file)
        {
            using var stream = File.OpenRead(file);
            return Load(stream);
        }

        private static Image Load(Stream stream)
        {
            byte* ptr = null;
            try
            {
                int width = 0;
                int height = 0;
                int comp = 0;
                var startPos = stream.Position;
                var context = new stbi__context(stream);
                stbi__info_main(context, &width, &height, &comp);
                stream.Position = startPos;

                var required = ColorComponents.Default;
                if (comp == (int)ColorComponents.RedGreenBlue)
                    required = ColorComponents.RedGreenBlueAlpha;

                ptr = load(context, out width, out height, out comp, out var bitsPerChannel, (int)required);

                comp = required != ColorComponents.Default ? (int)required : comp;
                var pixelFormat = GetPixelFormat((ColorComponents)comp, bitsPerChannel);

                var data = new byte[width * height * comp * bitsPerChannel / 8];
                MemUtil.Copy<byte>(data, (IntPtr)ptr);

                return new Image(pixelFormat, (uint)width, (uint)height, 1, 1, 1, TextureType.Texture2D, data);
            }
            finally
            {
                if (ptr != null)
                {
                    Marshal.FreeHGlobal((IntPtr)ptr);
                }
            }
        }

        private static PixelFormat GetPixelFormat(ColorComponents comp, int bitsPerChannel)
        {
            switch (comp)
            {
                case ColorComponents.RedGreenBlueAlpha:
                case ColorComponents.RedGreenBlue:
                    switch (bitsPerChannel)
                    {
                        case 8:
                            return PixelFormat.R8G8B8A8UNorm;
                        case 16:
                            return PixelFormat.R16G16B16A16UNorm;
                        case 32:
                            return PixelFormat.R32G32B32A32UInt;
                        default:
                            throw GetBitsException(bitsPerChannel);
                    }
                case ColorComponents.GreyAlpha:
                    switch (bitsPerChannel)
                    {
                        case 8:
                            return PixelFormat.R8G8UNorm;
                        case 16:
                            return PixelFormat.R16G16UNorm;
                        case 32:
                            return PixelFormat.R32G32UInt;
                        default:
                            throw GetBitsException(bitsPerChannel);
                    }
                case ColorComponents.Grey:
                    switch (bitsPerChannel)
                    {
                        case 8:
                            return PixelFormat.R8UNorm;
                        case 16:
                            return PixelFormat.R16UNorm;
                        case 32:
                            return PixelFormat.R32UInt;
                        default:
                            throw GetBitsException(bitsPerChannel);
                    }
                default:
                    throw new OutOfMemoryException($"The color components {comp} are not supported.");
            }
        }

        private static OutOfMemoryException GetBitsException(int bitsPerChannel) =>
            new OutOfMemoryException($"The image with {bitsPerChannel} are not supported.");

        private unsafe static byte* load(stbi__context s, out int width, out int height, out int comp, out int bitsPerChannel, in int req_comp)
        {
            var w = 0;
            var h = 0;
            var c = 0;
            stbi__result_info stbi__result_info = default;
            void* ptr = stbi__load_main(s, &w, &h, &c, req_comp, &stbi__result_info, 8);
            
            width = w;
            height = h;
            comp = c;
            bitsPerChannel = stbi__result_info.bits_per_channel;

            if (ptr == null)
            {
                return null;
            }

            if (stbi__vertically_flip_on_load != 0)
            {
                int bytes_per_pixel = (req_comp != 0) ? (int)req_comp : c;
                stbi__vertical_flip(ptr, w, h, bytes_per_pixel);
            }

            return (byte*)ptr;
        }
    }
}
