using System.Collections.Generic;

namespace Maze.Validation.Common
{
	public class ValidationResults : List<ValidationResult>
	{
		private ValidationResults allResults;

		public ValidationResults()
		{
		}

		public ValidationResults(IEnumerable<ValidationResult> results)
			: base(results)
		{
		}
	}
}