using CounterStrikeSharp.API.Core;
using CounterStrikeSharp.API.Modules.Entities;
using cs2_rockthevote.Core;

namespace cs2_rockthevote
{
    public partial class Plugin
    {
        private readonly MapLister _mapLister;

        public Plugin(MapLister mapLister)
        {
            _mapLister = mapLister;
        }

        [ConsoleCommand("mapnominated", "Display nominated maps")]
        public void OnMapNominated(CCSPlayerController player, CommandInfo command)
        {
            _mapLister.PrintNominatedMapsToClient(player);
        }
    }
}
