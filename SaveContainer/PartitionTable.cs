using System.Runtime.InteropServices;

namespace CTR
{
    [StructLayout(LayoutKind.Sequential)]
    public struct PartitionTable
    {
        public DIFI difi;
        public IVFC ivfc;
        public DPFS dpfs;

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 0x20)]
        public byte[] ivfcpart_masterhash;
    }
}