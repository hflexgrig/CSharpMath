﻿using CSharpMath.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace CSharpMath.Atoms {
  public class AtomCloner: IMathAtomVisitor<IMathAtom, bool> {
    public static AtomCloner Instance { get; } = new AtomCloner();
   public IMathAtom Visit(Accent target, bool finalize)
  => new Accent(target, finalize);
    public IMathAtom Visit(Fraction target, bool finalize)
  => new Fraction(target, finalize);

    public IMathAtom Visit(Inner target, bool finalize)
  => new Inner(target, finalize);
    public IMathAtom Visit(Group group, bool finalize) => new Group(group, finalize);
    public IMathAtom Visit(LargeOperator target, bool finalize)
      => new LargeOperator(target, finalize);
    public IMathAtom Visit(MathAtom target, bool finalize)
      => new MathAtom(target, finalize);

    public IMathAtom Visit(Color target, bool finalize)
      => new Color(target, finalize);
    public IMathAtom Visit(Space target, bool finalize)
      => new Space(target, finalize);

    public IMathAtom Visit(Table target, bool finalize)
      => new Table(target, finalize);

    public IMathAtom Visit(Style target, bool finalize)
      => new Style(target, finalize);

    public IMathAtom Visit(Overline target, bool finalize)
      => new Overline(target, finalize);

    public IMathAtom Visit(Radical target, bool finalize)
      => new Radical(target, finalize);

    public IMathAtom Visit(Underline target, bool finalize)
      => new Underline(target, finalize);
    public static IMathAtom Clone(IMathAtom target, bool finalize) {
      if (target == null) {
        return null;
      }
      return target.Accept(Instance, finalize);
    }
    /// <summary>Deep copy</summary> 
    public static IMathList Clone(IMathList target, bool finalize) {
      if (target == null) {
        return null;
      }
      return new MathList((MathList)target, finalize);
    }

   
  }
}
