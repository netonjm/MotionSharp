using System;
using System.Threading;
using System.Threading.Tasks;
using AppKit;
using LiteForms;
using LiteForms.Cocoa;

namespace MotionSharp.Example.Mac
{
	class StartView : ExampleSvgShapeView
	{
		public StartView() : base("star.svg") { }
	}

	class ArrowView : ExampleSvgShapeView
	{
		public ArrowView() : base("arrow.svg") { }
	}

	class ExampleSvgShapeView : SvgShapeView
	{
		public ExampleSvgShapeView(string file)
		{
			var svgData = FileHelper.GetFileDataFromBundle(file);
			Load(svgData);
		}

		protected override void OnSvgDraw(NGraphics.IImageCanvas canvas, NGraphics.Graphic graphic)
		{

		}
	}

	public class ExampleX
	{
		StartView shapeView;

		public ExampleX(IView content)
		{
			content.BackgroundColor = new Color(0.8f, 0.8f, 0.8f, 1);

			//var stackView = new StackView() { Orientation = LayoutOrientation.Vertical };
			//content.AddChild(stackView);
			//stackView.Allocation = content.Allocation;

			//var button = new Button() { Text = "Press" };
			//stackView.AddChild(button);

			shapeView = new StartView();
			content.AddChild(shapeView);
			shapeView.BackgroundColor = Color.Green;

			CenterView();
			//var shapeView = new CircleView();
			//stackView.AddChild(shapeView);
			//shapeView.Allocation = new Rectangle(0, 0, 100, 100);
			shapeView.ApplyTransform (Transform.FromRotation (2));

			Task.Run(() =>
			{
				while (true)
				{
					NSApplication.SharedApplication.InvokeOnMainThread(() =>
					{
						shapeView.ApplyTransform();
					});
					Thread.Sleep(25);
				}
			});

			//var rnd = new Random();
			//button.Clicked += (s, e) => {
			//	var randomColor = new NGraphics.Color(rnd.NextDouble(), rnd.NextDouble(), rnd.NextDouble(), 1);
			//	//shapeView.Brush = new NGraphics.SolidBrush(randomColor);
			//};
		}

		internal void OnWindowResize(object s, EventArgs e)
		{
			CenterView();
		}

		float ItemSize = 300;

		void CenterView ()
		{
			var parent = shapeView.Parent.Allocation;
			shapeView.Allocation = new Rectangle((parent.Width / 2f) - (ItemSize / 2f), (parent.Width / 2f) - (ItemSize / 2f), ItemSize, ItemSize);
		}
	}

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

			var example = new ExampleX(mainWindow.Content);

			mainWindow.Resize += (s, e) =>
			{
				example.OnWindowResize(s, e);
			};

			//mainWindow.Content = stackView;

			mainWindow.Show();

            //mainWindow.Closing += delegate { NSRunningApplication.CurrentApplication.Terminate(); };
            NSApplication.SharedApplication.ActivateIgnoringOtherApps(true);
            NSApplication.SharedApplication.Run();
        }
    }
}
