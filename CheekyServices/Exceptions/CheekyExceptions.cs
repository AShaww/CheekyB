using System.Runtime.Serialization;

namespace CheekyServices.Exceptions;

/// <summary>
/// Exception class for dealing with projects Exceptions 
/// </summary>
public class CheekyExceptions<T> : Exception where T : Exception
{
    public CheekyExceptions() { }
    public CheekyExceptions(string message, Exception innerException) : base(message, innerException){ }
    public CheekyExceptions(string message) : base(message){ }
    public CheekyExceptions(SerializationInfo info, StreamingContext context) : base(info, context){ }
}
