using System;
using CSharpMath.Forms.Example.Utils;
using SkiaSharp;
using SkiaSharp.Views.Forms;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CSharpMath.Forms.Example {
  [XamlCompilation(XamlCompilationOptions.Compile)]
  public partial class MathToolbar : ContentView {

    public event Action<Button, (double X, double Y)> ToolbarButtonClicked;
    public enum Tab {
      Numbers = 1, Operations, Functions, Letters, LettersCapitals, LargeOperators
    }
    public MathToolbar() {
      InitializeComponent();
    }

    public Rendering.MathKeyboard ViewModel => keyboard;

    public double SelectedBorderWidth { get; set; } = 3;
    public Color SelectedBorderColor { get; set; } = Color.Orange;

    private void Button_Clicked(object sender, EventArgs e) {
      var coords = VisualTreeHelper.GetScreenCoordinates(sender as VisualElement);
      var newCoords = (coords.X - ToolButtonsScroll.ScrollX, coords.Y);
      ToolbarButtonClicked?.Invoke(sender as Button, newCoords);
    }
  }
}