using Abp.Domain.Entities;
using Abp.EntityFramework;
using Abp.EntityFramework.Repositories;

namespace SAMS.EntityFramework.Repositories
{
    public abstract class SAMSRepositoryBase<TEntity, TPrimaryKey> : EfRepositoryBase<SAMSDbContext, TEntity, TPrimaryKey>
        where TEntity : class, IEntity<TPrimaryKey>
    {
        protected SAMSRepositoryBase(IDbContextProvider<SAMSDbContext> dbContextProvider)
            : base(dbContextProvider)
        {

        }

        //add common methods for all repositories
    }

    public abstract class SAMSRepositoryBase<TEntity> : SAMSRepositoryBase<TEntity, int>
        where TEntity : class, IEntity<int>
    {
        protected SAMSRepositoryBase(IDbContextProvider<SAMSDbContext> dbContextProvider)
            : base(dbContextProvider)
        {

        }

        //do not add any method here, add to the class above (since this inherits it)
    }
}
