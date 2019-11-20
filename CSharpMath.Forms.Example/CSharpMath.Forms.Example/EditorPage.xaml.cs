using System.Linq;
using SkiaSharp;
using SkiaSharp.Views.Forms;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CSharpMath.Forms.Example {
  using CSharpMath.Forms.Example.Controls;
  using Display;
  using Rendering;
  [XamlCompilation(XamlCompilationOptions.Compile)]
  public partial class EditorPage : ContentPage {
    public EditorPage() {
      InitializeComponent();
      Content = new EditorView();
    }
  }

  public class EditorView : ContentView {
    public EditorView() {
      // Basic functionality
      var view = new SKCanvasView { HeightRequest = 160, BackgroundColor = Color.AliceBlue, EnableTouchEvents = true , HorizontalOptions = LayoutOptions.FillAndExpand, VerticalOptions = LayoutOptions.FillAndExpand };
      
      var mathToolbar = new MathToolbar();
      //mathToolbar.keyboard
      var keyb = mathToolbar.Resources["Keyboard"] as CSharpMath.Rendering.MathKeyboard;

      
      var viewModel = mathToolbar.ViewModel;
      viewModel.BindDisplay(view, new SkiaSharp.MathPainter() {
        TextColor = SKColors.Black
      }, new SKColor(0, 0, 0, 153));

      // Input from physical keyboard
      var entry = new CustomEntry { Placeholder = "Enter keystrokes...", Opacity = 0, HeightRequest = 0 };
      view.Touch += (o, e) => {
          entry.Focus();

      };



      if (keyb != null) {
        keyb.RedrawRequested += (o, e) => {
          //if (!entry.IsFocused) {
            entry.Focus();

          //}
        };
      }
      entry.TextChanged += (sender, e) => {
        entry.Text = "";
        foreach (var c in e.NewTextValue)
          // The (int) extra conversion seems to be required by Android or a crash occurs
          viewModel.KeyPress((Editor.MathKeyboardInput)(int)c);
      };

      entry.OnBackspace += () => {
        viewModel.KeyPress(Editor.MathKeyboardInput.Backspace);
      };

      entry.OnShiftLeft += () => {
        viewModel.KeyPress(Editor.MathKeyboardInput.Left);

      };

      entry.OnShiftRight += () => {
        viewModel.KeyPress(Editor.MathKeyboardInput.Right);

      };

      // Debug labels
      var latex = new Label { Text = "LaTeX = " };
      var ranges = new Label { Text = "Ranges = " };
      var index = new Label { Text = "Index = " };
      viewModel.RedrawRequested += (sender, e) => {
        latex.Text = "LaTeX = " + viewModel.LaTeX;
        ranges.Text = "Ranges = " + string.Join(", ", ((ListDisplay<Fonts, Glyph>)viewModel.Display).Displays.Select(x => x.Range));
        index.Text = "Index = " + viewModel.InsertionIndex;
      };

     

      //var stk = new StackLayout { Orientation = StackOrientation.Horizontal, HorizontalOptions = LayoutOptions.FillAndExpand };
      //stk.Children.Add(view);
      //var scv = new ScrollView();
      //scv.Orientation = ScrollOrientation.Horizontal;
      //scv.HorizontalOptions = LayoutOptions.Start;
      //scv.Content = stk;
      // Assemble
      Content = new StackLayout { Children = { mathToolbar, latex, ranges, index, view,  entry } };
      entry.Focus();
    }

    
  }
}