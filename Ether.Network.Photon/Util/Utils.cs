using System;
using System.Linq;

namespace Ether.Network.Photon.Util
{
    internal static class Utils
    {
        public static byte[] ToArray(this string hex)
        {
            return Enumerable.Range(0, hex.Length)
                     .Where(x => x % 2 == 0)
                     .Select(x => Convert.ToByte(hex.Substring(x, 2), 16))
                     .ToArray();
        }
    }
}