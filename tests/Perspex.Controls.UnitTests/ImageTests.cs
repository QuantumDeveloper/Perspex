﻿// Copyright (c) The Perspex Project. All rights reserved.
// Licensed under the MIT license. See licence.md file in the project root for full license information.

using Moq;
using Perspex.Media;
using Perspex.Media.Imaging;
using Xunit;

namespace Perspex.Controls.UnitTests
{
    public class ImageTests
    {
        [Fact]
        public void Measure_Should_Return_Correct_Size_For_No_Stretch()
        {
            var bitmap = Mock.Of<IBitmap>(x => x.PixelWidth == 50 && x.PixelHeight == 100);
            var target = new Image();
            target.Stretch = Stretch.None;
            target.Source = bitmap;

            target.Measure(new Size(50, 50));

            Assert.Equal(new Size(50, 50), target.DesiredSize);
        }

        [Fact]
        public void Measure_Should_Return_Correct_Size_For_Fill_Stretch()
        {
            var bitmap = Mock.Of<IBitmap>(x => x.PixelWidth == 50 && x.PixelHeight == 100);
            var target = new Image();
            target.Stretch = Stretch.Fill;
            target.Source = bitmap;

            target.Measure(new Size(50, 50));

            Assert.Equal(new Size(50, 50), target.DesiredSize);
        }

        [Fact]
        public void Measure_Should_Return_Correct_Size_For_Uniform_Stretch()
        {
            var bitmap = Mock.Of<IBitmap>(x => x.PixelWidth == 50 && x.PixelHeight == 100);
            var target = new Image();
            target.Stretch = Stretch.Uniform;
            target.Source = bitmap;

            target.Measure(new Size(50, 50));

            Assert.Equal(new Size(25, 50), target.DesiredSize);
        }

        [Fact]
        public void Measure_Should_Return_Correct_Size_For_UniformToFill_Stretch()
        {
            var bitmap = Mock.Of<IBitmap>(x => x.PixelWidth == 50 && x.PixelHeight == 100);
            var target = new Image();
            target.Stretch = Stretch.UniformToFill;
            target.Source = bitmap;

            target.Measure(new Size(50, 50));

            Assert.Equal(new Size(50, 50), target.DesiredSize);
        }
    }
}
