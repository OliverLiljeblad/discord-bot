using Discord;
using Discord.WebSocket;

Console.WriteLine("DiscordBot starting");

int number = 0;

bool HasGameStarted = false;

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

    if (message.Content == "!help")
        await message.Channel.SendMessageAsync("Commands: \n !help \n !play \n !stop \n !creator");

    if (message.Content == "!creator")
    {
        await message.Channel.SendMessageAsync("Oliver#2191");
    }

    if (message.Content == "!play") 
    {
        await message.Channel.SendMessageAsync("Shall we play a game? \n I think of a number between 1-100");
        HasGameStarted = true; 
        Random rnd = new Random();
        number = rnd.Next(0, 101);
        return;
    }

    if (message.Content == "!stop")
        HasGameStarted = false;

if(HasGameStarted == true) {
    if (int.Parse(message.Content) > number && HasGameStarted == true) 
        await message.Channel.SendMessageAsync("Lower");
    
    if (int.Parse(message.Content) < number && HasGameStarted == true)
        await message.Channel.SendMessageAsync("Higher");

    if (int.Parse(message.Content) < 0 || int.Parse(message.Content) > 100 && HasGameStarted == true)
        await message.Channel.SendMessageAsync("The number is between 1 - 100");

    if (int.Parse(message.Content) == number && HasGameStarted == true) 
    {
        await message.Channel.SendMessageAsync("Correct, well played!");
        HasGameStarted = false;
        return;
    }
 }
}