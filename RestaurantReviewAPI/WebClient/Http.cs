using System;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net;

namespace WebClient
{
	//This class has wrappers around System.Net.Http methods like GetAsync, PostAsync...
	public class Http : IDisposable
	{
			private HttpClient _client;
		    //Http constructor method to initialize class with baseaddress
			public Http(string baseAddress)
			{
				_client = new HttpClient() { BaseAddress = new Uri(baseAddress) };

			}
		    //This method will make Get call using GetAsync method of system HttpClient
			public T Get<T>(string url)
			{
				var message = _client.GetAsync(url).Result;
				return (T)Deserialize<T>(message);

			}
			//This method will make Post call using PostAsync method of system HttpClient
			public T Post<T>(string url, object payload)
			{
				string content = JsonConvert.SerializeObject(payload, Formatting.Indented);
				var message = _client.PostAsync(url, new StringContent(content, Encoding.UTF8, "application/json")).Result;
				return (T)Deserialize<T>(message);

			}
			//This method will make Post call using DeleteAsync method of system HttpClient
			public T Delete<T>(string url, int id)
			{
					
				var message = _client.DeleteAsync(url+"/"+id).Result;
				return (T)Deserialize<T>(message);
			}
		    //This method will deserialize http content to C# objects
			private object Deserialize<T>(HttpResponseMessage message)
			{
				var content = message.Content.ReadAsStringAsync().Result;
				var obj = JsonConvert.DeserializeObject<T>(content);
				return obj;
			}


			public void Dispose()
			{
				_client.Dispose();
			}
		
	}


}
