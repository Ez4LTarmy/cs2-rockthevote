namespace cs2_rockthevote;

using CounterStrikeSharp.API.Core;
using CounterStrikeSharp.API.Core.Attributes.Registration;
using CounterStrikeSharp.API.Modules.Commands;


public partial class Plugin
{
    public char NewLine = '\u2029';
    [ConsoleCommand("nomlist", "Show nomlist")]
    public void OnNomlist(CCSPlayerController? player, CommandInfo command)
    {

        var Nomlist = _nominationManager.Nomlist
            .Values
            .SelectMany(list => list)
            .Distinct()
            .Select((map, index) => $"{index + 1}. {map}");

        string Maplist = string.Join(NewLine.ToString(), Nomlist);

        player.PrintToChat("*** LIST MAP NOMINATED ***");
        player.PrintToChat(Maplist);
        player.PrintToChat("********************************");
    }


}
