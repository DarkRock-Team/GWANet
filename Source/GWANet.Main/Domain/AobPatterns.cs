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
        // AgentArrayPtr
        public static readonly BytePattern ScanAgentBasePtr = new("FF 50 10 47 83 C6 04 3B FB 75 E1", 0xD);
        public static readonly BytePattern PlayerAgentIdPtr = new("5D E9 ?? ?? ?? ?? 55 8B EC 53", -0xE);
        public static readonly BytePattern SendDialog = new("0F B7 C0 0D 00 00 00 10", 0x9);
        public static readonly BytePattern InteractAgent = new("C7 45 F0 98 3A 00 00", 0x41);
        public static readonly BytePattern MoveAgentFuncPtr = new("DF E0 F6 C4 41 7B 64 56 E8", -0x48);
        public static readonly BytePattern MovementChangeAgentFuncPtr = new("0C 05 6F FF FF FF", -0x9);
        public static readonly BytePattern ChangeTargetFunc = new("3B DF 0F 95", -0x0089);
        
        #endregion

        #region Chat

        public static readonly BytePattern ChatEvent = new("83 FB 06 1B", -0x2A);
        public static readonly BytePattern GetSenderColor = new("C7 00 60 C0 FF FF 5D C3", -0x1C);
        public static readonly BytePattern GetMessageColor = new("C7 00 B0 B0 B0 FF 5D C3", -0x27);
        public static readonly BytePattern LocalMessage = new("8D 45 F8 6A 00 50 68 7E 00 00 10", -0x3D);
        public static readonly BytePattern SendChat = new("8D 85 E0 FE FF FF 50 68 1C 01", -0x3E);
        public static readonly BytePattern StartWhisper = new("FC 53 56 8B F1 57 6A 05 FF 36 E8", -0xF);
        public static readonly BytePattern WriteWhisper = new("83 C4 04 8D 58 2E", -0x18);
        public static readonly BytePattern PrintChat = new("3D ?? ?? 00 00 73 2B 6A", -0x46);
        public static readonly BytePattern AddToChatLog = new("40 25 FF 01 00 00", -0x97);
        public static readonly BytePattern ChatBuffer = new("8B 45 08 83 7D 0C 07 74", -4);
        public static readonly BytePattern IsTyping = new("08 FF D0 C7 05 ?? ?? ?? ?? 01", 5);

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