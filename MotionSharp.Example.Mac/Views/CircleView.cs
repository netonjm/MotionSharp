using NGraphics;

namespace MotionSharp.Cocoa
{
    public class CircleView : ShapeView
    {
        public Pen Pen { get; set; } = Pens.Blue;
        public Brush Brush { get; set; } = Brushes.DarkGray;
        public CircleView()
        {

        }

        protected override void OnDraw(IImageCanvas canvas)
        {
            //canvas.Translate(Allocation.Width/2f, Allocation.Height / 2f);
            canvas.DrawEllipse(
                      new Rect(new Size (canvas.Size.Width - 1, canvas.Size.Height - 1)),
                      pen: Pen,
                      brush: Brush);

        }
    }
}
