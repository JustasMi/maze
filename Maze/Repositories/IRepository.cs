using System;
using System.Linq;
using System.Threading.Tasks;

namespace Maze.Repositories
{
	public interface IRepository<TEntity, in TKey> : IDisposable
	{
		Task<TEntity> Get(TKey key);

		Task Create(TEntity entity);

		IQueryable<TEntity> GetAll();
	}
}