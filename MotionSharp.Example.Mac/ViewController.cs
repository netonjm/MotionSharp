using System;

using AppKit;
using FigmaSharp.Cocoa;
using Foundation;
using NGraphics;

namespace MotionSharp.Example.Mac
{
    public class ShapeView : ImageViewWrapper
    {
        public ShapeView (NSImageView imageView) : base(imageView)
        {
        }
    }

    public partial class ViewController : NSViewController
    {
        public ViewController(IntPtr handle) : base(handle)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            // Do any additional setup after loading the view.

            var canvas = Platforms.Current.CreateImageCanvas(new Size(120 * 5, 120), scale: 2);

            canvas.Translate(20, 20);
            for (var i = 0; i < 5; i++)
            {
                canvas.DrawEllipse(
                    new Rect(new Size(80)),
                    pen: Pens.DarkGray.WithWidth(1 << i),
                    brush: Brushes.LightGray);
                canvas.Translate(120, 0);
            }

            var imgage = canvas.GetImage();
            View = new NSImageView() { Image = imgage.GetNSImage () };
        }

        public override NSObject RepresentedObject
        {
            get
            {
                return base.RepresentedObject;
            }
            set
            {
                base.RepresentedObject = value;
                // Update the view, if already loaded.
            }
        }
    }
}
