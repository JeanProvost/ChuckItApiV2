using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChuckIt.Core.Exceptions
{
    public class ModelValidationException : Exception
    {
        public List<string> Errors { get; set; } = [];
        public ModelValidationException() { }
        public ModelValidationException(string message) : base(message) { }
        public ModelValidationException(List<string> errors) : base("Validation errors have occurred")
        {
            Errors = errors;
        }
    }
}
