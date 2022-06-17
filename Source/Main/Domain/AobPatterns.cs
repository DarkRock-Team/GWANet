using System;
using System.Text;

namespace GWANet.Domain
{
    // TODO: Think about moving related patterns to the suitable managers in the future
    public static class AobPatterns
    {
        public static readonly BytePattern ScanBasePtr = 
            new BytePattern(new byte[] { 0x50 ,0x6A, 0x0F, 0x6A, 0x00, 0xFF, 0x35 }, 0x7);

        public static readonly BytePattern GameVersion =
            new BytePattern(new byte[] {0x6A, 0x00, 0x68, 0x00, 0x00, 0x01, 0x00, 0x89}, 0x42);
        #region Agent
        public static readonly BytePattern ScanAgentBasePtr =
            new BytePattern(new byte[] { 0xFF, 0x50, 0x10, 0x47, 0x83, 0xC6, 0x04, 0x3B, 0xFB, 0x75, 0xE1 }, 0xD);
        public static readonly BytePattern MoveAgentFuncPtr =
            new BytePattern(new byte[] { 0xDF, 0xE0, 0xF6, 0xC4, 0x41, 0x7B, 0x64, 0x56, 0xE8 }, -0x48);

        public static readonly BytePattern MovementChangeAgentFuncPtr =
            new BytePattern(new byte[] { 0x0C, 0x05, 0x6F, 0xFF, 0xFF, 0xFF }, -0x9);
        public static readonly BytePattern PlayerAgentIdPtr =
            new BytePattern(new byte[] { 0x5D, 0xE9, 0x00, 0x00, 0x00, 0x00, 0x55, 0x8B, 0xEC, 0x53 }, -0xE);
        #endregion
        
        #region Map byte patterns
        public static readonly BytePattern ScanMapInfo = 
            new BytePattern(new byte[] { 0x8B, 0xF0, 0xEB, 0x03, 0x8B, 0x75, 0x0C, 0x3B }, 0xA);
        public static readonly BytePattern ScanAreaInfo = 
            new BytePattern(new byte[] {0x6B, 0xC6, 0x7C, 0x5E, 0x05 }, 0x5);
        public static readonly BytePattern ScanMapId = 
            new BytePattern(new byte[] {0xE8, 0x00, 0x00, 0x00, 0x00, 0x6A, 0x3D, 0x57, 0xE8 }, -0x4, "x????xxxx");
        public static readonly BytePattern ScanInstanceType =
            new BytePattern(new byte[] {0x6A, 0x2C, 0x50, 0xE8, 0x00, 0x00, 0x00, 0x00, 0x83, 0xC4, 0x08, 0xC7 }, 0x17, "xxxx????xxxx");
        #endregion
        #region Item byte patterns
        public static readonly BytePattern ScanItemTooltip = 
            new BytePattern(new byte[] { 0x8B, 0x40, 0x40, 0x89, 0x45, 0xFC }, -0xF);
        public static readonly BytePattern ScanItemClick = 
            new BytePattern(new byte[] { 0x8B, 0x48, 0x08, 0x83, 0xEA, 0x00, 0x0F, 0x84 }, -0x1C);
        public static readonly BytePattern ScanStorageOpen = 
            new BytePattern(new byte[] { 0xC7, 0x00, 0x0F, 0x00, 0x00, 0x00, 0x89, 0x48, 0x14 }, -0x28);
        public static readonly BytePattern ScanStoragePanel = 
            new BytePattern(new byte[] { 0x0F, 0x84, 0x5D, 0x01, 0x00, 0x00, 0x83, 0x7B, 0x14 }, -0x4);
        #endregion
    }
}