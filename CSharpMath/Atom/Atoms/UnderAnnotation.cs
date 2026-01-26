namespace CSharpMath.Atom.Atoms; 
/// <summary>
/// Abstract name of under annotations implementation \underbrace, \underbracket etc..
/// </summary>
public sealed class UnderAnnotation : MathAtom, IMathListContainer {
  public UnderAnnotation(string value, MathList innerList, MathList? underList) : base(value) {
    InnerList = innerList;
    UnderList = underList;
  }

  public MathList InnerList { get; }
  public MathList? UnderList { get; }

  System.Collections.Generic.IEnumerable<MathList> IMathListContainer.InnerLists =>
    new[] { InnerList };
  public new UnderAnnotation Clone(bool finalize) => (UnderAnnotation)base.Clone(finalize);
  protected override MathAtom CloneInside(bool finalize) =>
    new UnderAnnotation(Nucleus, InnerList.Clone(finalize), UnderList?.Clone(finalize));
  public override bool ScriptsAllowed => true;
  //depending on nucleus, DebugString will change to \underbrace , \underbracket etc..
  public override string DebugString =>
    new System.Text.StringBuilder(@"\underbrace")
    .AppendInBracesOrLiteralNull(InnerList.DebugString)
    .ToString();
  public bool EqualsUnderAnnotation(UnderAnnotation other) =>
    EqualsAtom(other) && InnerList.EqualsList(other.InnerList);
  public override bool Equals(object obj) =>
    obj is UnderAnnotation u ? EqualsUnderAnnotation(u) : false;
  public override int GetHashCode() => (base.GetHashCode(), InnerList).GetHashCode();
}