using System.Runtime.InteropServices;

namespace CTR
{
    [StructLayout(LayoutKind.Sequential)]
    public struct DISA
    {
        public uint Magic; // "DISA"
        public uint MagicNumber; // 0x40000

        public ulong total_partentries;
        public ulong secondarytable_offset;
        public ulong primarytable_offset;
        public ulong part_tablesize;

        public ulong save_partentoffset;
        public ulong save_partentsize;
        public ulong data_partentoffset;
        public ulong data_partentsize;

        public ulong save_partoffset;
        public ulong save_partsize;
        public ulong data_partoffset;
        public ulong data_partsize;

        public uint activepart_table;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 0x20)]
        public byte[] activepart_tablehash;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 0x74)]
        public byte[] reserved;
    }
}