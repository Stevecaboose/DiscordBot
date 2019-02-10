# DiscordBot
A bot for the Discord client written in C# with various features

# Requirements
- Discord.Net NuGet package by Discord.Net Contributors including RogueException

- Newtonsoft.Json NuGet package by James Newton-King

These can be installed by opening your NuGet Package Manager and clicking on restore on the top

# Setup
After compilation and the first execution, you must edit the bin\Debug\Resources\config.json file. There is one object with four fields. These must all be filled out. Note that the bot will generate this file after the first execution. This is what the json file looks like. If you wanted to, you can make this file and place it in the resources directory.

```
{
  "token": "DISCORD TOKEN",
  "cmdPrefix": "CHARACTER USED TO DENOTE A COMMAND",
  "guildID": GUILD ID (ID FOR SERVER),
  "textChannel": TEXT CHANNNEL ID (ID FOR SPECIFIC CHANNEL)

}
```

