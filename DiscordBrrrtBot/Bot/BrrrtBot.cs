using Discord;
using Discord.Commands;
using Discord.WebSocket;
using DiscordBrrrtBot.Logger;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DiscordBrrrtBot.Bot
{
    public class BrrrtBot
    {
        public DiscordSocketClient Client { get; private set; }
        public CommandService Commands { get; private set; }
        public IServiceProvider Services { get; private set; }

        public async Task RunBotAsync()
        {
            Console.WriteLine("Initiating Services");
            Client = new DiscordSocketClient();
            Commands = new CommandService();
            Services = new ServiceCollection()
                .AddSingleton(Client)
                .AddSingleton(Commands)
                .BuildServiceProvider();

            Console.WriteLine("Services initialised");

            Console.WriteLine("Setting up logger...");
            Client.Log += ClientLogger.LogClient;
            Console.WriteLine("Logs setup");

            Console.WriteLine("Registering Commands...");
            await RegisterCommandsAsync();
            Console.WriteLine("Commands registered");

            Console.WriteLine("Logging in...");
            await Client.LoginAsync(TokenType.Bot, Global.BOT_TOKEN);
            Console.WriteLine("Successfully logged in");

            Console.WriteLine("Starting BrrrtBot...");
            await Client.StartAsync();

            await Task.Delay(-1);

            Console.WriteLine("Shutting down");

            return;
        }

        public async Task RegisterCommandsAsync()
        {
            Client.MessageReceived += OnClientMessageReceivedAsync;
            await Commands.AddModulesAsync(Assembly.GetEntryAssembly(), Services);
        }

        public async Task OnClientMessageReceivedAsync(SocketMessage arg)
        {
            if (arg is SocketUserMessage message)
            {
                SocketCommandContext context = new SocketCommandContext(Client, message);

                if (message.Author.IsBot) return;

                int argPos = 0;

                Console.WriteLine($"New message-> '{message.Content}'");

                if (message.HasStringPrefix(@"!!", ref argPos))
                {
                    Console.WriteLine($"{DateTime.Now}: COMMAND INPUT -> '{message.Content}'");

                    IResult result = await Commands.ExecuteAsync(context, argPos, Services);

                    if (!result.IsSuccess)
                    {
                        Console.WriteLine($"Error: {result.ErrorReason}");
                    }
                }

                if (message.HasStringPrefix(@">", ref argPos))
                {
                    Console.WriteLine($"{DateTime.Now}: >LIFE COMMAND -> '{message.Content}'");

                    switch (message.Content.Substring(1, message.Content.Length - 1))
                    {
                        case "mk2":
                            await context.Channel.SendFileAsync("Images/e.jpg", ">when mk2");
                            break;
                        case "hax":
                            await context.Channel.SendMessageAsync(">when u hak but ur bad\n" +
                                "https://www.youtube.com/watch?v=rv4RAocf_uk");
                            break;
                    }
                }
            }
        }
    }
}
