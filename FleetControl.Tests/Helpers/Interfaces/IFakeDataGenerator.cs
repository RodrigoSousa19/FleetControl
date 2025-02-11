using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FleetControl.Tests.Helpers.Interfaces
{
    public interface IFakeDataGenerator<T>
    {
        T Generate();
        IList<T> GenerateList(int quantity);
    };
}
