using System;
using NGraphics;

namespace MotionSharp.Views
{
    public abstract class ShapeView : LiteForms.Cocoa.ImageView
    {
        IImageCanvas canvas;

        public ShapeView()
        {
            var size = new Size(1, 1);
            canvas = Platforms.Current.CreateImageCanvas(size, 2);
            Refresh(size);
        }

        internal void RefreshDraw()
        {
            Refresh(canvas.Size);
        }

        protected override void OnChangeFrameSize(LiteForms.Size newSize)
        {
            var size = new Size(newSize.Width, newSize.Height);
            canvas = Platforms.Current.CreateImageCanvas(size,2);
            Refresh(size);
        }

        void Refresh(Size size)
        {
            OnDraw(canvas);
            var image = canvas.GetImage ().GetNSImage ();
            image.Size = new CoreGraphics.CGSize(size.Width, size.Height);
            Image = new LiteForms.Cocoa.Image(image);
        }

        protected abstract void OnDraw(IImageCanvas canvas);

    }
}
