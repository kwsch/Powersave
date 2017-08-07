using System.Runtime.InteropServices;

namespace CTR
{
    [StructLayout(LayoutKind.Sequential)]
    public struct IVFCLevel
    {
        public ulong offset;
        public ulong size;
        public uint blksize;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
        public byte[] reserved;
    }
}