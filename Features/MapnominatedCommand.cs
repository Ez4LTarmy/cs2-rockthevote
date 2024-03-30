using System;
using CounterStrikeSharp.API;
using CounterStrikeSharp.API.Core;
using CounterStrikeSharp.API.Events;

namespace cs2_rockthevote.Features
{
    public class NominationListCommand : BaseCommand
    {
        public override string Name => "nomlist";

        public override CommandFlags Flags => CommandFlags.Player;

        public override void Execute(ICommandArguments arguments)
        {
            if (arguments.Player == null)
            {
                return;
            }

            var nominatedMaps = RockTheVotePlugin.NominatedMaps;

            if (nominatedMaps.Count == 0)
            {
                arguments.Player.SendMessage("No maps have been nominated.");
            }
            else
            {
                var nominationsString = string.Join(", ", nominatedMaps.Keys);
                arguments.Player.SendMessage($"Nominated maps: {nominationsString}");
            }
        }
    }
}
