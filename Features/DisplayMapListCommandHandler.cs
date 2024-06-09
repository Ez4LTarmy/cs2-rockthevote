namespace cs2_rockthevote.Features;

using cs2_rockthevote.Contracts;
using cs2_rockthevote.Core;

public class DisplayMapListCommandHandler : IPluginDependency<Plugin, Config>
{
    private IEnumerable<Map> _maps = Enumerable.Empty<Map>();
    private readonly int _mapsPerPage = 25;

    public void OnLoad(Plugin plugin)
    {
        var maplistPath = Path.Combine(plugin.ModulePath, "../maplist.txt");
        var maplist = File.ReadAllLines(maplistPath);
        _maps = maplist.Select(map =>
        {
            var mapWithId = map.Split(':');

            var name = mapWithId[0].AsSpan().Trim().ToString();
            var id = mapWithId[1].AsSpan().Trim().ToString();

            return new Map(name, id);
        });

        plugin.AddCommand("maplist", "Displays maplist into console", (player, info) =>
        {
            var part = info.GetArg(1); // current part to display

            if (!int.TryParse(part, out var partNumber))
            {
                player?.PrintToChat("You need to provide the number which part you want to display (0 by def)");
            }

            if (partNumber < 0)
            {
                player?.PrintToConsole("Invalid part number.");
                return;
            }

            _maps.Skip(_mapsPerPage * partNumber).Take(_mapsPerPage).ToList().ForEach(map =>
            {
                player?.PrintToConsole(map.Name);
            });
        });
    }
}
