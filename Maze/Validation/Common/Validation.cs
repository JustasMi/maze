namespace Maze.Validation.Common
{
	public class Validation<T>
	{
		private readonly T candidate;
		private readonly ValidationResults results;

		private Validation(T candidate)
		{
			this.candidate = candidate;
			this.results = new ValidationResults();
		}

		public Validation(T candidate, ValidationResults allResults)
		{
			this.candidate = candidate;
			this.results = new ValidationResults(allResults);
		}

		public static Validation<T> Candidate(T candidate)
		{
			return new Validation<T>(candidate);
		}

		public Validation<T> Validate(IValidator<T> validator)
		{
			ValidationResults allResults = new ValidationResults(results);
			allResults.AddRange(validator.Validate(candidate));
			return new Validation<T>(candidate, allResults);
		}

		public ValidationOutcome Outcome()
		{
			return new ValidationOutcome(results);
		}
	}
}