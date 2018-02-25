using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Maze.Validation.Common
{
	public interface IValidator<in T>
	{
		ValidationResults Validate(T validationCandidate);
	}
}