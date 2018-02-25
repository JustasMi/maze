using System;

namespace Maze.Exceptions
{
	public class ManagerException : Exception
	{
		public ManagerException(string reason)
			: base(reason)
		{
		}

		public ManagerException(string reason, Exception innerException)
			: base(reason, innerException)
		{
		}
	}
}