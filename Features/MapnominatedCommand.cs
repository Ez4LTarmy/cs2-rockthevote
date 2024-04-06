namespace cs2_rockthevote;

using CounterStrikeSharp.API.Core;
using CounterStrikeSharp.API.Core.Attributes.Registration;
using CounterStrikeSharp.API.Modules.Commands;

public partial class Plugin
{
    [ConsoleCommand("nomlist", "Show nomlist")]
    public void OnNomlist(CCSPlayerController? player, CommandInfo command)
    {
        var test = _nominationManager.Nomlist
            .Values
            .SelectMany(list => list)
            .Distinct();

    }
}
