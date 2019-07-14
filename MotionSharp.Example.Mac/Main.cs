using System;
using System.Threading;
using System.Threading.Tasks;
using AppKit;
using LiteForms;
using LiteForms.Cocoa;

namespace MotionSharp.Example.Mac
{
    static class MainClass
    {
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

            var button = new Button() { Text = "Press" };
            stackView.AddChild(button);

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
                       if (circleExample.Allocation.Width <= 0) {
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
            button.Clicked += (s, e) => {
                var randomColor = new NGraphics.Color(rnd.NextDouble(), rnd.NextDouble(), rnd.NextDouble(), 1);
                circleExample.Brush = new NGraphics.SolidBrush(randomColor);
            };

            mainWindow.Show();

            //mainWindow.Closing += delegate { NSRunningApplication.CurrentApplication.Terminate(); };
            NSApplication.SharedApplication.ActivateIgnoringOtherApps(true);
            NSApplication.SharedApplication.Run();
        }
    }
}
