using System.Runtime.InteropServices;

namespace CTR
{
    [StructLayout(LayoutKind.Sequential)]
    public struct DPFS
    {
        public uint magic; // "DPFS"
        public uint magicnum; // 0x10000

        [MarshalAs(UnmanagedType.ByValArray, ArraySubType = UnmanagedType.Struct, SizeConst = 2)]
        IVFCLevel[] tables;
        IVFCLevel ivfcpart;
    }
}