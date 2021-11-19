// Copyright (c) 2021 ezequias2d <ezequiasmoises@gmail.com> and the Ez contributors
// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.
using Ez.Memory;
using Ez.Numerics;
using System;
using System.IO;
using System.Text;

namespace Ez.Graphics
{
    /// <summary>
    /// Represents a texture.
    /// </summary>
    public sealed class Image : IEquatable<Image>
    {
        private readonly byte[] _data;
        private readonly int _length;
        private readonly int _hashcode;

        /// <summary>
        /// Initializes a new instance of <see cref="Image"/> class.
        /// </summary>
        /// <param name="pixelFormat">The pixel format of texture.</param>
        /// <param name="width">The width of texture.</param>
        /// <param name="height">The height of texture.</param>
        /// <param name="depth">The depth of texture.</param>
        /// <param name="mipmapLevels">The number of mipmap levels of texture.</param>
        /// <param name="layers">The number of layers of texture.</param>
        /// <param name="textureType">The texture type of texture.</param>
        /// <param name="data">The pixels data.</param>
        public Image(
            PixelFormat pixelFormat, 
            uint width, 
            uint height, 
            uint depth, 
            uint mipmapLevels, 
            uint layers, 
            TextureType textureType, 
            ReadOnlySpan<byte> data)
        {
            PixelFormat = pixelFormat;
            Width = width;
            Height = height;
            Depth = depth;
            MipmapLevels = mipmapLevels;
            ArrayLayers = layers;
            TextureType = textureType;

            _length = data.Length;
            _data = new byte[data.Length];
            MemUtil.Copy(_data, data);

            _hashcode = HashHelper<Image>.Combine(PixelFormat, Width, Height, Depth, MipmapLevels, ArrayLayers, TextureType, HashHelper<Image>.Combine(Data));
        }

        /// <summary>
        /// The pixel format.
        /// </summary>
        public PixelFormat PixelFormat { get; }
        /// <summary>
        /// Width of the texture, in pixels.
        /// </summary>
        public uint Width { get; }
        /// <summary>
        /// Height of the texture, in pixels.
        /// </summary>
        public uint Height { get; }

        /// <summary>
        /// Depth of the texture, in pixels.
        /// </summary>
        public uint Depth { get; }

        /// <summary>
        /// Number of mipmaps in texture.
        /// </summary>
        public uint MipmapLevels { get; }

        /// <summary>
        /// Number of layers in texture.
        /// </summary>
        public uint ArrayLayers { get; }

        /// <summary>
        /// Texture type of this texture.
        /// </summary>
        public TextureType TextureType { get; }
        
        /// <summary>
        /// Data of texture.
        /// </summary>
        public ReadOnlySpan<byte> Data { get => new(_data, 0, _length); }

        /// <summary>
        /// Returns a value that indicates whether this instance and another <see cref="Image"/> are equal.
        /// </summary>
        /// <param name="other">The other <see cref="Image"/>.</param>
        /// <returns><see langword="true"/> if the two <see cref="Image"/> are equals; otherwise, <see langword="false"/>.</returns>
        public bool Equals(Image other) =>
            ReferenceEquals(this, other) ||
            (PixelFormat == other.PixelFormat &&
                Width == other.Width &&
                Height == other.Height &&
                Data.SequenceEqual(other.Data));

        /// <summary>
        /// Returns a value that indicates whether this instance and a specified object are equal.
        /// </summary>
        /// <param name="obj">The object to compare with the current instance.</param>
        /// <returns><see langword="true"/> if the current instance and <paramref name="obj"/> are equal; otherwise, <see langword="false"/>.</returns>
        public override bool Equals(object obj) =>
            obj is Image texture && Equals(texture);

        /// <summary>
        /// Returns the hash code for this instance.
        /// </summary>
        /// <returns>The hash code.</returns>
        public override int GetHashCode() => _hashcode;

        #region static
        private static readonly ReadOnlyMemory<byte> Signature = new byte[]{ 0x45, 0x5A, 0x5F, 0x49, 0x4D, 0x41, 0x47, 0x45 };

        /// <summary>
        /// Save <paramref name="image"/> in the <paramref name="stream"/> with Ez Image Format.
        /// </summary>
        /// <param name="image">The image to save.</param>
        /// <param name="stream">The destination.</param>
        public static void Save(Image image, Stream stream)
        {
            using var writer = new BinaryWriter(stream, Encoding.UTF8, true);
            writer.Write(Signature.Span);
            writer.Write(image.Width);
            writer.Write(image.Height);
            writer.Write(image.Depth);
            writer.Write(image.MipmapLevels);
            writer.Write(image.ArrayLayers);
            writer.Write((byte)image.TextureType);
            writer.Write((uint)image.PixelFormat);
            writer.Write(image.Data.Length);
            writer.Write(image.Data);          
        }

        /// <summary>
        /// Load a <see cref="Image"/> from a stream with Ez Image format data.
        /// </summary>
        /// <param name="stream">The source stream with a Ez Image format data.</param>
        /// <returns>The image loaded from <paramref name="stream"/>.</returns>
        /// <exception cref="Exception">The <paramref name="stream"/> does not have a valid Ez Image format.</exception>
        public static Image Load(Stream stream)
        {
            using var reader = new BinaryReader(stream, Encoding.UTF8, true);

            Span<byte> buffer = stackalloc byte[Signature.Length];
            var signatureLength = reader.Read(buffer);
            if (!(signatureLength == buffer.Length && MemUtil.Equals(buffer, Signature.Span)))
                throw new OutOfMemoryException("The stream does not have a valid Ez Image format.");

            var width = reader.ReadUInt32();
            var height = reader.ReadUInt32();
            var depth = reader.ReadUInt32();
            var levels = reader.ReadUInt32();
            var layers = reader.ReadUInt32();
            var textureType = (TextureType)reader.ReadByte();
            var format = (PixelFormat)reader.ReadUInt32();
            var length = reader.ReadInt32();
            var data = reader.ReadBytes(length);

            return new(format, width, height, depth, levels, layers, textureType, data);
        }

        #endregion
    }
}
