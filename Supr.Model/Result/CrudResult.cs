using System.Collections.Generic;
using System.Data.Entity.Validation;

namespace Supr.Model.Result {

	/// <summary>
	/// CrudResult supports a boolean result, a message and an errors collection
	/// </summary>
	public class CrudResult {
		public CrudResult() {
			Success = false;
		}

		public bool Success { get; set; }
		public string Status { get; set; }
		public IEnumerable<DbEntityValidationResult> Errors { get; set; }
	}
}