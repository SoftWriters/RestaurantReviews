using Supr.Model.Entity;
using Supr.Model.Result;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;

namespace Supr.Model.Repository {
	// partial this class so that if it grows unmanageable as a single file it can be logically split into multiple files as necessary
	public partial class Repo : IDisposable {
		// super basic repository pattern - this would continue to evolve as the project gained complexity, but no need to over architect it out of the gate

		#region Setup
		private SuprContext ctx = new SuprContext();
		#endregion

		#region Restaurant Methods
		public ListResult<Restaurant> GetRestaurantsByCity( string city ) {
			ListResult<Restaurant> result = new ListResult<Restaurant>();

			List<Restaurant> list = new List<Restaurant>();
			try {
				list = ctx.Restaurant.Where( r => r.City.ToLower().Contains( city.ToLower() ) ).ToList();

				if ( list.Count > 0 )
					result.LoadList( list );
				else {
					result.Status = "No restaurants found in " + city;
				}
			}
			catch ( Exception except ) {
				result.Status = except.InnerException != null ? except.InnerException.Message : except.Message;
			}

			return result;
		}
		public ListResult<Restaurant> GetRestaurantsByCity( string city, string category ) {
			ListResult<Restaurant> result = new ListResult<Restaurant>();

			List<Restaurant> list = new List<Restaurant>();
			try {
				list = ctx.Restaurant.Where( r => r.City.ToLower().Contains( city.ToLower() ) && r.Category.ToLower() == category.ToLower() ).ToList();

				if ( list.Count > 0 )
					result.LoadList( list );
				else {
					result.Status = "No restaurants found in " + city + " of category " + category;
				}

			}
			catch ( Exception except ) {
				result.Status = except.InnerException != null ? except.InnerException.Message : except.Message;
			}

			return result;
		}
		public ItemResult<Restaurant> GetRestaurantById( int id ) {
			ItemResult<Restaurant> result = new ItemResult<Restaurant>();

			Restaurant item = new Restaurant();
			try {
				item = ctx.Restaurant.Where( r => r.Id == id ).FirstOrDefault();

				if ( item != null )
					result.Item = item;
				else {
					result.Status = "No restaurants found with Id = " + id;
				}
			}
			catch ( Exception except ) {
				result.Status = except.InnerException != null ? except.InnerException.Message : except.Message;
			}

			return result;
		}
		public CrudResult SaveRestaurant( Restaurant restaurant ) {
			CrudResult result = new CrudResult();

			if ( restaurant.Id > 0 )
				ctx.Entry( restaurant ).State = EntityState.Modified;
			else
				ctx.Restaurant.Add( restaurant );

			try {
				ctx.SaveChanges();
				result.Success = true;
			}
			catch ( DbEntityValidationException ex ) {
				result.Errors = ex.EntityValidationErrors;
				result.Success = false;
			}

			return result;
		}
		#endregion

		#region Review Methods
		public ListResult<Review> GetReviewsByUser( string user ) {
			ListResult<Review> result = new ListResult<Review>();

			List<Review> list = new List<Review>();
			try {
				list = ctx.Review.Where( r => r.UserId.ToLower() == user.ToLower() ).ToList();

				if ( list.Count > 0 )
					result.LoadList( list );
				else {
					result.Status = "No reviews found for " + user;
				}
			}
			catch ( Exception except ) {
				result.Status = except.InnerException.Message;
			}

			return result;
		}
		public CrudResult SaveReview( Review review ) {
			CrudResult result = new CrudResult();
			if ( review.Id > 0 )
				ctx.Entry( review ).State = EntityState.Modified;
			else
				ctx.Review.Add( review );

			try {
				ctx.SaveChanges();
				result.Success = true;
			}
			catch ( DbEntityValidationException ex ) {
				result.Errors = ex.EntityValidationErrors;
				result.Success = false;
			}

			return result;
		}
		public CrudResult DeleteReview( int id ) {
			CrudResult result = new CrudResult();
			try {
				Review delete = ctx.Review.Where( r => r.Id == id ).Single();
				ctx.Review.Remove( delete );
				ctx.SaveChanges();
				result.Success = true;
			}
			catch ( DbEntityValidationException ex ) {
				result.Errors = ex.EntityValidationErrors;
				result.Success = false;
			}

			return result;
		}
		#endregion

		#region Tear down
		private bool disposed = false;
		protected virtual void Dispose( bool disposing ) {
			if ( !this.disposed ) {
				if ( disposing ) {
					ctx.Dispose();
				}
			}
			this.disposed = true;
		}
		public void Dispose() {
			Dispose( true );
			GC.SuppressFinalize( this );
		}
		#endregion
	}
}
