using System;
using System.Collections.ObjectModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xamarin.Forms.PlatformConfiguration;
using System.ComponentModel;
using CSharpMath.Forms.Example.Controls;

[assembly: XamlCompilation (XamlCompilationOptions.Compile)]
namespace CSharpMath.Forms.Example
{
	public partial class App : Application
	{
    public event Action OnResumed;
    public event Action OnSleeped;
    public static ObservableCollection<MathView> AllViews = new ObservableCollection<MathView>();
    public App() => InitializeComponent();
    int index = -1;
    void Handle_ChildAdded(object sender, ElementEventArgs e) {
      index++;
      if (Device.RuntimePlatform == Device.iOS && e.Element is Page p && !(p is ExamplesPage)) {
        p.Padding = new Thickness(0, index > 3 ? 90 : 30, 0, 0); //Pages after 4th page have an extra thicc tab bar on iOS
      }
    }

    protected override void OnStart ()
		{
			// Handle when your app starts
		}

		protected override void OnSleep ()
		{
      OnSleeped?.Invoke();
      
			// Handle when your app sleeps
		}

		protected override void OnResume ()
		{
      OnResumed?.Invoke();
            // Handle when your app resumes
    }
	}
}
