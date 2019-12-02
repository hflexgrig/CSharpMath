using CSharpMath.Enumerations;
using CSharpMath.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace CSharpMath.Atoms {
  public class LargeOperator : MathAtom {

    public IMathList UpperLimit { get; set; }
    public IMathList LowerLimit { get; set; }
    public LargeOperator(string value): base(MathAtomType.LargeOperator, value) {
    }

    public LargeOperator(LargeOperator cloneMe, bool finalize): base(cloneMe, finalize) {
      UpperLimit = AtomCloner.Clone(cloneMe.UpperLimit, finalize);
      LowerLimit = AtomCloner.Clone(cloneMe.LowerLimit, finalize);

    }

    public override string StringValue => base.StringValue;

    public bool EqualsLargeOperator(LargeOperator obj) => EqualsAtom(obj); // Don't care about \limits or \nolimits

    public override T Accept<T, THelper>(IMathAtomVisitor<T, THelper> visitor, THelper helper) => visitor.Visit(this, helper);
  }
}
