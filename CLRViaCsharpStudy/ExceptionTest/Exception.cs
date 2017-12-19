using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace ExceptionTest
{
    [Serializable]
    public sealed  class Exception<TExceptionArg> :Exception, ISerializable 
     where TExceptionArg:ExceptionArg
    {

    }
    [Serializable]
    public abstract class ExceptionArg
    {
        public virtual string Message { get { return string.Empty; } }
    }
}
