// Automatically generated by xdrgen
// DO NOT EDIT or your changes may be overwritten

using System;

namespace stellar_dotnet_sdk.xdr
{
// === xdr source ============================================================
//  typedef opaque Hash[32];
//  ===========================================================================
    public class Hash
    {
        public byte[] InnerValue { get; set; } = default(byte[]);

        public Hash()
        {
        }

        public Hash(byte[] value)
        {
            InnerValue = value;
        }

        public static void Encode(XdrDataOutputStream stream, Hash encodedHash)
        {
            int Hashsize = encodedHash.InnerValue.Length;
            stream.Write(encodedHash.InnerValue, 0, Hashsize);
        }

        public static Hash Decode(XdrDataInputStream stream)
        {
            Hash decodedHash = new Hash();
            int Hashsize = 32;
            decodedHash.InnerValue = new byte[Hashsize];
            stream.Read(decodedHash.InnerValue, 0, Hashsize);
            return decodedHash;
        }
    }
}