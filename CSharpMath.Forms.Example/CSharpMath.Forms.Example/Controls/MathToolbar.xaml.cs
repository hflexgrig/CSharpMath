using System;
using SkiaSharp;
using SkiaSharp.Views.Forms;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CSharpMath.Forms.Example {
  [XamlCompilation(XamlCompilationOptions.Compile)]
  public partial class MathToolbar : ContentView {
    public enum Tab {
      Numbers = 1, Operations, Functions, Letters, LettersCapitals, LargeOperators
    }
    public MathToolbar() {
      InitializeComponent();
    }

    public Rendering.MathKeyboard ViewModel => keyboard;

    public double SelectedBorderWidth { get; set; } = 3;
    public Color SelectedBorderColor { get; set; } = Color.Orange;
   
  }
}