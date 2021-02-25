using System;
using System.Runtime.Serialization;

namespace MonkeyShelter.App
{
    [Serializable]
    public class NotFoundException : ApplicationException
    {
        public NotFoundException()
            : base("Monkey not found")
        {
        }

        public NotFoundException(string id) : base($"Monkey {id} not found.")
        {
        }

        public NotFoundException(string id, Exception inner) : base($"Monkey {id} not found.", inner)
        {
        }

        protected NotFoundException(
            SerializationInfo info,
            StreamingContext context) : base(info, context)
        {
        }
    }
}