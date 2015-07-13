using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using System.Collections.ObjectModel;

namespace AltBeaconLibrary.Sample
{
	public class MainPage : ContentPage 
	{
		ListView _list;
		BeaconViewModel _viewModel;

		public MainPage()
		{
			BackgroundColor = Color.White;
			Title = "AltBeacon Forms Sample";

			_viewModel = new BeaconViewModel();
			_viewModel.ListChanged += (sender, e) => 
			{
				_list.ItemsSource = _viewModel.Data;
			};

		
			BindingContext = _viewModel;
			this.Content = BuildContent();
		}

		private StackLayout BuildContent()
		{
			var layout = new StackLayout ();

			_list = new ListView {
				//VerticalOptions = LayoutOptions.FillAndExpand,
				ItemTemplate = new DataTemplate(typeof(ListItemView)),
				RowHeight = 90,
			};

			_list.ItemTapped +=	(sender, e) => {
				var todoListPage = new TodoListPage ();
				Navigation.PushAsync (todoListPage);
			};

			_list.ItemSelected += (sender, e) => {
				var todoListPage = new TodoListPage ();
				Navigation.PushAsync (todoListPage);
			};

			_list.SetBinding(ListView.ItemsSourceProperty, "Data");



			layout.Children.Add (_list);
			layout.VerticalOptions = LayoutOptions.FillAndExpand;
			layout.BackgroundColor = Color.Yellow;

			return layout;
		}

		protected override void OnAppearing()
		{
			base.OnAppearing();
			_viewModel.Init();
		}
	}
}