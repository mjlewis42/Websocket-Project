const discord = require("discord.js");
const { Client, Intents } = require('discord.js');
const fs = require("fs");
const config = require("./botconfig.json");
const io = require("./SocketServer");
const express = require("./ExpressServer");
const bot = new Client({ intents: [Intents.FLAGS.GUILDS, Intents.FLAGS.GUILD_MESSAGES, Intents.FLAGS.DIRECT_MESSAGES], partials: ["MESSAGE", "CHANNEL", "REACTION"] });


bot.on("ready", async () => {
  console.log(`${bot.user.username} is ready for action!`);
  bot.user.setActivity("(^help)");

});

bot.commands = new discord.Collection();

fs.readdir("./commands/", (err, files) => {
  if (err) console.error(err);
  let jsfiles = files.filter(f => f.split(".").pop() === "js");

  if (jsfiles.length <= 0) return console.log("There are no commands to load...");

  console.log(`Loading ${jsfiles.length} commands...`);
  jsfiles.forEach((f, i) => {
    let props = require(`./commands/${f}`);
    console.log(`${i + 1}: ${f} loaded!`);
    bot.commands.set(props.help.name, props);
  });
});


bot.on("message", async message => {
    if(message.author.bot || message.channel.type === "dm") return;
    if (message.channel.type === "dm") return;

    let prefix = config.prefix;
    let messageArray = message.content.split(" ");
    let command = messageArray[0].toLowerCase();
    let args = messageArray.slice(1);

    if (!command.startsWith(prefix)) return;

    let cmd = bot.commands.get(command.slice(prefix.length));
    if (cmd) cmd.run(bot, message, args);

    if (command === `${prefix}tester`){

    }
});

bot.login(config.token);