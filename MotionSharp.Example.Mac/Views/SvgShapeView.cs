using NGraphics;

namespace MotionSharp.Cocoa
{
    class SvgShapeView : ShapeView
    {
        Graphic g;
        public void Load(string data)
        {
            var reader = new SvgReader(data);
            g = reader.Graphic;
            RefreshDraw();
        }

        protected override void OnDraw(IImageCanvas canvas)
        {
            g?.Draw(canvas);
        }
    }
}
