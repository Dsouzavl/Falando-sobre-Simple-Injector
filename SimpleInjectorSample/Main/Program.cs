using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SimpleInjector;
namespace Main
{
    static class Program
    {
        static readonly Container container = new Container();

        static void Main(string[] args)
        {
            container.Register<IRepository,SqlRepository>(Lifestyle.Singleton);
            container.Verify();

        }
    }
}
