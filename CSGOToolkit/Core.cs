using ProcessMemoryReaderLib;
using System;
using System.Collections.Generic;

namespace CSGOToolkit
{
    public static class Core
    {
        public static IList<Player> GetPlayers(ProcessMemoryReader memoryReader)
        {
            IList<Player> players = new List<Player>();

            for (var i = 0; i < Offsets.MaxEntities; i++)
            {
                var heapAddress = GetHeapAddress(GetEntities(memoryReader), i);

                if (heapAddress != 0)
                {
                    players.Add(new Player()
                    {
                        PlayerId = i,
                        TeamId = ReadInt(memoryReader, heapAddress, Offsets.EntityTeamId),
                        Health = ReadInt(memoryReader, heapAddress, Offsets.EntityHealth),
                        X = ReadSingle(memoryReader, heapAddress, Offsets.EntityCoordinateOffset + 0),
                        Y = ReadSingle(memoryReader, heapAddress, Offsets.EntityCoordinateOffset + 4),
                        Z = ReadSingle(memoryReader, heapAddress, Offsets.EntityCoordinateOffset + 8)
                    });
                }
            }

            return players;
        }

        private static int GetHeapAddress(IReadOnlyList<byte> entities, int index)
        {
            return BitConverter.ToInt32(new[] {
                entities[0 + index * Offsets.EntityLength],
                entities[1 + index * Offsets.EntityLength],
                entities[2 + index * Offsets.EntityLength],
                entities[3 + index * Offsets.EntityLength] }, 0);
        }

        private static byte[] GetEntities(ProcessMemoryReader memoryReader)
        {
            return memoryReader.ReadProcessMemory((IntPtr)(Offsets.ClientBaseAddress + Offsets.EntityListBaseAddress), (uint)(Offsets.MaxEntities * Offsets.EntityLength), out int _);
        }

        private static int ReadInt(ProcessMemoryReader memoryReader, int baseaddress, int offset)
        {
            return BitConverter.ToInt32(memoryReader.ReadProcessMemory((IntPtr)(baseaddress + offset), 4, out int _), 0);
        }

        private static float ReadSingle(ProcessMemoryReader memoryReader, int baseaddress, int offset)
        {
            return BitConverter.ToSingle(memoryReader.ReadProcessMemory((IntPtr)(baseaddress + offset), 4, out int _), 0);
        }
    }
}
