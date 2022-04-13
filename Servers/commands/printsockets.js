const dbexport = require("../exports/databaseExport");
const { io } = require("../SocketServer");

module.exports.run = async (bot, message, args) => {
    var discordID = message.member.id;
    var discordName = message.author.tag;
    const ids = await io.allSockets();

    message.reply("Number of users: " + ids.size);
};

module.exports.help = {
  name: "sockets"
}