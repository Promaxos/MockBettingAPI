using System;
using System.Collections.Generic;
using System.Text;

namespace MockBettingAPI.DataAccess.Managers.Interfaces
{
    public interface IMasterManager
    {
        public IMatchManager Matches { get; }
    }
}
