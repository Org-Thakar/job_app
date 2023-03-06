using Microsoft.Extensions.Configuration;
using Job.Core.Repositories.Base;


namespace Job.Infrastructure.Repositories.Base
{
    public class Repository<T> : DbConnector, IRepository<T> where T : class
    {
        public Repository(IConfiguration configuration)
            : base(configuration)
        {

        }
    }
}
