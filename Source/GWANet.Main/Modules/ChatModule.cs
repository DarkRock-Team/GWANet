using System;
using System.Collections.Generic;
using GWANet.Main.Domain;
using RapidMemory.Definitions;

namespace GWANet.Main.Modules;

internal sealed class ChatModule : IModule, IDisposable
{
    public void Initialize()
    {
        
    }

    public static void AddChatAobsToList(ref List<BytePattern> patternsToScan)
    {
        patternsToScan.AddRange(new List<BytePattern>
        {
            AobPatterns.ChatEvent,
            AobPatterns.GetSenderColor,
            AobPatterns.GetMessageColor,
            AobPatterns.LocalMessage,
            AobPatterns.SendChat,
            AobPatterns.StartWhisper,
            AobPatterns.WriteWhisper,
            AobPatterns.PrintChat,
            AobPatterns.AddToChatLog,
            AobPatterns.ChatBuffer,
            AobPatterns.IsTyping
        });
    }

    public void Dispose()
    {
        // remove hooks
    }
}