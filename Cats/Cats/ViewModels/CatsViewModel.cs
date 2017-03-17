using System;
using System.ComponentModel;
using System.Collections.ObjectModel;
using Cats.Models;
using Xamarin.Forms;
using System.Threading.Tasks;

namespace Cats.ViewModels
{
	public class CatsViewModel:INotifyPropertyChanged
	{
   		private bool Busy;
		public CatsViewModel()
		{
			Cats = new ObservableCollection<Models.Cat>();
			GetCatsCommand = new Command(
				async () => await GetCats(),
				() => !IsBusy
				);
		}

		public event PropertyChangedEventHandler PropertyChanged;

		private void OnPropertyChanged([System.Runtime.CompilerServices.CallerMemberName] string propertyName = null) =>
				PropertyChanged?.Invoke(this,
					new PropertyChangedEventArgs(propertyName));

		public bool IsBusy
		{
			get
			{
				return Busy;
			}
			set
			{
				Busy = value;
				OnPropertyChanged();
				GetCatsCommand.ChangeCanExecute();
			}
		}


		public ObservableCollection<Cat> Cats { get; set; }


     	async Task GetCats()
		{
        	if(!IsBusy)
			{
				Exception Error = null;
				try
				{
					IsBusy = true;
					var Repository = new Repository();
					var Items = await Repository.GetCats();
					Cats.Clear();
					foreach (var Cat in Items)
					{
						Cats.Add(Cat);
					}
				}
				catch (Exception ex)
				{
					Error = ex;
				}
				finally
				{
					IsBusy = false;
				}
				if (Error != null)
				{
					await Xamarin.Forms.Application.Current.MainPage.DisplayAlert(
						"Error!", Error.Message, "OK");
				}
			}
			return;
		}

		public Command GetCatsCommand { get; set; }


	}
}
