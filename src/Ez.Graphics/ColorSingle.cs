// Copyright (c) 2021 ezequias2d <ezequiasmoises@gmail.com> and the Ez contributors
// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.
using Ez.Numerics;
using System;
using System.Drawing;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Ez.Graphics
{
    /// <summary>
    /// A color struct in 32-bits floating-point values in RGBA format.
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public readonly partial struct ColorSingle : IColor<float>, IEquatable<ColorSingle>
    {
        private readonly Vector4 _literal;

        /// <summary>
        /// Creates a new <see cref="ColorSingle"/> struct.
        /// </summary>
        /// <param name="r">The red component.</param>
        /// <param name="g">The green component.</param>
        /// <param name="b">The blue component.</param>
        /// <param name="a">The alpha component.</param>
        public ColorSingle(float r, float g, float b, float a)
        {
            _literal = new Vector4(r, g, b, a);
        }

        /// <summary>
        /// Creates a new <see cref="ColorSingle"/> struct.
        /// </summary>
        /// <param name="channels">The vector containing the color components.</param>
        public ColorSingle(Vector4 channels)
        {
            _literal = channels;
        }

        /// <inheritdoc/>
        public float R => _literal.X;

        /// <inheritdoc/>
        public float G => _literal.Y;

        /// <inheritdoc/>
        public float B => _literal.Z;

        /// <inheritdoc/>
        public float A => _literal.W;

        /// <summary>
        /// Converts this ColorSingle into a Vector4.
        /// </summary>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Vector4 ToVector4()
        {
            return _literal;
        }

        /// <inheritdoc/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool Equals(ColorSingle other) =>
            _literal.Equals(other._literal);

        /// <inheritdoc/>
        public override bool Equals(object obj) =>
            obj is ColorSingle other && Equals(other);

        /// <inheritdoc/>
        public bool Equals(IColor other) => Equals(other.GetColorSingle());

        bool IEquatable<IColor<float>>.Equals(IColor<float> other) => Equals(other.GetColorSingle());

        /// <inheritdoc/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override int GetHashCode()
        {
            return HashHelper<ColorSingle>.Combine(R, G, B, A);
        }

        /// <inheritdoc/>
        public override string ToString() =>  string.Format("R:{0}, G:{1}, B:{2}, A:{3}", R, G, B, A);

        /// <inheritdoc/>
        public Color GetColor() => Color.FromArgb(ToByte(A), ToByte(R), ToByte(G), ToByte(B));

        /// <inheritdoc/>
        public ColorSingle GetColorSingle() => this;

        /// <inheritdoc/>
        public ColorByte GetColorByte() => new(ToByte(R), ToByte(G), ToByte(B), ToByte(A));

        /// <inheritdoc/>
        public ColorInt GetColorInt() => new(ToInt(R), ToInt(G), ToInt(B), ToInt(A));

        /// <inheritdoc/>
        public ColorUInt GetColorUInt() => new (ToUInt(R), ToUInt(G), ToUInt(B), ToUInt(A));
    }
}
