using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Data.Core
{
    public class OperationResult<TData>
    {
        public bool Success { get; set; }
        public string? Message { get; set; }
        public TData? Result { get; set; }
    }
}
