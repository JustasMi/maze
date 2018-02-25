using System;
using System.Threading.Tasks;

namespace Maze.Repositories
{
	public interface IRepository<TEntity, in TKey> : IDisposable
	{
		Task<TEntity> Get(TKey key);

		Task Create(TEntity entity);
	}
}