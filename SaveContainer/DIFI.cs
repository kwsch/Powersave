using System.Runtime.InteropServices;

namespace CTR
{
    [StructLayout(LayoutKind.Sequential)]
    public struct DIFI
    {
        public uint Magic; // "DIFI"
        public uint MagicNumber; // 0x10000

        public ulong ivfc_offset; //Relative to this DIFI
        public ulong ivfc_size;
        public ulong dpfs_offset; //Relative to the IVFC
        public ulong dpfs_size;
        public ulong parthash_offset;
        public ulong parthash_size;

        public uint flags; //When the low 8-bits are non-zero, this is a DATA partition

        public ulong filebase_offset; //DATA partition only
    }
}