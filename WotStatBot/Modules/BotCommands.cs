using System.Threading.Tasks;
using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;

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
    }
}
