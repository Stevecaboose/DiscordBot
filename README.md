# DiscordBot
A bot for the Discord client written in C# with various features

# Requirements
- Visual Studio 2017

- .NET Framework 4.6.1

- Discord.Net NuGet package by Discord.Net Contributors including RogueException

- Newtonsoft.Json NuGet package by James Newton-King

These can be installed by opening your NuGet Package Manager and clicking on restore on the top

# Setup
After compilation and the first execution, you must edit the bin\Debug\Resources\config.json file. There is one object with six fields. These must all be filled out. Note that the bot will generate this file after the first execution. This is what the json file looks like. If you wanted to, you can make this file and place it in the resources directory.

```
{
  "token": <STRING>DISCORD TOKEN, 
  "cmdPrefix": <STRING>CHARACTER USED TO DENOTE A COMMAND,
  "guildID": <INTEGER>GUILD ID (ID FOR SERVER),
  "textChannel": <INTEGER>TEXT CHANNNEL ID (ID FOR SPECIFIC CHANNEL),
  "XPGain": <INTEGER>HOW MUCH XP IS GAINED PER MESSAGE SENT
  "levelMessageTimeout": <INTEGER>HOW MANY MINUTES BEFORE A USER CAN GAIN XP AFTER SENDING A MESSAGE
  "pruneDaysForTooManyWarnings": <INTEGER>HOW MANY DAYS OF MESSAGES ARE REMOVED AFTER A BAN DUE TO WARNINGS,
  "warningsBeforeBan": <INTEGER>INCLUSIVE NUMBER OF WARNINGS BEFORE AN AUTOMATIC BAN 
}
```

