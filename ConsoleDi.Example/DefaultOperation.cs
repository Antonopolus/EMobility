using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleDi.Example
{
    class DefaultOperation : ITransientOperation, IScopedOperation, ISingletonOperation
    {
        public string OperationId { get; } = Guid.NewGuid().ToString()[^4..];


    }
}
