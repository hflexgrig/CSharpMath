using CSharpMath.Atoms;
using CSharpMath.Display.Text;
using CSharpMath.FrontEnd;
using CSharpMath.Interfaces;
using Color = CSharpMath.Structures.Color;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace CSharpMath.Display {
  public class TextLineDisplay<TFont, TGlyph> : IDisplay<TFont, TGlyph> where TFont: IFont<TGlyph> {
    public TextLineDisplay(
      AttributedString<TFont, TGlyph> text,
      Range range,
      TypesettingContext<TFont, TGlyph> context,
      List<IMathAtom> atoms) : this(
        text.Runs.Select(
          run => new TextRunDisplay<TFont, TGlyph>(run, new Range(range.Location, run.Length), context)
        ).ToList(), atoms) { }
    public TextLineDisplay(List<TextRunDisplay<TFont, TGlyph>> runs, List<IMathAtom> atoms) {
      Runs = runs;
      Atoms = new IMathAtom[atoms.Count];
      atoms.CopyTo(Atoms);
    }
    // We don't implement count as it's not clear if it would refer to runs or atoms.
    public List<TextRunDisplay<TFont, TGlyph>> Runs { get; }
    public IMathAtom[] Atoms { get; }
    public IEnumerable<TGlyph> Text => Runs.SelectMany(run => run.Run.Glyphs);

    public RectangleF DisplayBounds =>
      this.ComputeDisplayBounds();

    public void Draw(IGraphicsContext<TFont, TGlyph> context) {
      context.SaveState();
      context.SetTextPosition(this.Position);
      foreach (var run in Runs) {
        run.Draw(context);
      }
      context.RestoreState();
    }
    public PointF Position { get; set; }

    public float Ascent => Runs.CollectionAscent();
    public float Descent => Runs.CollectionDescent();
    public float Width => Runs.CollectionWidth();
    public Range Range => Range.Combine(Runs.Select(r => r.Range));
    public bool HasScript { get; set; }
    public Color? TextColor { get; set; }

    public void SetTextColorRecursive(Color? textColor) {
      TextColor = TextColor ?? textColor;
      foreach (var run in Runs) {
        run.SetTextColorRecursive(textColor);
      }
    }

    public override string ToString() => string.Concat(Runs);
  }
}

