using System;
using System.Runtime.InteropServices;

namespace GWANet.Entities
{
    internal class AgentEntity
    {
        public UIntPtr VTable { get; set; }
        public uint Timer { get; set; } //0x14, instance timer in game frames
        public uint Timer2 { get; set; } //0x18
        public virtual AgentEntity AgentLink { get; set; } // 0x1C, next
        public virtual AgentEntity AgentLink2 { get; set; } //0x24, previous
        public uint AgentId { get; set; } // 0x2C
        public float Z { get; set; } //0x30, z coord
        public ModelBoxStruct AgentModelBox { get; set; }
    }
    
    [StructLayout(LayoutKind.Sequential)]
    internal struct ModelBoxStruct
    {
        public float Width1 { get; set; } // 0x34 from AgentEntity
        public float Height1 { get; set; }
        public float Width2 { get; set; }
        public float Height2 { get; set; }
        public float Width3 { get; set; }
        public float Height3 { get; set; }
        public float RotationAngle { get; set; } // 0x4C from AgentEntity
        public float RotationCos { get; set; }
        public float RotationSin { get; set; }
    }
}