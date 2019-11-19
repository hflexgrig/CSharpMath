using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CSharpMath.Forms.Example.Controls;
using CSharpMath.Forms.Example.UWP.CustomRenderers;
using Xamarin.Forms;
using Xamarin.Forms.Platform.UWP;

[assembly: ExportRenderer(typeof(CustomEntry), typeof(CustomEntryRenderer))]
namespace CSharpMath.Forms.Example.UWP.CustomRenderers {
  public class CustomEntryRenderer:EntryRenderer {
    protected override void OnElementChanged(ElementChangedEventArgs<Entry> e) {
      base.OnElementChanged(e);

      if (Control == null) {
        return;
        //Control.Background = new SolidColorBrush(Colors.Cyan);
      }

      var entry = (CustomEntry)Element;

      var clickTime = DateTime.Now;
      Control.KeyDown += (o, e1) => {
        if (e1.Key == Windows.System.VirtualKey.Back) {
          var diffTime = DateTime.Now.Subtract(clickTime);

          if (diffTime > TimeSpan.FromMilliseconds(10)) {
            entry.OnBackspacePressed();
          }

          clickTime = DateTime.Now;
        }

        if (e1.Key == Windows.System.VirtualKey.Left) {
          var diffTime = DateTime.Now.Subtract(clickTime);

          if (diffTime > TimeSpan.FromMilliseconds(10)) {
            entry.OnShiftLeftPressed();
          }

          clickTime = DateTime.Now;
        }

        if (e1.Key == Windows.System.VirtualKey.Right) {
          var diffTime = DateTime.Now.Subtract(clickTime);

          if (diffTime > TimeSpan.FromMilliseconds(10)) {
            entry.OnShiftRightPressed();
          }

          clickTime = DateTime.Now;
        }
      };
    }
  }
}
