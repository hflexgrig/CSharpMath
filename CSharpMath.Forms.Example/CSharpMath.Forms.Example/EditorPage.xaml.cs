using System.Linq;
using SkiaSharp;
using SkiaSharp.Views.Forms;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CSharpMath.Forms.Example {
  using System;
  using System.Diagnostics;
  using System.IO;
  using System.Xml.Linq;
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
    private int _surfaceWidth;

    public CustomEntry Entry => _entry;
    public EditorView() {
      // Basic functionality
      var view = new SKCanvasView { HeightRequest = 160, BackgroundColor = Color.AliceBlue, EnableTouchEvents = true, HorizontalOptions = LayoutOptions.FillAndExpand, VerticalOptions = LayoutOptions.FillAndExpand };

      var mathToolbar = new MathToolbar();
      //mathToolbar.keyboard
      //var keyb = mathToolbar.Resources["Keyboard"] as CSharpMath.Rendering.MathKeyboard;


      var viewModel = mathToolbar.ViewModel;
      viewModel.BindDisplay(view, new SkiaSharp.MathPainter() {
        TextColor = SKColors.Black
      }, new SKColor(0, 0, 0, 153));

      // Input from physical keyboard
      _entry = new CustomEntry { AutomationId = "CustomEntry", Placeholder = "Enter keystrokes...", Opacity = 0, HeightRequest = 0 };
      view.Touch += (o, e) => {
        //Device.BeginInvokeOnMainThread(() => {
        //Invoke on Main thread, or this won't work
        e.Handled = true;
        _entry.Focus();
        //});

      };

      //view.Unfocused += (o, e) => {
      //  Debug.WriteLine("Unfocused*************************");

      //};

      var boxViewPopup = new Grid { Children = { } };


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
      var stk = new Grid { VerticalOptions = LayoutOptions.Fill };
      stk.Children.Add(view);
      var scv = new ScrollView();
      //scv.WidthRequest = 500;
      scv.Orientation = ScrollOrientation.Both;
      scv.HorizontalOptions = LayoutOptions.Fill;
      scv.VerticalOptions = LayoutOptions.Fill;
      scv.Content = stk;

      // Assemble
      var mainViews = new Grid { Children = { mathToolbar, scrPanelTexts, scv } };

      Grid.SetRow(mathToolbar, 0);
      Grid.SetRow(scrPanelTexts, 1);
      Grid.SetRow(scv, 2);
      mainViews.RowDefinitions.Add(new RowDefinition() { Height = GridLength.Auto });
      mainViews.RowDefinitions.Add(new RowDefinition() { Height = GridLength.Auto });
      mainViews.RowDefinitions.Add(new RowDefinition() { Height = GridLength.Star });
      AbsoluteLayout.SetLayoutFlags(mainViews, AbsoluteLayoutFlags.All);
      AbsoluteLayout.SetLayoutBounds(mainViews, new Rectangle(0, 0, 1, 1));

      var abslayout = new AbsoluteLayout { Children = { _entry, mainViews/*, boxViewPopup */} };
      Content = abslayout;

      //TODO: init layout changes
      mathToolbar.ToolbarButtonClicked += (button, coords) => {
        var subButtonsView = button.Resources["SubButtons"] as Grid;
        if (subButtonsView is null) {
          return;
        }
        boxViewPopup.Children.Clear();
        boxViewPopup.Children.Add(subButtonsView);

        double xOffset;
        double yOffset;

        if (coords.X + 150 > view.Width) {
          xOffset = view.Width - 150;
        } else {
          xOffset = coords.X;
        }

        yOffset = coords.Y + button.Height;
        AbsoluteLayout.SetLayoutBounds(boxViewPopup, new Rectangle(xOffset, yOffset, 150, 150));

        abslayout.Children.Add(boxViewPopup);
      };



      double scale = 1;
      view.PaintSurface += View_PaintSurface;
      viewModel.RedrawRequested += (sender, e) => {
        latex.Text = "LaTeX = " + viewModel.LaTeX;
        ranges.Text = "Ranges = " + string.Join(", ", ((ListDisplay<Fonts, Glyph>)viewModel.Display).Displays.Select(x => x.Range));
        index.Text = "Index = " + viewModel.InsertionIndex;

        _entry.Focus();
        boxViewPopup.Children.Clear();
        abslayout.Children.Remove(boxViewPopup);
      };

      void View_PaintSurface(object sender, SKPaintSurfaceEventArgs e) {
        try {
          var image = e.Surface.Snapshot();
          //ExportSvg(e.Surface, new SKRect(0,0, viewModel.Measure.Width, viewModel.Measure.Height));
          scale = view.Width / e.Info.Width;
          var formulaWidth = viewModel.Measure.Width * scale;
          var gap = 100 * scale;
          if (formulaWidth > scv.Width - gap) {
            view.WidthRequest = formulaWidth + gap;
            //Debug.WriteLine(view.Width + " | " + canvasWidth);
          } else {
            view.WidthRequest = scv.Width;
          }

          var formulaHeight = viewModel.Measure.Height * scale;

          if (formulaHeight > scv.Height - gap) {
            view.HeightRequest = formulaHeight + gap;
            //Debug.WriteLine(view.Width + " | " + canvasWidth);
          } else {
            view.HeightRequest = scv.Height;
          }
        } catch (Exception ex) {

          Debug.WriteLine(ex);
        }

      }
    }

    private static void ExportSvg(MathToolbar mathToolbar) {

     
      using (var stream = new MemoryStream()) {
        // draw the SVG
        using (var skStream = new SKManagedWStream(stream, false))
        using (var writer = new SKXmlStreamWriter(skStream))
        using (var canvas = SKSvgCanvas.Create(SKRect.Create(200, 200), writer)) {
          //var rectPaint = new SKPaint { Color = SKColors.Blue, Style = SKPaintStyle.Fill };
          //canvas.DrawRect(SKRect.Create(50, 70, 100, 30), rectPaint);

          //var circlePaint = new SKPaint { Color = SKColors.Red, Style = SKPaintStyle.Fill };
          //canvas.DrawOval(SKRect.Create(50, 70, 100, 30), circlePaint);
          //canvas.DrawImage(capture, new SKRect(0, 0, capture.Width, capture.Height));
          //canvas.DrawSurface(capture, default);
          //canvas.DrawText();
          var viewModel = mathToolbar.ViewModel;
          viewModel.BindDisplay(new SKCanvasView(), new SkiaSharp.MathPainter() {
            TextColor = SKColors.Black
          }, new SKColor(0, 0, 0, 153));

          skStream.Flush();
        }

        // reset the sream
        stream.Position = 0;

        // read the SVG
        var xdoc = XDocument.Load(stream);
        var svg = xdoc.Root;
      }
    }


  }

}
