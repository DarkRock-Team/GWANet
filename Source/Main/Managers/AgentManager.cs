using System;
using GWANet.Domain;

namespace GWANet.Managers
{
    internal class AgentManager
    {
        private readonly IMemScanner _memScanner;
        
        private ulong _agentArrayPtr;
        private ulong _moveFuncPtr;
        private ulong _movementChangeFuncPtr;
        private ulong _playerAgentIdPtr;
        
        public AgentManager(IMemScanner memScanner)
        {
            _memScanner = memScanner;

            _memScanner.AddPattern(_movementChangeFuncPtr, AobPatterns.MovementChangeAgentFuncPtr);
            _memScanner.AddPattern(_agentArrayPtr, AobPatterns.ScanAgentBasePtr);
            _memScanner.AddPattern(_moveFuncPtr, AobPatterns.MoveAgentFuncPtr);
            _memScanner.AddPattern(_playerAgentIdPtr, AobPatterns.PlayerAgentIdPtr);
        }

        public int GetAgentId()
        {
            throw new NotImplementedException();
        }
    }
}