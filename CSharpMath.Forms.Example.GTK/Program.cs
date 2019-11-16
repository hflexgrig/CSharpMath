using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Platform.GTK;

namespace CSharpMath.Forms.Example.GTK
{
  class MainClass {
    [STAThread]
    public static void Main(string[] args) {
      Gtk.Application.Init();
      Xamarin.Forms.Forms.Init();

      var app = new App();
      var window = new FormsWindow();
      window.LoadApplication(app);
      window.SetApplicationTitle("Game of Life");
      window.Show();

      Gtk.Application.Run();
    }
  }
}
