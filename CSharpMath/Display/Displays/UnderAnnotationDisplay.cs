using System.Drawing;
using CSharpMath.Atom;

namespace CSharpMath.Display.Displays;

using FrontEnd;
/// <summary>Creates underanotation display</summary> 
public class UnderAnnotationDisplay<TFont, TGlyph> : IDisplay<TFont, TGlyph>
  where TFont : IFont<TGlyph> {
  public UnderAnnotationDisplay(IDisplay<TFont, TGlyph> inner, IDisplay<TFont, TGlyph>? underList, IGlyphDisplay<TFont, TGlyph> annotationGlyph, PointF position) {
    Inner = inner;
    UnderList = underList;
    AnnotationGlyph = annotationGlyph;
    _annotationGlyphHeight = AnnotationGlyph.Ascent + AnnotationGlyph.Descent;
    Position = position;
  }

  /// <summary>A display representing the inner list of annotation
  /// Its position is relative to the parent. </summary>
  public IDisplay<TFont, TGlyph> Inner { get; }
  public IDisplay<TFont, TGlyph>? UnderList { get; }
  public IGlyphDisplay<TFont, TGlyph> AnnotationGlyph { get; }

  private readonly float _annotationGlyphHeight;

  public float Ascent => System.Math.Max(_annotationGlyphHeight, Inner.Ascent);
  public float Descent => System.Math.Max(_annotationGlyphHeight, Inner.Descent) + _annotationGlyphHeight;
  public float Width => Inner.Width;
  public Range Range => Inner.Range;
  public PointF Position {
    get => Inner.Position;
    set => Inner.Position = value;
  }
  public bool HasScript { get; set; }
  public void Draw(IGraphicsContext<TFont, TGlyph> context) {
    this.DrawBackground(context);
    Inner.Draw(context);
    AnnotationGlyph.Draw(context);
  }
  public Color? TextColor { get; set; }
  public void SetTextColorRecursive(Color? textColor) {
    TextColor ??= textColor;
    Inner.SetTextColorRecursive(textColor);
    AnnotationGlyph?.SetTextColorRecursive(textColor);
  }
  public Color? BackColor { get; set; }
  public override string ToString() => $@"\underannotation{{{Inner}}}";
}