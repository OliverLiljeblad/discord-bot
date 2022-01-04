using Discord;
using Discord.WebSocket;

Console.WriteLine("DiscordBot starting");

var client = new DiscordSocketClient();
client.Log += LogAsync;
client.Ready += ReadyAsync;
client.MessageReceived += MessageReceivedAsync;

var token = Environment.GetEnvironmentVariable("Discord-Bot-Token");
Console.WriteLine($"Token: {token}");

await client.LoginAsync(TokenType.Bot, token);
await client.StartAsync();
await Task.Delay(Timeout.Infinite);

Task LogAsync(LogMessage log)
{
    Console.WriteLine("Nu loggar jag");
    Console.WriteLine(log.ToString());
    return Task.CompletedTask;
}

Task ReadyAsync()
{
    Console.WriteLine($"{client.CurrentUser} is connected!");
    
    return Task.CompletedTask;
}

async Task MessageReceivedAsync(SocketMessage message)
{
    Console.WriteLine(message.Content);

    if (message.Author.Id == client.CurrentUser.Id)
    return;

    if (message.Content == "!ping")
      await message.Channel.SendMessageAsync("pong!");
}