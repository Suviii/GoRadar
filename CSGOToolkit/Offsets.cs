namespace CSGOToolkit
{
    public static class Offsets
    {
        public static int ClientBaseAddress = 0;
        public static int PlayerBaseAddress = ClientBaseAddress + 78120104;
        public static int EntityListBaseAddress = ClientBaseAddress + 78161492;
        public static int MaxEntities = 64;
        public static int EntityLength = 16;
        public static int EntityCoordinateOffset = 160;
        public static int EntityTeamId = 240;
        public static int EntityHealth = 252;
    }
}
