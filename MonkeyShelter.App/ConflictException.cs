using System;
using System.Runtime.Serialization;

namespace MonkeyShelter.App
{
    [Serializable]
    public class ConflictException : ApplicationException
    {
        public ConflictException()
            : base("Monkey already exists.")
        {
        }

        public ConflictException(string id) : base($"Monkey {id} already exists.")
        {
        }

        public ConflictException(string id, Exception innerException) : base($"Monkey {id} already exists.", innerException)
        {
        }

        protected ConflictException(
            SerializationInfo info,
            StreamingContext context) : base(info, context)
        {
        }

    }
}