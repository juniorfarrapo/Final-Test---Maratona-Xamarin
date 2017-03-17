using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.MobileServices;
using System.Linq;
namespace Cats.Services
{
	public class AzureService<T>
	{
        IMobileServiceClient Client;
		IMobileServiceTable<T> Table;

		public AzureService()
		{
			string MyAppServiceURL = "http://amazingcat.azurewebsites.net";
			Client = new MobileServiceClient(MyAppServiceURL);
			Table = Client.GetTable<T>();
		}

		public Task<IEnumerable<T>> GetTable()
		{
			return Table.ToEnumerableAsync();
		}
	}
}
