using System;
using System.Collections.Generic;
using System.Linq;

namespace CTR
{
    public class SaveContainer
    {
        private readonly byte[] Data;
        private readonly DISA DISA;
        private readonly PartitionTable[] Partitions;

        private const int SIZE_HEADER = 0x100;
        private const int SIZE_DISA = 0x100;
        private const int SIZE_TABLE = 0x130;

        public bool IsMACValid(IEnumerable<byte> hash) => hash.SequenceEqual(Data);

        public SaveContainer(byte[] data)
        {
            Data = data;
            byte[] disa_data = GetArray(data, SIZE_HEADER, SIZE_DISA);
            DISA = StructConverter.ByteArrayToStructure<DISA>(disa_data);

            var table1 = GetArray(data, (int)DISA.primarytable_offset, SIZE_TABLE);
            var table2 = GetArray(data, (int)DISA.secondarytable_offset, SIZE_TABLE);
            Partitions = new[]
            {
                StructConverter.ByteArrayToStructure<PartitionTable>(table1),
                StructConverter.ByteArrayToStructure<PartitionTable>(table2),
            };
        }

        private static byte[] GetArray(byte[] data, int offset, int size)
        {
            byte[] buff = new byte[size];
            Buffer.BlockCopy(data, offset, buff, 0, size);
            return buff;
        }
    }
}
