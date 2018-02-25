using System.Collections.Generic;
using System.Linq;

namespace Maze.Validation.Common
{
	public class ValidationOutcome
	{
		private ValidationResults results;

		public ValidationResults Results
		{
			get
			{
				return results;
			}
		}

		public ValidationOutcome(ValidationResults results)
		{
			this.results = results;
		}

		public bool IsValid()
		{
			return results.All(validationResult => validationResult.IsValid);
		}

		public bool IsInvalid()
		{
			return results.Any(validationResult => !validationResult.IsValid);
		}

		public string GetValidationMessage()
		{
			IEnumerable<string> failedValidations = results
				.Where(result => !result.IsValid)
				.Select(result => result.ToString());

			if (!failedValidations.Any())
			{
				return "Sucessfully validated";
			}
			else
			{
				return string.Join(",", failedValidations);
			}
		}
	}
}