using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CounterStrikeSharp.API.Core;
using CounterStrikeSharp.API.Events;

namespace RockTheVotePlugin.Features
{
    public class NominationListCommand : ICommand
    {
        public string Name => "nomlist";
        public string Description => "Lists the maps nominated by players.";
        public CommandFlags Flags => CommandFlags.Server;

        public void Execute(ICommandArguments arguments)
        {
            if (!RockTheVotePlugin.IsVoteInProgress)
            {
                arguments.Player.SendMessage("There is no vote in progress.");
                return;
            }

            var nominatedMaps = RockTheVotePlugin.GetNominatedMaps();
            if (nominatedMaps.Count == 0)
            {
                arguments.Player.SendMessage("No maps have been nominated.");
                return;
            }

            var sb = new StringBuilder();
            sb.AppendLine("Nominated maps:");
            foreach (var map in nominatedMaps)
            {
                sb.AppendLine($"- {map.Name} ({map.VoteCount} votes)");
            }
            arguments.Player.SendMessage(sb.ToString());
        }
    }
}
