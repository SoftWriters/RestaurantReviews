using System.Collections.Generic;

namespace Supr.Model.Result {

	/// <summary>
	/// ListResult supports a generic list and a message
	/// </summary>
	/// <typeparam name="T">The ObjectType for the generic List collection</typeparam>
	public class ListResult<T> {
		public ListResult() { }

		private List<T> list;
		public List<T> List {
			get {
				return this.list;
			}
		}

		public string Status { get; set; }

		public void LoadList( List<T> list ) {
			this.list = list;
		}
	}
}
