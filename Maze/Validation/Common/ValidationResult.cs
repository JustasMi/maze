using System;

namespace Maze.Validation.Common
{
	public enum ResultType
	{
		Success,
		Failure
	}

	public class ValidationResult
	{
		private ResultType resultType = ResultType.Failure;
		private Type typeFrom;
		private string message;
		private ValidationResults innerResults;

		public string ResultFrom
		{
			get
			{
				return typeFrom.Name;
			}
		}

		public string Message
		{
			get
			{
				return resultType == ResultType.Failure ? message : resultType.ToString();
			}
		}

		public ValidationResults InnerResults
		{
			get
			{
				return innerResults;
			}
		}

		public bool IsValid
		{
			get
			{
				return resultType == ResultType.Success;
			}
		}

		private ValidationResult(Type typeFrom, ResultType resultType)
		{
			Initialise(typeFrom, resultType, null, null);
		}

		private ValidationResult(Type typeFrom, ResultType resultType, string message)
		{
			Initialise(typeFrom, resultType, message, null);
		}

		private ValidationResult(Type typeFrom, ResultType resultType, string message, ValidationResults innerResults)
		{
			Initialise(typeFrom, resultType, message, innerResults);
		}

		private void Initialise(Type typeFrom, ResultType resultType, string message, ValidationResults innerResults)
		{
			this.typeFrom = typeFrom;
			this.resultType = resultType;
			this.message = message;
			this.innerResults = innerResults != null ? innerResults : new ValidationResults();
		}

		internal static ValidationResults Success<T>()
		{
			return new ValidationResults() { new ValidationResult(typeof(T), ResultType.Success) };
		}

		internal static ValidationResults Failure<T>(string message, ValidationResults innerResults = null)
		{
			return new ValidationResults() { new ValidationResult(typeof(T), ResultType.Failure, message, innerResults) };
		}

		public override string ToString()
		{
			return string.Format("{0}: {1}", ResultFrom, Message);
		}
	}
}