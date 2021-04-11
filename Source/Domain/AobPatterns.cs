using System;
using System.Text;

namespace GWANet.Domain
{
    // TODO: Think about moving related patterns to the suitable managers in the future
    internal static class AobPatterns
    {
        public static BytePattern ScanBasePtr = 
            new BytePattern(new byte[] { 0x50 ,0x6A, 0x0F, 0x6A, 0x00, 0xFF, 0x35 }, "+0x7");
        
        #region Map byte patterns
        public static BytePattern ScanMapInfo = 
            new BytePattern(new byte[] { 0x8B, 0xF0, 0xEB, 0x03, 0x8B, 0x75, 0x0C, 0x3B }, "+0xA");
        public static BytePattern ScanAreaInfo = 
            new BytePattern(new byte[] {0x6B, 0xC6, 0x7C, 0x5E, 0x05 }, "+0x5");
        public static BytePattern ScanMapId = 
            new BytePattern(new byte[] {0xE8, 0x00, 0x00, 0x00, 0x00, 0x6A, 0x3D, 0x57, 0xE8 }, "-0x4", "x????xxxx");
        public static BytePattern ScanInstanceType =
            new BytePattern(new byte[] {0x6A, 0x2C, 0x50, 0xE8, 0x00, 0x00, 0x00, 0x00, 0x83, 0xC4, 0x08, 0xC7 }, "+0x17", "xxxx????xxxx");
        #endregion
        #region Item byte patterns
        public static BytePattern ScanItemTooltip = 
            new BytePattern(new byte[] { 0x8B, 0x40, 0x40, 0x89, 0x45, 0xFC }, "-0xF");
        public static BytePattern ScanItemClick = 
            new BytePattern(new byte[] { 0x8B, 0x48, 0x08, 0x83, 0xEA, 0x00, 0x0F, 0x84 }, "-0x1C");
        public static BytePattern ScanStorageOpen = 
            new BytePattern(new byte[] { 0xC7, 0x00, 0x0F, 0x00, 0x00, 0x00, 0x89, 0x48, 0x14 }, "-0x28");
        public static BytePattern ScanStoragePanel = 
            new BytePattern(new byte[] { 0x0F, 0x84, 0x5D, 0x01, 0x00, 0x00, 0x83, 0x7B, 0x14 }, "-0x4");
        #endregion
    }
}