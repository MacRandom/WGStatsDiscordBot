using System.Threading.Tasks;
using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using WotStatBot.Models;
using WotStatBot.Wargaming;

namespace WotStatBot.Modules
{
    public class BotCommands
    {
        [Command("alive")]
        [Description("Bot is runnung test command")]
        public async Task Alive(CommandContext context)
        {
            await context.TriggerTypingAsync();

            await context.RespondAsync("I'm alive!");
        }

        [Command("get")]
        public async Task GetPlayer(CommandContext context, string name)
        {
            Player player = await context.Dependencies.GetDependency<WargamingApi>().GetPlayer(name);

            await context.TriggerTypingAsync();

            await context.RespondAsync($"Name: {player.Name}, Id: {player.Id}");
        }
    }
}
