using Discord;
using Discord.Interactions;
using Discord.Net;
using Discord.WebSocket;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Discord.Commands;
using System.Timers;

namespace Bot_Discord
{

    internal class Program
    {
        public static Task Main(string[] args) => new Program().MainAsync();
        private DiscordSocketClient _client;
        public List<string> connessi = new List<string>();
        public async Task MainAsync()
        {
            _client = new DiscordSocketClient();

            _client.Log += Log;

            _client.MessageReceived += ClientOnMessageReceived;
            _client.UserVoiceStateUpdated += UserVoiceUpdate;

            //  You can assign your bot token to a string, and pass that in to connect.
            //  This is, however, insecure, particularly if you plan to have your code hosted in a public repository.
            var token = "";

            // Some alternative options would be to keep your token in an Environment Variable or a standalone file.
            // var token = Environment.GetEnvironmentVariable("NameOfYourEnvironmentVariable");
            // var token = File.ReadAllText("token.txt");
            // var token = JsonConvert.DeserializeObject<AConfigurationClass>(File.ReadAllText("config.json")).Token;

            await _client.LoginAsync(TokenType.Bot, token);
            await _client.StartAsync();

            // Block this task until the program is closed.
            await Task.Delay(-1);
        }
        private Task Log(LogMessage msg)
        {

            Console.WriteLine(msg.ToString());
            return Task.CompletedTask;
        }
        private async Task UserVoiceUpdate(SocketUser user, SocketVoiceState curVoiceState, SocketVoiceState nextVoiceState)
        {
            if (user is SocketGuildUser guildUser)
            {
                //Console.WriteLine(user.Username);
                Console.WriteLine(guildUser.DisplayName + " Da: " + curVoiceState + " A: " + nextVoiceState);

                if (!connessi.Contains(guildUser.DisplayName))
                {
                    connessi.Add(guildUser.DisplayName);
                    foreach (string utonti in connessi)
                    {
                        Console.WriteLine(utonti);
                    }
                }
                if (nextVoiceState.ToString() == "Unknown")
                {
                    connessi.Remove(guildUser.DisplayName);
                    foreach (string utonti in connessi)
                    {
                        Console.WriteLine(utonti);
                    }
                }
            }
        }
        private async Task ClientOnMessageReceived(SocketMessage arg)
        {
            if (arg.Content.StartsWith("!Controllo"))
            {
                foreach (SocketRole role in ((SocketGuildUser)arg.Author).Roles)
                {
                    if (role.Id == ) //Insert ID of controller role
                    {
                        foreach (string utonti in connessi)
                        {
                            arg.Channel.SendMessageAsync(utonti).Wait();
                        }
                    }
                }
                //arg.Channel.SendMessageAsync($"User '{arg.Author.Username}' successfully ran helloworld!");
            }
            //return Task.CompletedTask;
        }

    }
}   
