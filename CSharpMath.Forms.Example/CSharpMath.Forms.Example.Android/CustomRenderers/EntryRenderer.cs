using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Text;
using Android.Views;
using Android.Widget;
using CSharpMath.Forms.Example.Controls;
using CSharpMath.Forms.Example.Droid.CustomRenderers;
using Java.Lang;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(CustomEntry), typeof(CustomEntryRenderer))]

namespace CSharpMath.Forms.Example.Droid.CustomRenderers {
  public class CustomEntryRenderer : EntryRenderer {
    public CustomEntryRenderer(Context context) : base(context) {

    }

    //public ICharSequence FilterFormatted(ICharSequence source, int start, int end, ISpanned dest, int dstart, int dend) {
    //  if (string.IsNullOrWhiteSpace(source.ToString())) {
    //    var entry = (CustomEntry)Element;
    //    entry.OnBackspacePressed();
    //  }
    //  return source;
    //}

    protected override void OnElementChanged(ElementChangedEventArgs<Entry> e) {
      base.OnElementChanged(e);


      if (Control == null) {
        return;
      }

      var entry = (CustomEntry)Element;

      var clickTime = DateTime.Now;
      Control.KeyPress += (o,e1) => {
        //if (e1.KeyCode == Keycode.Del) {
        //  var diffTime = DateTime.Now.Subtract(clickTime);

        //  if (diffTime > TimeSpan.FromMilliseconds(200)) {
        //    entry.OnBackspacePressed();
        //  }

        //  clickTime = DateTime.Now;
        //}

        //if (e1.KeyCode == Keycode.ShiftLeft) {
        //  var diffTime = DateTime.Now.Subtract(clickTime);

        //  if (diffTime > TimeSpan.FromMilliseconds(200)) {
        //    entry.OnShiftLeftPressed();
        //  }

        //  clickTime = DateTime.Now;
        //}

        //if (e1.KeyCode == Keycode.ShiftRight) {
        //  var diffTime = DateTime.Now.Subtract(clickTime);

        //  if (diffTime > TimeSpan.FromMilliseconds(200)) {
        //    entry.OnShiftRightPressed();
        //  }

        //  clickTime = DateTime.Now;
        //}
      };

      Control.SetOnKeyListener(new CustomKeyListener(entry));
      //Control.SetFilters(new Android.Text.IInputFilter[] { this });
      

    }



  }

  class CustomKeyListener : Java.Lang.Object, Android.Views.View.IOnKeyListener {
    private readonly CustomEntry _entry;

    const int TimeOut = 50;
    private static DateTime _currentTime = DateTime.Now;
    public CustomKeyListener(CustomEntry entry) {
      this._entry = entry;
    }
    public bool OnKey(Android.Views.View v, [GeneratedEnum] Keycode keyCode, KeyEvent e) {
      //System.Diagnostics.Debug.WriteLine(e.KeyCode);
      if (e.KeyCode == Keycode.Del) {
        var diffTime = DateTime.Now.Subtract(_currentTime);

        if (diffTime > TimeSpan.FromMilliseconds(TimeOut)) {
          _entry.OnBackspacePressed();
        }

        _currentTime = DateTime.Now;
      }else

      if (e.KeyCode == Keycode.DpadRight) {
        //var diffTime = DateTime.Now.Subtract(_currentTime);

        //if (diffTime > TimeSpan.FromMilliseconds(TimeOut)) {
        //  _entry.OnShiftRightPressed();
        //}

        //_currentTime = DateTime.Now;
      }else

      if (e.KeyCode == Keycode.DpadLeft) {
        //var diffTime = DateTime.Now.Subtract(_currentTime);

        //if (diffTime > TimeSpan.FromMilliseconds(TimeOut)) {
        //  _entry.OnShiftLeftPressed();
        //}

        //_currentTime = DateTime.Now;
      }else

      if (e.KeyCode == Keycode.DpadUp) {
        var diffTime = DateTime.Now.Subtract(_currentTime);

        if (diffTime > TimeSpan.FromMilliseconds(TimeOut)) {
          _entry.OnShiftRightPressed();
        }

        _currentTime = DateTime.Now;
      } else

      if (e.KeyCode == Keycode.DpadDown) {
        var diffTime = DateTime.Now.Subtract(_currentTime);

        if (diffTime > TimeSpan.FromMilliseconds(TimeOut)) {
          _entry.OnShiftLeftPressed();
        }

        _currentTime = DateTime.Now;
      } else {
        //Unicode values
        //_entry.OnKeyPressed();
      }

      return true;
    }
  }
}