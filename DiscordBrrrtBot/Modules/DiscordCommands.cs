using Discord;
using Discord.Commands;
using Discord.WebSocket;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace DiscordBrrrtBot.Modules
{
    /// <summary>
    /// A class that handles commands by users
    /// </summary>
    public class DiscordCommands : ModuleBase<SocketCommandContext>
    {
        public const char NewLine = '\n';
        [Command("help")]
        public async Task DisplayHelp()
        {
            await ReplyAsync(
                "```\n" +
                $"-----   dont include the <>'s ----- \n" + 
                $"-----   and.. use !! to do commands ----- \n" + 
                $"help          -  Displays help...?\n" + 
                $"echo <input>  -  Outputs what you say. useless xdd\n" + 
                $"kick <user>   -  kicks some nob out of the party xd\n" + 
                $"-----   Actual (totally useful) commands ----- \n" + 
                $"brrrt         -  BRRRRRRRRRRRRRRRRRRRRT\n" +
                $"muie=bad      -  calls muie bad loxd\n" +
                $"reddit        -  posts the link to the subreddit\n" + 
                $"microwave     -  microwave\n" +
                $"-----   ahahah funny commands lolxd ----- \n" + 
                $"-----   use > prefix xd ----- \n" + 
                $"mk2           - idk when mk2 is clueless \n" + 
                $"hax           - when u tri 2 hak but no\n" +
                "```"
                );
        }

        #region Okayish commands

        [Command("echo")]
        public async Task EchoMessage(string messageToEcho)
        {
            await ReplyAsync(messageToEcho, true);
        }

        [Command("kick")]
        [RequireBotPermission(GuildPermission.KickMembers)]
        public async Task KickUser(IGuildUser user)
        {
            SocketGuildUser GuildUser = Context.Guild.GetUser(Context.User.Id);

            if (!GuildUser.GuildPermissions.KickMembers)
            {
                await Context.Message.DeleteAsync();
                await ReplyAsync("`cant kick em. youre not admin or something idk`");
                return;
            }
            else
            {
                await user.KickAsync();
                await Context.Channel.SendMessageAsync(
                    $"`{user.Username} has been booted out the party`");
            }
        }

        #endregion

        #region Useful commands xddd

        [Command("brrrt")]
        public async Task Brrrt()
        {
            await Context.Channel.SendFileAsync("Images/brrrt.png", "BRRRRRRRRRRRRRRRRRRRRT");
        }

        [Command("muie=bad")]
        public async Task MuieBadXD()
        {
            await ReplyAsync("you missed 134 shots before finally hitting a mk2 in the air.\n"+
                "XDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDD (jk)");
        }

        [Command("reddit")]
        public async Task PostRedditLink()
        {
            await ReplyAsync("https://www.reddit.com/r/BrrrtForever/");
        }

        [Command("microwave")]
        public async Task Microwave()
        {
            await ReplyAsync("MMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMM");
            await Task.Delay(4000);
            await ReplyAsync("BEEEEEP");
            await Task.Delay(1000);
            await ReplyAsync("BEEEEEP");
            await Task.Delay(1000);
            await ReplyAsync("BEEEEEP");
            await Task.Delay(1000);
        }

        #endregion





        [Command("delsystem32")]
        public async Task ShutdownBot()
        {
            await ReplyAsync("no");
            //await ReplyAsync("rip shuttin' down");
            //await Context.Client.StopAsync();
        }
        [Command("startup")]
        public async Task StartupBot()
        {
            //await Context.Client.StartAsync();
            //await ReplyAsync("online xd");
            await ReplyAsync("no");
        }
    }
}
