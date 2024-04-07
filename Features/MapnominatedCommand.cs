namespace cs2_rockthevote;

using CounterStrikeSharp.API.Core;
using CounterStrikeSharp.API.Core.Attributes.Registration;
using CounterStrikeSharp.API.Modules.Commands;

public partial class Plugin
{
    [ConsoleCommand("nomlist", "Show nomlist")]
    public void OnNomlist(CCSPlayerController? player, CommandInfo command)
    {
        var Nomlist = _nominationManager.Nomlist
            .Values
            .SelectMany(list => list)
            .Distinct()
            .Select((map, index) => $"{index + 1}. {map}");

        player.PrintToChat("==> Nomlist will display on console <==");

        string Maplist = string.Join(System.Environment.NewLine, Nomlist);

        player.PrintToConsole("**** LIST OF MAPS NOMINATED ***");
        player.PrintToConsole(Maplist);
        player.PrintToConsole("*********************************");
    }

}
