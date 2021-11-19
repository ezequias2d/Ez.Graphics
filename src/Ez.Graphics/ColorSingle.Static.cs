// Copyright (c) 2021 ezequias2d <ezequiasmoises@gmail.com> and the Ez contributors
// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.

using Ez.Numerics;
using System;
using System.Numerics;
using System.Runtime.CompilerServices;

namespace Ez.Graphics
{
    /// <summary>
    /// Provides static methods to <see cref="ColorSingle"/> manipulation.
    /// </summary>
    public readonly partial struct ColorSingle
    {
        #region Cast operators
        /// <summary>
        /// Cast a <see cref="ColorSingle"/> to <see cref="Vector4"/>.
        /// </summary>
        /// <param name="color">The color to cast.</param>
        public static implicit operator Vector4(ColorSingle color) => color._literal;

        /// <summary>
        /// Cast a <see cref="Vector4"/> to <see cref="ColorSingle"/>.
        /// </summary>
        /// <param name="color"></param>
        public static implicit operator ColorSingle(Vector4 color) => new(color);
        #endregion
        #region Math
        /// <summary>
        /// Negates the specified color.
        /// </summary>
        /// <param name="color">The color to negate.</param>
        /// <returns>The negated vector.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ColorSingle Negate(ColorSingle color) => -color._literal;

        /// <summary>
        /// Adds two colors together.
        /// </summary>
        /// <param name="left">The first color.</param>
        /// <param name="right">The second color.</param>
        /// <returns>The summed color.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ColorSingle Add(ColorSingle left, ColorSingle right) => left._literal + right._literal;

        /// <summary>
        /// Subtracts the second color from the first.
        /// </summary>
        /// <param name="left">The first color.</param>
        /// <param name="right">The second color.</param>
        /// <returns>The color that results from subracting <paramref name="right"/> from <paramref name="left"/>.</returns>

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ColorSingle Subtract(ColorSingle left, ColorSingle right) => left._literal - right._literal;

        /// <summary>
        /// Multiplies two colors together.
        /// </summary>
        /// <param name="left">The first color.</param>
        /// <param name="right">The second color.</param>
        /// <returns>The product color.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ColorSingle Multiply(ColorSingle left, ColorSingle right) => left._literal * right._literal;

        /// <summary>
        /// Divides the first color by the second.
        /// </summary>
        /// <param name="left">The first color.</param>
        /// <param name="right">The second color.</param>
        /// <returns>The color that results from dividing <paramref name="left"/> by <paramref name="right"/>.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ColorSingle Divide(ColorSingle left, ColorSingle right) => left._literal / right._literal;

        /// <summary>
        /// Multiples the specified color by the specified scalar value.
        /// </summary>
        /// <param name="left">The color.</param>
        /// <param name="right">The scalar value.</param>
        /// <returns>The scaled color.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ColorSingle Multiply(ColorSingle left, float right) => left._literal * right;

        /// <summary>
        /// Divides the specified color by a specified scalar value.
        /// </summary>
        /// <param name="left">The color.</param>
        /// <param name="right">The scalar value.</param>
        /// <returns>The result of the division.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ColorSingle Divide(ColorSingle left, float right) => left._literal / right;

        /// <summary>
        /// Multiples the specified color by the specified scalar value.
        /// </summary>
        /// <param name="left">The scalar value.</param>
        /// <param name="right">The color.</param>
        /// <returns>The scaled color.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ColorSingle Multiply(float left, ColorSingle right) => left * right._literal;

        /// <summary>
        /// Linearly interpolates between two colors.
        /// </summary>
        /// <param name="a">The first color.</param>
        /// <param name="b">The second color.</param>
        /// <param name="t">Influence of the second color on the final result.</param>
        /// <returns><paramref name="a"/> * (1f - <paramref name="t"/>) + <paramref name="b"/> * <paramref name="t"/>.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ColorSingle Lerp(in ColorSingle a, in ColorSingle b, float t) =>
            a + (b - a) * t;
        #endregion
        #region Operators
        /// <summary>
        /// Element-wise equality.
        /// </summary>
        /// <param name="left">The first value.</param>
        /// <param name="right">The second value.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator ==(ColorSingle left, ColorSingle right) =>
            left.Equals(right);

        /// <summary>
        /// Element-wise inequality.
        /// </summary>
        /// <param name="left">The first value.</param>
        /// <param name="right">The second value.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator !=(ColorSingle left, ColorSingle right) =>
            !left.Equals(right);

        /// <summary>
        /// Negates the specified color.
        /// </summary>
        /// <param name="color">The color to negate.</param>
        /// <returns>The negated vector.</returns>
        public static ColorSingle operator -(ColorSingle color) => Negate(color);

        /// <summary>
        /// Adds two colors together.
        /// </summary>
        /// <param name="left">The first color.</param>
        /// <param name="right">The second color.</param>
        /// <returns>The summed color.</returns>
        public static ColorSingle operator +(ColorSingle left, ColorSingle right) => Add(left, right);

        /// <summary>
        /// Subtracts the second color from the first.
        /// </summary>
        /// <param name="left">The first color.</param>
        /// <param name="right">The second color.</param>
        /// <returns>The color that results from subracting <paramref name="right"/> from <paramref name="left"/>.</returns>
        public static ColorSingle operator -(ColorSingle left, ColorSingle right) => Subtract(left, right);

        /// <summary>
        /// Multiplies two colors together.
        /// </summary>
        /// <param name="left">The first color.</param>
        /// <param name="right">The second color.</param>
        /// <returns>The product color.</returns>
        public static ColorSingle operator *(ColorSingle left, ColorSingle right) => Multiply(left, right);

        /// <summary>
        /// Divides the first color by the second.
        /// </summary>
        /// <param name="left">The first color.</param>
        /// <param name="right">The second color.</param>
        /// <returns>The color that results from dividing <paramref name="left"/> by <paramref name="right"/>.</returns>
        public static ColorSingle operator /(ColorSingle left, ColorSingle right) => Divide(left, right);

        /// <summary>
        /// Multiples the specified color by the specified scalar value.
        /// </summary>
        /// <param name="left">The color.</param>
        /// <param name="right">The scalar value.</param>
        /// <returns>The scaled color.</returns>
        public static ColorSingle operator *(ColorSingle left, float right) => Multiply(left, right);

        /// <summary>
        /// Divides the specified color by a specified scalar value.
        /// </summary>
        /// <param name="left">The color.</param>
        /// <param name="right">The scalar value.</param>
        /// <returns>The result of the division.</returns>
        public static ColorSingle operator /(ColorSingle left, float right) => Divide(left, right);

        /// <summary>
        /// Multiples the specified color by the specified scalar value.
        /// </summary>
        /// <param name="left">The scalar value.</param>
        /// <param name="right">The color.</param>
        /// <returns>The scaled color.</returns>
        public static ColorSingle operator *(float left, ColorSingle right) => Multiply(left, right);
        #endregion
        #region Colors
        /// <summary>
        /// Red (1, 0, 0, 1)
        /// </summary>
        public static readonly ColorSingle Red = new(1f, 0f, 0f, 1f);

        /// <summary>
        /// Dark Red (139f / 255f, 0f, 0f, 1f)
        /// </summary>
        public static readonly ColorSingle DarkRed = new(139f / 255f, 0f, 0f, 1f);

        /// <summary>
        /// Green (0f, 1f, 0f, 1f)
        /// </summary>
        public static readonly ColorSingle Green = new(0f, 1f, 0f, 1f);

        /// <summary>
        /// Blue (0f, 0f, 1f, 1f)
        /// </summary>
        public static readonly ColorSingle Blue = new(0f, 0f, 1f, 1f);

        /// <summary>
        /// Yellow (1f, 1f, 0f, 1f)
        /// </summary>
        public static readonly ColorSingle Yellow = new(1f, 1f, 0f, 1f);

        /// <summary>
        /// Grey (128f / 255f, 128f / 255f, 128 / 255f, 1f)
        /// </summary>
        public static readonly ColorSingle Grey = new(128f / 255f, 128f / 255f, 128f / 255f, 1f);

        /// <summary>
        /// Light Grey (211f / 255f, 211f / 255f, 211f / 255f, 1f)
        /// </summary>
        public static readonly ColorSingle LightGrey = new(211f / 255f, 211f / 255f, 211f / 255f, 1f);

        /// <summary>
        /// Cyan (0f, 1f, 1f, 1f)
        /// </summary>
        public static readonly ColorSingle Cyan = new(0f, 1f, 1f, 1f);

        /// <summary>
        /// White (1f, 1f, 1f, 1f)
        /// </summary>
        public static readonly ColorSingle White = new(1f, 1f, 1f, 1f);

        /// <summary>
        /// Cornflower Blue (100f / 255f, 149f / 255f, 237f / 255f, 1f)
        /// </summary>
        public static readonly ColorSingle CornflowerBlue = new(100f / 255f, 149f / 255f, 237f / 255f, 1f);

        /// <summary>
        /// Clear (0f, 0f, 0f, 0f)
        /// </summary>
        public static readonly ColorSingle Clear = new(0f, 0f, 0f, 0f);

        /// <summary>
        /// Black (0f, 0f, 0f, 1f)
        /// </summary>
        public static readonly ColorSingle Black = new(0f, 0f, 0f, 1f);

        /// <summary>
        /// Pink (1f, 192f / 255f, 203f / 255f, 1f)
        /// </summary>
        public static readonly ColorSingle Pink = new(1f, 192f / 255f, 203f / 255f, 1f);

        /// <summary>
        /// Orange (1f, 165f / 255f, 0f, 1f)
        /// </summary>
        public static readonly ColorSingle Orange = new(1f, 165f / 255f, 0f, 1f);
        #endregion

        #region private functions
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static uint ToUInt(in float value)
        {
            const double fromMin = +0d;
            const double fromMax = +1d;
            const double toMin = uint.MinValue;
            const double toMax = uint.MaxValue;
            const double fromMaxAbs = fromMax - fromMin;
            var result = ((toMax - toMin) * ((value - fromMin) / fromMaxAbs)) + toMin;
            return (uint)Math.Clamp(result, toMin, toMax);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static int ToInt(in float value)
        {
            const double fromMin = -1d;
            const double fromMax = +1d;
            const double toMin = int.MinValue;
            const double toMax = int.MaxValue;
            const double fromMaxAbs = fromMax - fromMin;
            var result = ((toMax - toMin) * ((value - fromMin) / fromMaxAbs)) + toMin;
            return (int)Math.Clamp(result, toMin, toMax);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static byte ToByte(in float value)
        {
            const float fromMin = +0f;
            const float fromMax = +1f;
            const float toMin = byte.MinValue;
            const float toMax = byte.MaxValue;
            const float fromMaxAbs = fromMax - fromMin;
            var result = ((toMax - toMin) * ((value - fromMin) / fromMaxAbs)) + toMin;
            return (byte)Math.Clamp(result, toMin, toMax);
        }
        #endregion
    }
}
