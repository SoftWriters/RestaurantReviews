using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;


namespace Supr.Test {
	public class ApiClient {
		public static async Task<string> GetAPIToken( string usr, string pwd, string uri ) {
			using ( var client = new HttpClient() ) {
				// setup client
				client.BaseAddress = new Uri( uri );
				client.DefaultRequestHeaders.Accept.Clear();
				client.DefaultRequestHeaders.Accept.Add( new MediaTypeWithQualityHeaderValue( "application/json" ) );

				// setup login data
				FormUrlEncodedContent formContent = new FormUrlEncodedContent( new[] {
					 new KeyValuePair<string, string>("grant_type", "password"),
					 new KeyValuePair<string, string>("username", usr),
					 new KeyValuePair<string, string>("password", pwd),
				} );

				// send request
				HttpResponseMessage responseMessage = await client.PostAsync( "/Token", formContent );

				// get access token from response body
				var responseJson = await responseMessage.Content.ReadAsStringAsync();
				var jObject = JObject.Parse( responseJson );
				return jObject.GetValue( "access_token" ).ToString();
			}
		}

		public static async Task<object> GetRequest( string token, string uri, string path ) {
			using ( var client = new HttpClient() ) {
				// setup client authorization
				client.BaseAddress = new Uri( uri );
				client.DefaultRequestHeaders.Accept.Clear();
				client.DefaultRequestHeaders.Accept.Add( new MediaTypeWithQualityHeaderValue( "application/json" ) );
				client.DefaultRequestHeaders.Add( "Authorization", "Bearer " + token );

				// make request
				HttpResponseMessage response = await client.GetAsync( path );
				var message = await response.Content.ReadAsAsync<object>();
				return message;
			}
		}

		public static async Task<object> PostRequest( string token, string uri, string path, List<KeyValuePair<string, string>> values ) {
			using ( var client = new HttpClient() ) {
				// setup client authorization
				client.BaseAddress = new Uri( uri );
				client.DefaultRequestHeaders.Accept.Clear();
				client.DefaultRequestHeaders.Accept.Add( new MediaTypeWithQualityHeaderValue( "application/json" ) );
				client.DefaultRequestHeaders.Add( "Authorization", "Bearer " + token );
				// setup form data
				FormUrlEncodedContent formContent = new FormUrlEncodedContent( values );

				// make request
				HttpResponseMessage response = await client.PostAsync( path, formContent );
				var message = await response.Content.ReadAsAsync<object>();
				return message;
			}
		}

		public static async Task<object> DeleteRequest( string token, string uri, string path ) {
			using ( var client = new HttpClient() ) {
				// setup client authorization
				client.BaseAddress = new Uri( uri );
				client.DefaultRequestHeaders.Accept.Clear();
				client.DefaultRequestHeaders.Accept.Add( new MediaTypeWithQualityHeaderValue( "application/json" ) );
				client.DefaultRequestHeaders.Add( "Authorization", "Bearer " + token );

				// make request
				HttpResponseMessage response = await client.DeleteAsync( path );
				var message = await response.Content.ReadAsAsync<object>();
				return message;
			}
		}
	}
}


