namespace Supr.Model.Result {

	/// <summary>
	/// ItemResult supports a generic object and a message
	/// </summary>
	/// <typeparam name="T">The ObjectType for the generic object</typeparam>
	public class ItemResult<T> {
		public ItemResult() { }

		public T Item { get; set; }
		public string Status { get; set; }
	}
}
