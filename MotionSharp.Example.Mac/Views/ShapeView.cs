using LiteForms;
using NGraphics;

namespace MotionSharp.Example.Mac
{
	public abstract class ShapeView : LiteForms.Cocoa.ImageView
    {
        IImageCanvas canvas;

        public ShapeView()
        {
            var size = new NGraphics.Size(1, 1);
            canvas = Platforms.Current.CreateImageCanvas(size, scale: 2);
            Refresh(size);
        }

		internal void RefreshDraw()
		{
			Refresh(canvas.Size);
		}

		protected override void OnChangeFrameSize(LiteForms.Size newSize)
        {
            var size = new NGraphics.Size(newSize.Width, newSize.Height);
            canvas = Platforms.Current.CreateImageCanvas(size, scale: 2);
            Refresh(size);
        }

        void Refresh (NGraphics.Size size)
        {
            OnDraw(canvas);

            var imageNative = canvas.GetImage().GetNSImage();
			imageNative.Size = new CoreGraphics.CGSize(size.Width, size.Height);
            Image = new LiteForms.Cocoa.Image(imageNative);
        }

        protected abstract void OnDraw(IImageCanvas canvas);

    }
}
