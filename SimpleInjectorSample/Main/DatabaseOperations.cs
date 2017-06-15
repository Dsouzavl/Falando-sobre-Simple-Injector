using SimpleInjector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Main
{
    public class DatabaseOperations
    {
        static readonly Container container = new Container();

        public DatabaseOperations()
        {
            container.Register<IRepository, SqlRepository>(Lifestyle.Singleton);
            container.Verify();
        }       

        public bool SomeDatabaseOperation()
        {
            var sqlRepository = container.GetInstance<IRepository>();
            return sqlRepository.Create(Guid.NewGuid());
        }
    }
}
