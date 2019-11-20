using System.Drawing;
using CSharpMath.Rendering;
using CSharpMath.Structures;
using SkiaSharp;
using Color = CSharpMath.Structures.Color;

namespace CSharpMath.SkiaSharp {
  public class MathPainter : MathPainter<SKCanvas, SKColor>, ICanvasPainter<SKCanvas, MathSource, SKColor> {
    public const AntiAlias DefaultAntiAlias = AntiAlias.WithSubpixelText;

    public MathPainter(float fontSize = DefaultFontSize, AntiAlias antiAlias = DefaultAntiAlias) : base(fontSize) =>
      AntiAlias = antiAlias;

    public SKStrokeCap StrokeCap { get; set; }
    public AntiAlias AntiAlias { get; set; }

    public void Draw(SKCanvas canvas, SKPoint point) => Draw(canvas, point.X, point.Y);

    protected override bool CoordinatesFromBottomLeftInsteadOfTopLeft => false;

    public override SKColor UnwrapColor(Color color) => color.ToNative();

    public override Color WrapColor(SKColor color) => color.FromNative();

    public override ICanvas WrapCanvas(SKCanvas canvas) =>
      new SkiaCanvas(canvas, StrokeCap, AntiAlias);

    static readonly MathSource staticValidSource = new MathSource(Atoms.MathLists.WithAtoms());
  
    /// <summary>
    /// Ignores the MathList and LaTeX of the <see cref="MathPainter"/> provided. Repositions the <paramref name="display"/>.
    /// </summary>
    /// <param name="settings"></param>
    /// <param name="display"></param>
    public static void DrawDisplay(MathPainter settings, IDisplay<Fonts, Glyph> display, SKCanvas canvas, PointF position) =>
        DrawDisplay(settings, display, _ => _.Draw(canvas, position));
    /// <summary>
    /// Ignores the MathList and LaTeX of the <see cref="MathPainter"/> provided. Repositions the <paramref name="display"/>.
    /// </summary>
    /// <param name="settings"></param>
    /// <param name="display"></param>
    public static void DrawDisplay(MathPainter settings, IDisplay<Fonts, Glyph> display, SKCanvas canvas, SKPoint position) =>
        DrawDisplay(settings, display, _ => _.Draw(canvas, position));
    /// <summary>
    /// Ignores the MathList and LaTeX of the <see cref="MathPainter"/> provided. Repositions the <paramref name="display"/>.
    /// </summary>
    /// <param name="settings"></param>
    /// <param name="display"></param>
    public static void DrawDisplay(MathPainter settings, IDisplay<Fonts, Glyph> display, SKCanvas canvas, float x, float y) =>
        DrawDisplay(settings, display, _ => _.Draw(canvas, x, y));
    /// <summary>
    /// Ignores the MathList and LaTeX of the <see cref="MathPainter"/> provided. Repositions the <paramref name="display"/>.
    /// </summary>
    /// <param name="settings"></param>
    /// <param name="display"></param>
    public static void DrawDisplay(MathPainter settings, IDisplay<Fonts, Glyph> display, SKCanvas canvas,
      TextAlignment textAlignment = TextAlignment.TopLeft, Thickness padding = default, float offsetX = 0, float offsetY = 0) =>
          DrawDisplay(settings, display, _ => _.Draw(canvas, textAlignment, padding, offsetX, offsetY));
    
    private static void DrawDisplay(MathPainter settings, IDisplay<Fonts, Glyph> display, System.Action<MathPainter> draw) {
      if (display is null) return;
      var original = (settings.Source, settings._display, settings._displayChanged);
      (settings.Source, settings._display, settings._displayChanged) = (staticValidSource, display, false);
      draw(settings);
      (settings.Source, settings._display, settings._displayChanged) = original;
    }
  }
}
