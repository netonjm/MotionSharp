using System;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using AppKit;
using LiteForms;
using LiteForms.Cocoa;
using MotionSharp.Cocoa;

namespace MotionSharp.Example.Mac
{
    static class MainClass
    {
        static void AnimationCircle (StackView stackView)
        {
            var circleExample = new CircleView();
            stackView.AddChild(circleExample);
            circleExample.Allocation = new Rectangle(0, 0, 100, 100);

            Task.Run(() =>
            {
                bool positive = true;
                int addition = 5;

                while (true)
                {
                    NSApplication.SharedApplication.InvokeOnMainThread(() =>
                    {
                        if (circleExample.Allocation.Width <= 0)
                        {
                            return;
                        }

                        var circleWidth = circleExample.Width;
                        if (positive)
                        {
                            circleWidth += addition;
                            circleExample.Width = circleWidth;

                            if (circleWidth > 150)
                                positive = false;
                        }
                        else
                        {
                            circleWidth -= addition;
                            circleExample.Width = circleWidth;
                            if (circleWidth < 50)
                                positive = true;
                        }
                    });
                    Thread.Sleep(50);
                }
            });

            var rnd = new Random();

            var button = new Button() { Text = "Press" };
            stackView.AddChild(button);
            button.Clicked += (s, e) => {
                var randomColor = new NGraphics.Color(rnd.NextDouble(), rnd.NextDouble(), rnd.NextDouble(), 1);
                circleExample.Brush = new NGraphics.SolidBrush(randomColor);
            };

        }

        static void Main(string[] args)
        {
            NSApplication.Init();
            NSApplication.SharedApplication.ActivationPolicy = NSApplicationActivationPolicy.Regular;

            var mainWindow = new Window(new Rectangle(0, 0, 540, 800))
            {
                Title = "Test Window",
            };

            var stackView = new StackView() { Orientation = LayoutOrientation.Vertical };
            mainWindow.Content = stackView;

            var path = System.IO.Path.GetDirectoryName(System.IO.Path.GetDirectoryName(Assembly.GetEntryAssembly().Location));
            path = System.IO.Path.Combine(path, "Resources", "mydraw.svg");
            var data = System.IO.File.ReadAllText(path);

            SvgShapeView shapeView = new SvgShapeView();
            shapeView.Allocation = new Rectangle(0, 0, 100, 100);
            stackView.AddChild(shapeView);

            shapeView.Load(data);

            //AnimationCircle(stackView);

            mainWindow.Show();

            //mainWindow.Closing += delegate { NSRunningApplication.CurrentApplication.Terminate(); };
            NSApplication.SharedApplication.ActivateIgnoringOtherApps(true);
            NSApplication.SharedApplication.Run();
        }
    }
}
