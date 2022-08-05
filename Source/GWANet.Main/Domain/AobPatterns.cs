using GWANet.Scanner.Definitions;

namespace GWANet.Main.Domain
{
    // TODO: Move related patterns to separate classes
    public static class AobPatterns
    {
        public static readonly BytePattern ScanBasePtr = new("50 6A 0F 6A 00 FF 35", 0x7);

        public static readonly BytePattern GameVersion = new("6A 00 68 00 00 01 00 89", 0x42);
        public static readonly BytePattern CharacterName = new("CC CC CC 55 8B EC 51 66", 0x102B);
        #region Agent
        public static readonly BytePattern ScanAgentBasePtr = new("FF 50 10 47 83 C6 04 3B FB 75 E1", 0xD);
        public static readonly BytePattern MoveAgentFuncPtr = new("DF E0 F6 C4 41 7B 64 56 E8", -0x48);

        public static readonly BytePattern MovementChangeAgentFuncPtr = new("0C 05 6F FF FF FF", -0x9);
        public static readonly BytePattern PlayerAgentIdPtr = new("5D E9 00 00 00 00 55 8B EC 53", -0xE);
        #endregion
        
        #region Map byte patterns
        public static readonly BytePattern ScanMapInfo = new("8B F0 EB 03 8B 75 0C 3B", 0xA);
        public static readonly BytePattern ScanAreaInfo = new("6B C6 7C 5E 05", 0x5);
        public static readonly BytePattern ScanMapId = new("E8 ?? ?? ?? ?? 6A 3D 57 E8", -0x4);
        public static readonly BytePattern ScanInstanceType = new("6A 2C 50 E8 ?? ?? ?? ?? 83 C4 08 C7", 0x17);
        #endregion
        #region Item byte patterns
        public static readonly BytePattern ScanItemTooltip = new("8B 40 40 89 45 FC", -0xF);
        public static readonly BytePattern ScanItemClick = new("8B 48 08 83 EA 00 0F 84", -0x1C);
        public static readonly BytePattern ScanStorageOpen = new("C7 00 0F 00 00 00 89 48 14", -0x28);
        public static readonly BytePattern ScanStoragePanel = new("0F 84 5D 01 00 00 83 7B 14", -0x4);
        #endregion
    }
}