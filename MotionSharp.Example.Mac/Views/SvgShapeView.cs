using NGraphics;
using System;
using System.Collections.Generic;

namespace MotionSharp.Example.Mac
{
	public class SvgShapeView : ShapeView
	{
		Graphic g;
		string data;

		List<Element> originals = new List<Element>();

		public void Load(string data)
		{
			this.data = data;

			var reader = new SvgReader(data);
			g = reader.Graphic;

			originals.Clear();
			foreach (var item in g.Children)
				originals.Add(item.Clone());

			RefreshDraw();
		}

		protected override void OnChangeFrameSize(LiteForms.Size newSize)
		{
			var deltaX = (newSize.Width == 0 ? g.SampleableBox.Width : newSize.Width) / g.SampleableBox.Width;
			var deltaY = (newSize.Height == 0 ? g.SampleableBox.Height : newSize.Height) / g.SampleableBox.Height;

			g.Children.Clear();
			foreach (var item in originals)
			{
				var cloned = item.Clone();
				if (cloned is NGraphics.Path path)
				{
					foreach (var op in path.Operations)
					{
						if (op is LineTo lineTo)
						{
							lineTo.Point.X *= deltaX;
							lineTo.Point.Y *= deltaY;
						}
						else if (op is MoveTo moveTo)
						{
							moveTo.Point.X *= deltaX;
							moveTo.Point.Y *= deltaY;
						}
					}
				}
				g.Children.Add(cloned);
			}
			base.OnChangeFrameSize(newSize);
		}

		protected virtual void OnSvgDraw (IImageCanvas canvas, Graphic graphic)
		{
			
		}

		protected override void OnDraw(IImageCanvas canvas)
		{
			OnSvgDraw(canvas, g);
			g?.Draw(canvas);
		}
	}
}
