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
    private CustomEntry _entry;
    public CustomEntry Entry => _entry;
    public EditorView() {
      // Basic functionality
      var view = new SKCanvasView { HeightRequest = 160, BackgroundColor = Color.AliceBlue, EnableTouchEvents = true , HorizontalOptions = LayoutOptions.FillAndExpand, VerticalOptions = LayoutOptions.FillAndExpand };
      
      var mathToolbar = new MathToolbar();
      //mathToolbar.keyboard
      //var keyb = mathToolbar.Resources["Keyboard"] as CSharpMath.Rendering.MathKeyboard;

      
      var viewModel = mathToolbar.ViewModel;
      viewModel.BindDisplay(view, new SkiaSharp.MathPainter() {
        TextColor = SKColors.Black
      }, new SKColor(0, 0, 0, 153));

      // Input from physical keyboard
      _entry = new CustomEntry {AutomationId = "CustomEntry", Placeholder = "Enter keystrokes...", Opacity = 0, HeightRequest = 0 };
      view.Touch += (o, e) => {
          //Device.BeginInvokeOnMainThread(() => {
          //Invoke on Main thread, or this won't work
          _entry.Focus();
        //});

      };

      var boxViewPopup = new Grid { Children = {  } };


      //if (keyb != null) {
      //  keyb.RedrawRequested += (o, e) => {
      //    //if (!entry.IsFocused) {
      //      entry.Focus();

      //    //}
      //  };
      //}
      _entry.TextChanged += (sender, e) => {
        _entry.Text = "";
        foreach (var c in e.NewTextValue)
          // The (int) extra conversion seems to be required by Android or a crash occurs
          viewModel.KeyPress((Editor.MathKeyboardInput)(int)c);
      };

      _entry.OnBackspace += () => {
        viewModel.KeyPress(Editor.MathKeyboardInput.Backspace);
      };

      _entry.OnShiftLeft += () => {
        viewModel.KeyPress(Editor.MathKeyboardInput.Left);

      };

      _entry.OnShiftRight += () => {
        viewModel.KeyPress(Editor.MathKeyboardInput.Right);

      };

      // Debug labels
      var latex = new Label { Text = "LaTeX = " };
      var ranges = new Label { Text = "Ranges = " };
      var index = new Label { Text = "Index = " };
      

      var stkPanelTexts = new StackLayout { Orientation = StackOrientation.Horizontal };
      stkPanelTexts.Children.Add(latex);
      stkPanelTexts.Children.Add(ranges);
      stkPanelTexts.Children.Add(index);

      var scrPanelTexts = new ScrollView { Orientation = ScrollOrientation.Horizontal };
      scrPanelTexts.Content = stkPanelTexts;
      //var stk = new StackLayout { Orientation = StackOrientation.Horizontal, HorizontalOptions = LayoutOptions.FillAndExpand };
      //stk.Children.Add(view);
      //var scv = new ScrollView();
      //scv.Orientation = ScrollOrientation.Horizontal;
      //scv.HorizontalOptions = LayoutOptions.Start;
      //scv.Content = stk;
      // Assemble
      var mainViews = new Grid { Children = { new StackLayout { Children = { mathToolbar, scrPanelTexts, view, _entry } } } };

      AbsoluteLayout.SetLayoutFlags(mainViews, AbsoluteLayoutFlags.All);
      AbsoluteLayout.SetLayoutBounds(mainViews, new Rectangle(0,0,1,1));

      var abslayout = new AbsoluteLayout { Children = { mainViews/*, boxViewPopup */} };
      Content = abslayout;

      //TODO: init layout changes
      mathToolbar.ToolbarButtonClicked += (button, coords) => {
        var subButtonsView = button.Resources["SubButtons"] as Grid;
        if (subButtonsView is null) {
          return;
        }
        boxViewPopup.Children.Add(subButtonsView);

        double xOffset;
        double yOffset;

        if (coords.X + 150 > view.Width) {
          xOffset = view.Width - 150;
        } else {
          xOffset = coords.X;
        }

        yOffset = coords.Y + button.Height;
        AbsoluteLayout.SetLayoutBounds(boxViewPopup, new Rectangle(xOffset , yOffset, 150, 150));

        abslayout.Children.Add(boxViewPopup);
      };

      viewModel.RedrawRequested += (sender, e) => {
        latex.Text = "LaTeX = " + viewModel.LaTeX;
        ranges.Text = "Ranges = " + string.Join(", ", ((ListDisplay<Fonts, Glyph>)viewModel.Display).Displays.Select(x => x.Range));
        index.Text = "Index = " + viewModel.InsertionIndex;
        _entry.Focus();
        boxViewPopup.Children.Clear();
        abslayout.Children.Remove(boxViewPopup);
      };
      
    }

  }

   

    
  }
