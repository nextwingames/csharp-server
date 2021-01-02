using MessagePack;
using Nextwin.Protocol;
using System;

namespace Nextwin.Server.Protocol
{
    public class Serializer : ISerializer
    {
        public static Serializer Instance
        {
            get
            {
                return _instance.Value;
            }
        }
        private static readonly Lazy<Serializer> _instance = new Lazy<Serializer>(() => new Serializer());

        private Serializer() { }

        public T Deserialize<T>(byte[] bytes)
        {
            return MessagePackSerializer.Deserialize<T>(bytes);
        }

        public byte[] Serialize<T>(T data)
        {
            return MessagePackSerializer.Serialize(data);
        }
    }
}
