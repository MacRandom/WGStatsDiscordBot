using System;
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

        [Command("stat")]
        public async Task GetStat(CommandContext context, string name)
        {
            var api = context.Dependencies.GetDependency<WargamingApi>();

            var player = await api.GetPlayer(name);

            var playerStat = await api.GetStat(player.Id);

            await context.TriggerTypingAsync();

            await context.RespondAsync(
                $"Статистика игрока {player.Name} в случайных боях{Environment.NewLine}" +
                $"Провёл {playerStat.BattleCount} боёв, из них побед - {playerStat.WinCount}, ничьих - {playerStat.DrawCount}, поражений - {playerStat.LoseCount}{Environment.NewLine}" +
                $"Выжил в {playerStat.SurviveCount} боях{Environment.NewLine}" +
                $"Процент побед {(float)playerStat.WinCount * 100 / playerStat.BattleCount}%{Environment.NewLine}" +
                $"Средние показатели за бой: {Environment.NewLine}" +
                $"Средний урон: {(float)playerStat.DamageDealt / playerStat.BattleCount}{Environment.NewLine}" +
                $"Средний ассист: {playerStat.AverageDamageAssisted}{Environment.NewLine}" +
                $"Средний засвет: {playerStat.AverageDamageAssistedRadio}{Environment.NewLine}" +
                $"Среднее гусление: {playerStat.AverageDamageAssistedTrack}{Environment.NewLine}" +
                $"Процент попаданий: {playerStat.HitsPercent}%{Environment.NewLine}" +
                $"Убил врагов: {playerStat.FragsCount}{Environment.NewLine}" +
                $"Убил деревьев: {playerStat.TreesCut}{Environment.NewLine}");
        }
    }
}
