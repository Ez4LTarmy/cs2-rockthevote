using CounterStrikeSharp.API;
using CounterStrikeSharp.API.Core;
using CounterStrikeSharp.Events;
using System;

namespace cs2_rockthevote.Features
{
    public class NominationListCommand : ICommand
    {
        public string Name => "nomlist";

        public CommandFlags Flags => CommandFlags.Server;

        public void Execute(ICommandArguments arguments)
        {
            if (arguments.Player == null)
            {
                return;
            }

            var nominatedMaps = RockTheVote.NominatedMaps;

            if (nominatedMaps.Count == 0)
            {
                arguments.Player.SendMessage("No maps have been nominated.");
            }
            else
            {
                var nominationsString = string.Join(", ", nominatedMaps.Select(x => $"{x.Key} ({x.Value})"));
                arguments.Player.SendMessage($"Nominated maps: {nominationsString}");
            }
        }
    }
}
