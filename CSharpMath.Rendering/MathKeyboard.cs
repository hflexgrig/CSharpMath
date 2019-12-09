namespace CSharpMath.Rendering {
  using System;
  using System.Drawing;

  using Editor;

  public enum CaretShape {
    IBeam, UpArrow
  }
  public class MathKeyboard : MathKeyboard<Fonts, Glyph> {
    public MathKeyboard(float fontSize = PainterConstants.DefaultFontSize * 3 / 2)
      : base(TypesettingContext.Instance) {
      Font = new Fonts(Array.Empty<Typography.OpenFont.Typeface>(), fontSize);
    }
    
    public void DrawCaret(ICanvas canvas, Structures.Color color, CaretShape shape) {
      if (!(Caret is CaretHandle caret))
        return;
      var cursorPosition = Display.PointForIndex(TypesettingContext.Instance, InsertionIndex) ?? Display.Position;
      cursorPosition.Y *= -1; //inverted canvas, blah blah
      var path = canvas.GetPath();
      path.BeginRead(1);
      path.Foreground = color;
      path.MoveTo(cursorPosition.X, cursorPosition.Y + 25);
      switch (shape) {
        case CaretShape.IBeam:
          ReadOnlySpan<PointF> s = stackalloc PointF[4] {
            new PointF(caret.Width / 16, 0),
            new PointF(caret.Width / 16, -caret.Height),
            new PointF(-caret.Width / 16, -caret.Height),
            new PointF(-caret.Width / 16, 0),
          };
          foreach (var p in s)
            path.LineTo(p.X + cursorPosition.X, p.Y + cursorPosition.Y );
          break;
        case CaretShape.UpArrow:
          s = stackalloc PointF[4] {
            new PointF(caret.Width / 2, caret.Height / 4),
            new PointF(caret.Width / 2, caret.Height),
            new PointF(-caret.Width / 2, caret.Height),
            new PointF(-caret.Width / 2, caret.Height / 4)
          };
          foreach (var p in s)
            path.LineTo(p.X + cursorPosition.X, p.Y + cursorPosition.Y);
          break;
      }
      path.CloseContour();
      path.EndRead();
    }
  }
}