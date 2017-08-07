using System.Runtime.InteropServices;

namespace CTR
{
    [StructLayout(LayoutKind.Sequential)]
    public struct FSTEntry
    {
        public uint parent_dirid;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 0x10)]
        public byte[] name;
        public uint id;
        public uint unk1;
        public uint block_no;
        public ulong size;
        public uint unk4;
        public uint unk5;
    }
}