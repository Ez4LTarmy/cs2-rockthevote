using CounterStrikeSharp.API.Core;
using CounterStrikeSharp.API.Modules.Entities;
using CounterStrikeSharp.API.Core.Attributes.Registration;
using cs2_rockthevote.Core;

namespace cs2_rockthevote
{
    public partial class Plugin
    {
        [ConsoleCommand("mapnominated", "Display nominated maps")]
        public void OnMapNominated(CCSPlayerController player, string[] args)
        {
            _mapLister.PrintNominatedMapsToClient(player);
        }
    }
}
