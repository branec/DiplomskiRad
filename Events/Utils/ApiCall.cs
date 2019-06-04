using Events.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.ModelBinding;

namespace Events.Utils
{
	public static class ApiCall
	{

		public static string Request(string Address, string Path) {

			using (var client = new HttpClient())
			{
				client.BaseAddress = new Uri("https://api.ipgeolocation.io/");

				//Called Member default GET All records  
				//GetAsync to send a GET request   
				// PutAsync to send a PUT request  
				var responseTask = client.GetAsync("ipgeo?apiKey=25ae91146bd7466f8285c647cf284dfd&ip=46.35.151.83");
				responseTask.Wait();

				//To store result of web api response.   
				var result = responseTask.Result;

				//If success received   
				if (result.IsSuccessStatusCode)
				{
					var readTask = result.Content.ReadAsStringAsync();
					readTask.Wait();

					return readTask.Result;
					
				}
				else
				{
					return "";

				}
			}
		}
	}
}