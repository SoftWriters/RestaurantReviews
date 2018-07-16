using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace WebClient
{
	class Program
	{
		static void Main(string[] args)
		{
			char ch;
			int number;
			ExecuteMethods exe = new ExecuteMethods();
			do
			{
				Console.WriteLine("Execute API Methods");
				Console.WriteLine("---------------------------------------------------------------------------");
				Console.WriteLine("Enter 1 to get all restaurants.");
				Console.WriteLine("Enter 2 to get restaurant by zipcode.");
				Console.WriteLine("Enter 3 to add a new restaurant.");
				Console.WriteLine("Enter 4 to delete the added restaurant(make sure you add restaurant first).");
				Console.WriteLine("Enter 11 to get all reviews.");
				Console.WriteLine("Enter 12 to get reviews by a user.");
				Console.WriteLine("Enter 13 to add a review.");
				Console.WriteLine("Enter 14 to delete added review(make sure you add review first).");
				Console.WriteLine("----------------------------------------------------------------------------");
				try
				{
					number = Convert.ToInt32(Console.ReadLine());
					switch (number)
					{						
						case 1:						
							exe.GetAllRestaurants();
							break;
						case 2:							
							exe.GetRestaurantByZipCode();
							break;
						case 3:
							exe.AddRestaurant();
							break;
						case 4:
							exe.DeleteRestaurant();
							break;
						case 11:
							exe.GetAllReviews();
							break;
						case 12:
							exe.GetReviewByUser();
							break;
						case 13:
							exe.AddReview();
							break;
						case 14:
							exe.DeleteReview();
							break;
						default:
							break;
						
					}
				}
				catch(Exception e)
				{
					var w = e.InnerException;
				}

				Console.WriteLine("Do you want to continue (Y/N)? ");
				ch = Convert.ToChar(Console.ReadLine().ToUpper());

			} while (ch.Equals(Char.Parse("Y")));




		}

	}    
	}

