// Copyright (c) 2021 ezequias2d <ezequiasmoises@gmail.com> and the Ez contributors
// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.
using Ez.Memory;
using Ez.Numerics;
using System;
using System.IO;
using System.Text;

namespace Ez.Graphics.Data
{
    /// <summary>
    /// Represents a texture.
    /// </summary>
    public sealed class TextureData : IEquatable<TextureData>
    {
        private readonly byte[] _data;
        private readonly int _length;
        private readonly int _hashcode;

        /// <summary>
        /// Initializes a new instance of <see cref="TextureData"/> class.
        /// </summary>
        /// <param name="pixelFormat">The pixel format of texture.</param>
        /// <param name="width">The width of texture.</param>
        /// <param name="height">The height of texture.</param>
        /// <param name="depth">The depth of texture.</param>
        /// <param name="mipmapLevels">The number of mipmap levels of texture.</param>
        /// <param name="layers">The number of layers of texture.</param>
        /// <param name="textureType">The texture type of texture.</param>
        /// <param name="data">The pixels data.</param>
        public TextureData(
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

            _hashcode = HashHelper<TextureData>.Combine(PixelFormat, Width, Height, Depth, MipmapLevels, ArrayLayers, TextureType, HashHelper<TextureData>.Combine(Data));
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
        /// Returns a value that indicates whether this instance and another <see cref="TextureData"/> are equal.
        /// </summary>
        /// <param name="other">The other <see cref="TextureData"/>.</param>
        /// <returns><see langword="true"/> if the two <see cref="TextureData"/> are equals; otherwise, <see langword="false"/>.</returns>
        public bool Equals(TextureData other) =>
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
            obj is TextureData texture && Equals(texture);

        /// <summary>
        /// Returns the hash code for this instance.
        /// </summary>
        /// <returns>The hash code.</returns>
        public override int GetHashCode() => _hashcode;

        #region static
        private const uint Signature = 0x455A5444;

        /// <summary>
        /// Save a texture data in a stream.
        /// </summary>
        /// <param name="texture">Texture data to save.</param>
        /// <param name="stream">Stream to write.</param>
        public static void Save(TextureData texture, Stream stream)
        {
            using var writer = new BinaryWriter(stream, Encoding.UTF8, true);
            writer.Write(Signature);
            writer.Write(texture.Width);
            writer.Write(texture.Height);
            writer.Write(texture.Depth);
            writer.Write(texture.MipmapLevels);
            writer.Write(texture.ArrayLayers);
            writer.Write((byte)texture.TextureType);
            writer.Write((uint)texture.PixelFormat);
            writer.Write(texture.Data.Length);
            writer.Write(texture.Data);          
        }

        public static TextureData Load(Stream stream)
        {
            using var reader = new BinaryReader(stream, Encoding.UTF8, true);

            if (Signature != reader.ReadUInt32())
                throw new Exception("Stream data is corrupt.");

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
