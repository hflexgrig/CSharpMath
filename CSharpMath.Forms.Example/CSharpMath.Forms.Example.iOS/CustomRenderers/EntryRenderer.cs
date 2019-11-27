using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using CSharpMath.Forms.Example.Controls;
using CSharpMath.Forms.Example.iOS.CustomRenderers;
using Foundation;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;


[assembly: ExportRenderer(typeof(CustomEntry), typeof(CustomEntryRenderer))]

namespace CSharpMath.Forms.Example.iOS.CustomRenderers {
  public class CustomEntryRenderer : EntryRenderer {
    public static bool IsButtonClickedWithVisibleKeyboard;

    IElementController ElementController => Element as IElementController;

    protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e) {
      if (e.PropertyName == VisualElement.IsFocusedProperty.PropertyName) {
        if (Control != null) {
          Control.ShouldEndEditing =
              (UITextField textField) => {
                Control.ResignFirstResponder();
                //do here coading first check flag and then fire an event. 

                if (IsButtonClickedWithVisibleKeyboard) {
                  IsButtonClickedWithVisibleKeyboard = false;
                  return false;
                } else {
                  return true;
                }
              };
        }
      }
      base.OnElementPropertyChanged(sender, e);
    }
    protected override void OnElementChanged(ElementChangedEventArgs<Entry> e) {
      base.OnElementChanged(e);
      if (Control == null) {
        return;
        //Control.Background = new SolidColorBrush(Colors.Cyan);
      }

      var entry = (CustomEntry)Element;

      //var textField = new UIBackwardsTextField();
      //textField.BecomeFirstResponder();
      ////textField.EditingChanged += OnEditingChanged;
      //textField.OnDeleteBackward += (sender, a) => {
      //  entry.OnBackspacePressed();
      //};

      //SetNativeControl(textField);



      //var clickTime = DateTime.Now;
      //Control.textch += (o, e1) => {
      //  if (e1.Key == Windows.System.VirtualKey.Back) {
      //    var diffTime = DateTime.Now.Subtract(clickTime);

      //    if (diffTime > TimeSpan.FromMilliseconds(10)) {
      //      entry.OnBackspacePressed();
      //    }

      //    clickTime = DateTime.Now;
      //  }

      //  if (e1.Key == Windows.System.VirtualKey.Left) {
      //    var diffTime = DateTime.Now.Subtract(clickTime);

      //    if (diffTime > TimeSpan.FromMilliseconds(10)) {
      //      entry.OnShiftLeftPressed();
      //    }

      //    clickTime = DateTime.Now;
      //  }

      //  if (e1.Key == Windows.System.VirtualKey.Right) {
      //    var diffTime = DateTime.Now.Subtract(clickTime);

      //    if (diffTime > TimeSpan.FromMilliseconds(10)) {
      //      entry.OnShiftRightPressed();
      //    }

      //    clickTime = DateTime.Now;
      //  }
      //};
    }
  }

  public class UIBackwardsTextField : UITextField {
    // A delegate type for hooking up change notifications.
    public delegate void DeleteBackwardEventHandler(object sender, EventArgs e);

    // An event that clients can use to be notified whenever the
    // elements of the list change.
    public event DeleteBackwardEventHandler OnDeleteBackward;


    public void OnDeleteBackwardPressed() {
      if (OnDeleteBackward != null) {
        OnDeleteBackward(null, null);
      }
    }

    public UIBackwardsTextField() {
      //BorderStyle = UITextBorderStyle.RoundedRect;
      ClipsToBounds = true;
    }

    public override void DeleteBackward() {
      base.DeleteBackward();
      OnDeleteBackwardPressed();
    }

    //public override void 
  }
}