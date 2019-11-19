using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using CSharpMath.Forms.Example.Droid.CustomRenderers;

namespace CSharpMath.Forms.Example.Droid {
  [Activity(Label = "CSharpMath.Forms.Example", Icon = "@mipmap/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
  public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity {
    protected override void OnCreate(Bundle bundle) {
      TabLayoutResource = Resource.Layout.Tabbar;
      ToolbarResource = Resource.Layout.Toolbar;

      base.OnCreate(bundle);

      global::Xamarin.Forms.Forms.Init(this, bundle);
      LoadApplication(new App());
    }

    private bool _lieAboutCurrentFocus;
    public override bool DispatchTouchEvent(MotionEvent ev) {
      var focused = CurrentFocus;
      bool customEntryRendererFocused = focused != null && focused.Parent is CustomEntryRenderer;

      _lieAboutCurrentFocus = customEntryRendererFocused;
      var result = base.DispatchTouchEvent(ev);
      _lieAboutCurrentFocus = false;

      return result;
    }

    public override Android.Views.View CurrentFocus {
      get {
        if (_lieAboutCurrentFocus) {
          return null;
        }

        return base.CurrentFocus;
      }
    }
  }
}

