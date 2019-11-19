using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace CSharpMath.Forms.Example.Controls {
  public class CustomEntry: Entry {

    public event Action OnBackspace;
    public event Action OnShiftLeft;
    public event Action OnShiftRight;

    public void OnBackspacePressed() {
        OnBackspace?.Invoke();
    }

    public void OnShiftLeftPressed() {
      OnShiftLeft?.Invoke();
    }

    public void OnShiftRightPressed() {
      OnShiftRight?.Invoke();
    }
  }
}
