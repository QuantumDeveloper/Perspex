﻿// Copyright (c) The Perspex Project. All rights reserved.
// Licensed under the MIT license. See licence.md file in the project root for full license information.

using Perspex.VisualTree;

namespace Perspex.Media
{
    /// <summary>
    /// Paints an area with an <see cref="IVisual"/>.
    /// </summary>
    public class VisualBrush : TileBrush
    {
        /// <summary>
        /// Defines the <see cref="Visual"/> property.
        /// </summary>
        public static readonly StyledProperty<IVisual> VisualProperty =
            PerspexProperty.Register<VisualBrush, IVisual>("Visual");

        /// <summary>
        /// Initializes a new instance of the <see cref="VisualBrush"/> class.
        /// </summary>
        public VisualBrush()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="VisualBrush"/> class.
        /// </summary>
        /// <param name="visual">The visual to draw.</param>
        public VisualBrush(IVisual visual)
        {
            Visual = visual;
        }

        /// <summary>
        /// Gets or sets the visual to draw.
        /// </summary>
        public IVisual Visual
        {
            get { return GetValue(VisualProperty); }
            set { SetValue(VisualProperty, value); }
        }
    }
}
