using System;
using Gtk;

namespace UVAArena
{
	public partial class MainWindow: Gtk.Window
	{
		public MainWindow () : base (Gtk.WindowType.Toplevel)
		{
			Build ();
			this.Maximize ();
		}

		protected void OnDeleteEvent (object sender, DeleteEventArgs a)
		{
			Application.Quit ();
			a.RetVal = true;
		}

		protected void OnCloseActionActivated (object sender, EventArgs e)
		{
			Application.Quit ();
		}  
	}
}