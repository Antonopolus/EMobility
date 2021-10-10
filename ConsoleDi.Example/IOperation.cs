using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleDi.Example
{
    public interface IOperation
    {
        public string OperationId { get; }
    }

    public interface ITransientOperation : IOperation
    {
    }
    public interface IScopedOperation : IOperation
    {
    }

    public interface ISingletonOperation : IOperation
    {
    }
}
