﻿using System;
using System.Collections.Generic;
using System.Text;

namespace CSharpMath.Interfaces {
  /// <summary>Marker interface</summary>
  public interface IMathObject {
  }

  public static class IMathObjectExtensions {
    /// Safe to call, even if one or both are null. Returns true if both are null. 
    public static bool NullCheckingEquals(this IMathObject obj1, IMathObject obj2) =>
      obj1 == null ? obj2 == null ? true : false : obj2 == null ? false : obj1.Equals(obj2);
  }
}
