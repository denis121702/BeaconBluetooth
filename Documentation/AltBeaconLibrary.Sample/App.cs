using System;
using Xamarin.Forms;

namespace AltBeaconLibrary.Sample
{
    /// <summary>
    /// The app.
    /// </summary>
    public class App : Application
	{
        /// <summary>
        /// Initializes a new instance of the <see cref="App"/> class.
        /// </summary>
        public App()
		{
			this.MainPage = new NavigationPage(new MainPage());            
		}
	}
}