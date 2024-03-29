using CounterStrikeSharp.API.Core;
using CounterStrikeSharp.API.Modules.Entities;
using CounterStrikeSharp.API.Modules.Commands;
using cs2_rockthevote.Core;

namespace cs2_rockthevote
{
    public partial class Plugin
    {
        [ConsoleCommand("mapnominated", "Display nominated maps")]
        public void OnMapNominated(CCSPlayerController player, CommandInfo command)
        {
            _mapLister.PrintNominatedMapsToClient(player);
        }
    }
}
