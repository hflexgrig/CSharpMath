namespace CSharpMath.Editor {
  using System;
  using System.Drawing;
  using System.Linq;

  using Display;
  using Display.Text;
  using FrontEnd;
  using Color = Structures.Color;

  partial class DisplayEditingExtensions {
    public static MathListIndex IndexForPoint<TFont, TGlyph>(this LargeOpLimitsDisplay<TFont, TGlyph> self, TypesettingContext<TFont, TGlyph> context, PointF point) where TFont : IFont<TGlyph> {
      // We can be before or after the radical
      if (point.X < self.Position.X - PixelDelta)
        //We are before the radical, so
        return MathListIndex.Level0Index(self.Range.Location);
      else if (point.X > self.Position.X + self.Width + PixelDelta)
        //We are after the radical
        return MathListIndex.Level0Index(self.Range.End);

      //We can be either near the degree or the radicand
      var lowerLimitRect = self.LowerLimit != null ? new RectangleF(self.LowerLimit.Position, self.LowerLimit.DisplayBounds.Size) : default;
      var upperLimitRect = self.UpperLimit != null ? new RectangleF(self.UpperLimit.Position, self.UpperLimit.DisplayBounds.Size) : default;
      var lowerLimitDistance = DistanceFromPointToRect(point, lowerLimitRect);
      var upperLimitDistance = DistanceFromPointToRect(point, upperLimitRect);
      if (lowerLimitDistance < upperLimitDistance) {
        if (self.LowerLimit != null)
          return MathListIndex.IndexAtLocation(self.Range.Location, MathListSubIndexType.LargeOperatorLowerLimit, self.LowerLimit.IndexForPoint(context, point));
        return MathListIndex.Level0Index(self.Range.Location);
      } else {
        if (self.UpperLimit != null)
          return MathListIndex.IndexAtLocation(self.Range.Location, MathListSubIndexType.LargeOperatorUpperLimit, self.UpperLimit.IndexForPoint(context, point));
        return MathListIndex.Level0Index(self.Range.Location);
      }
    }

    public static PointF? PointForIndex<TFont, TGlyph>(this LargeOpLimitsDisplay<TFont, TGlyph> self, TypesettingContext<TFont, TGlyph> context, MathListIndex index) where TFont : IFont<TGlyph> {
      //if (index.SubIndexType != MathListSubIndexType.Subscript)
      //  throw Arg("The subindex must be none to get the closest point for it.", nameof(index));

      if (index.AtomIndex == self.Range.End)
        // draw a caret after the radical
        return self.Position.Plus(new PointF(self.DisplayBounds.Right, 0));
      // draw a caret before the radical
      return self.Position;
    }

    public static PointF? PointForIndex<TFont, TGlyph>(this GlyphDisplay<TFont, TGlyph> self, TypesettingContext<TFont, TGlyph> context, MathListIndex index) where TFont : IFont<TGlyph> {
      //if (index.SubIndexType != MathListSubIndexType.Subscript)
      //  throw Arg("The subindex must be none to get the closest point for it.", nameof(index));

      if (index.AtomIndex == self.Range.End)
        // draw a caret after the radical
        return self.Position.Plus(new PointF(self.DisplayBounds.Right, 0));
      // draw a caret before the radical
      return self.Position;
    }

    public static void HighlightCharacterAt<TFont, TGlyph>(this LargeOpLimitsDisplay<TFont, TGlyph> self, MathListIndex index, Color color) where TFont : IFont<TGlyph> {
      if (index.SubIndexType != MathListSubIndexType.None)
        throw Arg("The subindex must be none to get the highlight a character in it.", nameof(index));
      self.Highlight(color);
    }

    public static void Highlight<TFont, TGlyph>(this LargeOpLimitsDisplay<TFont, TGlyph> self, Color color) where TFont : IFont<TGlyph> {
#warning Is including Degree intended? It is not present in iosMath
      self.LowerLimit?.Highlight(color);
      self.UpperLimit?.Highlight(color);
    }

    public static IDisplay<TFont, TGlyph> SubListForIndexType<TFont, TGlyph>(this LargeOpLimitsDisplay<TFont, TGlyph> self, MathListSubIndexType type) where TFont : IFont<TGlyph> {
      switch (type) {
        case MathListSubIndexType.LargeOperatorLowerLimit:
          return self.LowerLimit;
        case MathListSubIndexType.LargeOperatorUpperLimit:
          return self.UpperLimit;
        default:
          throw ArgOutOfRange("Subindex type is not a radical subtype.", type, nameof(type));
      }
    }
  }
}
