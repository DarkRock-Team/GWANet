using System;
using System.Collections.Generic;
using TitleHelper.Domain;

namespace TitleHelper.Services
{
    public interface IGWANetService
    {
        public void InitializeWithoutCharName();
        public void MaxOutSweettoothTitle();
        public List<Title> GetAllCharacterTitles();

    }
}
