using System;
using System.Collections.Generic;
using System.Text;
using CSharpMath.Enumerations;
using CSharpMath.Interfaces;

namespace CSharpMath.Atoms {
  public class Prime : MathAtom {
    public Prime(int length) : base(MathAtomType.Prime, PrimeOfLength(length)) =>
      Length = length;

    public int Length { get; }

    private static string PrimeOfLength(int length) {
      var sb = new StringBuilder();
      if (length < 0)
        throw new ArgumentException("A negative value was passed.", nameof(length));
      else if (length == 0)
        throw new ArgumentException("A value of zero was passed.", nameof(length));
      Append: switch (length) {
        //glyphs are already superscripted
        //pick appropriate codepoint depending on number of primes
        case 1:
          sb.Append('\u2032');
          break;
        case 2:
          sb.Append('\u2033');
          break;
        case 3:
          sb.Append('\u2034');
          break;
        case 4:
          sb.Append('\u2057');
          break;
        default:
          sb.Append('\u2057');
          length -= 4;
          goto Append;
      }
      return sb.ToString();
    }

    public override int GetHashCode() => unchecked(base.GetHashCode() + 401 * Length);
    public override bool Equals(object obj) =>
      EqualsAtom(obj as Prime) && ((Prime)obj).Length == Length;
  }
}