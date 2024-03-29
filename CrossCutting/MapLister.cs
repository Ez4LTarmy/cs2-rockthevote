using System;
using System.IO;
using System.Linq;
using CounterStrikeSharp.API.Core;
using CounterStrikeSharp.API.Modules.Entities;
using cs2_rockthevote.Core;

namespace cs2_rockthevote
{
    public class MapLister : IPluginDependency<Plugin, Config>
    {
        public Map[]? Maps { get; private set; } = null;
        public bool MapsLoaded { get; private set; } = false;

        public event EventHandler<Map[]>? EventMapsLoaded;

        private Plugin? _plugin;
        private MapCooldown? _mapCooldown; // Reference to MapCooldown class

        // Store nominated maps
        private List<Map> NominatedMaps { get; } = new List<Map>();

        public MapLister(MapCooldown mapCooldown)
        {
            _mapCooldown = mapCooldown;
        }

        public void Clear()
        {
            MapsLoaded = false;
            Maps = null;
            NominatedMaps.Clear();
        }

        void LoadMaps()
        {
            Clear();
            string mapsFile = Path.Combine(_plugin!.ModulePath, "../maplist.txt");
            if (!File.Exists(mapsFile))
                throw new FileNotFoundException(mapsFile);

            Maps = File.ReadAllText(mapsFile)
                .Replace("\r\n", "\n")
                .Split("\n")
                .Select(x => x.Trim())
                .Where(x => !string.IsNullOrWhiteSpace(x) && !x.StartsWith("//"))
                .Select(mapLine =>
                {
                    string[] args = mapLine.Split(":");
                    return new Map(args[0], args.Length == 2 ? args[1] : null);
                })
                .ToArray();

            MapsLoaded = true;
            if (EventMapsLoaded is not null)
                EventMapsLoaded.Invoke(this, Maps!);
        }

        public void OnMapStart(string _map)
        {
            if (_plugin is not null)
            {
                LoadMaps();
                _mapCooldown?.UpdateRecentlyPlayedMap(_map);
            }
        }

        public void OnLoad(Plugin plugin)
        {
            _plugin = plugin;
            LoadMaps();
        }

        // returns "" if there's no matching or if there's more than one
        // otherwise, returns the matching name
        public string GetSingleMatchingMapName(string map, CCSPlayerController player, StringLocalizer _localizer)
        {
            // Check if the map is already on cooldown due to being recently played
            if (_mapCooldown != null && _mapCooldown.IsMapInCooldown(map.ToLower()))
            {
                player?.PrintToChat(_localizer.LocalizeWithPrefix("nominate.map-on-cooldown", map, _mapCooldown.InCoolDown));
                return ""; // Return empty string to indicate that the map cannot be nominated
            }

            // Check for maps containing the provided map name
            var matchingMaps = this.Maps
                .Where(x => x.Name.ToLower().Contains(map.ToLower()))
                .ToList();

            if (matchingMaps.Count == 0)
            {
                player?.PrintToChat(_localizer.LocalizeWithPrefix("general.invalid-map"));
                return ""; // Return empty string if no matching maps found
            }
            else if (matchingMaps.Count > 1)
            {
                player?.PrintToChat(_localizer.LocalizeWithPrefix("nominate.multiple-maps-containing-name"));
                return ""; // Return empty string if multiple matching maps found
            }

            // Return the name of the single matching map
            return matchingMaps[0].Name;
        }

        // Method to nominate a map
        public void NominateMap(string mapName)
        {
            var map = Maps.FirstOrDefault(x => x.Name.ToLower() == mapName.ToLower());
            if (map != null && !NominatedMaps.Contains(map))
            {
                NominatedMaps.Add(map);
            }
        }

        // Method to print nominated maps to the client
        public void PrintNominatedMapsToClient(CCSPlayerController player)
        {
            if (NominatedMaps.Count == 0)
            {
                player?.PrintToChat("No maps have been nominated yet.");
                return;
            }

            player?.PrintToChat("Nominated Maps:");
            for (int i = 0; i < Math.Min(NominatedMaps.Count, 6); i++)
            {
                player?.PrintToChat($"{i + 1}. {NominatedMaps[i].Name}");
            }
        }
    }
}
