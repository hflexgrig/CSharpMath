using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CSharpMath.Forms.Example.Controls;
using CSharpMath.Forms.Example.iOS.CustomRenderers;
using Foundation;
using SkiaSharp.Views.iOS;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(CustomGrid), typeof(CustomVisualElementRenderer))]
namespace CSharpMath.Forms.Example.iOS.CustomRenderers {
  public class CustomVisualElementRenderer : VisualElementRenderer<Grid> {

    public override UIView HitTest(CoreGraphics.CGPoint point, UIEvent uievent) {

      UIView view = base.HitTest(point, uievent);

      if (view.GetType() == typeof(UIButton) || view.GetType() == typeof(SKCanvasView) ) {

        CustomEntryRenderer.IsButtonClickedWithVisibleKeyboard = true;

      }

      return view;

    }

  }

}
