using Ether.Network.Packets;
using System;
using System.IO;

namespace Ether.Network.Photon.Packet
{
    public class PhotonPacketStream : MemoryStream, INetPacketStream
    {
        public PhotonPacketStream()
        {
            Writer = new BinaryWriter(this);
            Reader = new BinaryReader(this);
        }

        public T Read<T>()
        {
            if (!PhotonMethods.InMethods.ContainsKey(typeof(T)))
                throw new InvalidOperationException(nameof(T));

            return (T)PhotonMethods.InMethods[typeof(T)](Reader);
        }

        public T[] Read<T>(int amount)
        {
            Type type = typeof(T);
            var array = new T[amount];

            if (type == typeof(byte))
                array = this.Reader.ReadBytes(amount) as T[];
            else if (PhotonMethods.InMethods.ContainsKey(type))
            {
                for (var i = 0; i < amount; i++)
                    array[i] = Read<T>();
            }

            return array;
        }

        public void Write<T>(T value)
        {
            Type type = typeof(T);

            if (PhotonMethods.OutMethods.ContainsKey(type))
                PhotonMethods.OutMethods[type](Writer, value);
        }
        
        public int Size => (int)base.Length;
        public virtual byte[] Buffer => base.ToArray();
        private readonly BinaryReader Reader;
        private readonly BinaryWriter Writer;
    }
}