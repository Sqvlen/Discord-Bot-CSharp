using System;
using System.Threading.Tasks;
using Discord;
using Discord.WebSocket;

namespace DiscordBot
{
    class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            DiscordSQB discord = new DiscordSQB();
            discord.Start();
        }

    }

    public class DiscordSQB
    {
        private string Token { get; } = "";
        private DiscordSocketClient _client;


        public void Start()
            => new DiscordSQB().StartAsync().GetAwaiter().GetResult();

        private async Task StartAsync()
        {
            _client = new DiscordSocketClient();
            _client.MessageReceived += OnMessageHandler;
            _client.Log += Logs;

            await _client.LoginAsync(TokenType.Bot, Token);
            await _client.StartAsync();

            Console.ReadLine();
        }

        private Task Logs(LogMessage arg)
        {
            Console.WriteLine(arg.ToString());
            return Task.CompletedTask;
        }

        private Task OnMessageHandler(SocketMessage message)
        {
            if (!message.Author.IsBot)
                switch (message.Content)
                {
                    case "!hello":
                        message.Channel.SendMessageAsync($"<@{message.Author.Id}>, hi!");
                        break;
                }
            return Task.CompletedTask;
        }
    }
}