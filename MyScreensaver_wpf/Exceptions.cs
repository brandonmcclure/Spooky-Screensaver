using System;

namespace MyScreensaver_wpf
{
    [Serializable]
    public class IncorrectConfigurationException : Exception
    {
        public IncorrectConfigurationException() : this("Invalid Configuration detected")
        {

        }

        public IncorrectConfigurationException(string message)
           : base(message)
        {
        }

        public IncorrectConfigurationException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected IncorrectConfigurationException(System.Runtime.Serialization.SerializationInfo serializationInfo, System.Runtime.Serialization.StreamingContext streamingContext)
        {
            throw new NotImplementedException();
        }
    }
}
