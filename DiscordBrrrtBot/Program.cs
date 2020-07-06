using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using Discord.WebSocket;
using DiscordBrrrtBot.Bot;
using Microsoft.Extensions.DependencyInjection;

namespace DiscordBrrrtBot
{
    class Program
    {
        static void Main(string[] args)
        {
            BrrrtBot bot = new BrrrtBot();
            bot.RunBotAsync().GetAwaiter().GetResult();

            return;
        }
    }
}
