using System;
using System.Collections.Generic;
using TitleHelper.Domain;
using GWANet;

namespace TitleHelper.Services
{
    public class GWANetService : IGWANetService
    {
        private readonly IGWANet _gwaNet;

        public GWANetService(IGWANet gwaNet)
        {
            _gwaNet = gwaNet;
        }
        public List<Title> GetAllCharacterTitles()
        {
            throw new NotImplementedException();
        }

        public void InitializeWithoutCharName()
        {
            _gwaNet.Initialize(string.Empty, isChangeGameTitle: false);
        }

        public void MaxOutSweettoothTitle()
        {
            throw new NotImplementedException();
        }
    }
}
