﻿using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace Cats.Views
{
	public partial class CatsPage : ContentPage
	{
		public CatsPage()
		{
			InitializeComponent();
			ListViewCats.ItemSelected += ListViewCats_ItemSelected;
		}

		private async void ListViewCats_ItemSelected(object sender,
		 SelectedItemChangedEventArgs e)
		{
			var SelectedCat = e.SelectedItem as Models.Cat;
			if (SelectedCat != null)
			{
				await Navigation.PushAsync(new Views.DetailsPage(SelectedCat));
				ListViewCats.SelectedItem = null;
			}
		}
	}
}
