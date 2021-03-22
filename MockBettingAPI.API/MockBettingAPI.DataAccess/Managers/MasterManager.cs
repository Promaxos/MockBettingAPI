using MockBettingAPI.DataAccess.Managers.Interfaces;

namespace MockBettingAPI.DataAccess.Managers
{
    public class MasterManager : IMasterManager
    {
        public IMatchManager Matches { get; private set; }


        public MasterManager(Context db)
        {
            Matches = new MatchManager(db);
        }
    }
}
