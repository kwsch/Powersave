using System.Runtime.InteropServices;

namespace CTR
{
    [StructLayout(LayoutKind.Sequential)]
    public struct IVFC
    {
        public uint magic; // "IVFC"
        public uint magicnum; // 0x20000

        public ulong masterhash_size; // Size of the hash which hashes lvl1

        [MarshalAs(UnmanagedType.ByValArray, ArraySubType = UnmanagedType.Struct, SizeConst = 4)]
        IVFCLevel[] levels;

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]
        public byte[] unknown;
    }
}