﻿// Copyright (c) The Perspex Project. All rights reserved.
// Licensed under the MIT license. See licence.md file in the project root for full license information.

using System;
using Perspex.Platform;

namespace Perspex.Media
{
    /// <summary>
    /// Represents the geometry of an ellipse or circle.
    /// </summary>
    public class EllipseGeometry : Geometry
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EllipseGeometry"/> class.
        /// </summary>
        /// <param name="rect">The rectangle that the ellipse should fill.</param>
        public EllipseGeometry(Rect rect)
        {
            IPlatformRenderInterface factory = PerspexLocator.Current.GetService<IPlatformRenderInterface>();
            IStreamGeometryImpl impl = factory.CreateStreamGeometry();

            using (IStreamGeometryContextImpl ctx = impl.Open())
            {
                double controlPointRatio = (Math.Sqrt(2) - 1) * 4 / 3;
                var center = rect.Center;
                var radius = new Vector(rect.Width / 2, rect.Height / 2);

                var x0 = center.X - radius.X;
                var x1 = center.X - (radius.X * controlPointRatio);
                var x2 = center.X;
                var x3 = center.X + (radius.X * controlPointRatio);
                var x4 = center.X + radius.X;

                var y0 = center.Y - radius.Y;
                var y1 = center.Y - (radius.Y * controlPointRatio);
                var y2 = center.Y;
                var y3 = center.Y + (radius.Y * controlPointRatio);
                var y4 = center.Y + radius.Y;

                ctx.BeginFigure(new Point(x2, y0), true);
                ctx.CubicBezierTo(new Point(x3, y0), new Point(x4, y1), new Point(x4, y2));
                ctx.CubicBezierTo(new Point(x4, y3), new Point(x3, y4), new Point(x2, y4));
                ctx.CubicBezierTo(new Point(x1, y4), new Point(x0, y3), new Point(x0, y2));
                ctx.CubicBezierTo(new Point(x0, y1), new Point(x1, y0), new Point(x2, y0));
                ctx.EndFigure(true);
            }

            PlatformImpl = impl;
        }

        /// <inheritdoc/>
        public override Geometry Clone()
        {
            return new EllipseGeometry(Bounds);
        }
    }
}
