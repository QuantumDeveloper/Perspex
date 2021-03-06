﻿// Copyright (c) The Perspex Project. All rights reserved.
// Licensed under the MIT license. See licence.md file in the project root for full license information.

using Perspex.Media;

namespace Perspex.Controls
{
    /// <summary>
    /// A control which decorates a child with a border and background.
    /// </summary>
    public class Border : Decorator
    {
        /// <summary>
        /// Defines the <see cref="Background"/> property.
        /// </summary>
        public static readonly StyledProperty<Brush> BackgroundProperty =
            PerspexProperty.Register<Border, Brush>(nameof(Background));

        /// <summary>
        /// Defines the <see cref="BorderBrush"/> property.
        /// </summary>
        public static readonly StyledProperty<Brush> BorderBrushProperty =
            PerspexProperty.Register<Border, Brush>(nameof(BorderBrush));

        /// <summary>
        /// Defines the <see cref="BorderThickness"/> property.
        /// </summary>
        public static readonly StyledProperty<double> BorderThicknessProperty =
            PerspexProperty.Register<Border, double>(nameof(BorderThickness));

        /// <summary>
        /// Defines the <see cref="CornerRadius"/> property.
        /// </summary>
        public static readonly StyledProperty<float> CornerRadiusProperty =
            PerspexProperty.Register<Border, float>(nameof(CornerRadius));

        /// <summary>
        /// Initializes static members of the <see cref="Border"/> class.
        /// </summary>
        static Border()
        {
            AffectsRender(BackgroundProperty, BorderBrushProperty);
        }

        /// <summary>
        /// Gets or sets a brush with which to paint the background.
        /// </summary>
        public Brush Background
        {
            get { return GetValue(BackgroundProperty); }
            set { SetValue(BackgroundProperty, value); }
        }

        /// <summary>
        /// Gets or sets a brush with which to paint the border.
        /// </summary>
        public Brush BorderBrush
        {
            get { return GetValue(BorderBrushProperty); }
            set { SetValue(BorderBrushProperty, value); }
        }

        /// <summary>
        /// Gets or sets the thickness of the border.
        /// </summary>
        public double BorderThickness
        {
            get { return GetValue(BorderThicknessProperty); }
            set { SetValue(BorderThicknessProperty, value); }
        }

        /// <summary>
        /// Gets or sets the radius of the border rounded corners.
        /// </summary>
        public float CornerRadius
        {
            get { return GetValue(CornerRadiusProperty); }
            set { SetValue(CornerRadiusProperty, value); }
        }

        /// <summary>
        /// Renders the control.
        /// </summary>
        /// <param name="context">The drawing context.</param>
        public override void Render(DrawingContext context)
        {
            var background = Background;
            var borderBrush = BorderBrush;
            var borderThickness = BorderThickness;
            var cornerRadius = CornerRadius;
            var rect = new Rect(Bounds.Size).Deflate(BorderThickness);

            if (background != null)
            {
                context.FillRectangle(background, rect, cornerRadius);
            }

            if (borderBrush != null && borderThickness > 0)
            {
                context.DrawRectangle(new Pen(borderBrush, borderThickness), rect, cornerRadius);
            }
        }

        /// <summary>
        /// Measures the control.
        /// </summary>
        /// <param name="availableSize">The available size.</param>
        /// <returns>The desired size of the control.</returns>
        protected override Size MeasureOverride(Size availableSize)
        {
            var child = Child;
            var padding = Padding + new Thickness(BorderThickness);

            if (child != null)
            {
                child.Measure(availableSize.Deflate(padding));
                return child.DesiredSize.Inflate(padding);
            }
            else
            {
                return new Size(padding.Left + padding.Right, padding.Bottom + padding.Top);
            }
        }

        /// <summary>
        /// Arranges the control's child.
        /// </summary>
        /// <param name="finalSize">The size allocated to the control.</param>
        /// <returns>The space taken.</returns>
        protected override Size ArrangeOverride(Size finalSize)
        {
            var child = Child;

            if (child != null)
            {
                var padding = Padding + new Thickness(BorderThickness);
                child.Arrange(new Rect(finalSize).Deflate(padding));
            }

            return finalSize;
        }
    }
}