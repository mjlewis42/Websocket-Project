const dbexport = require("../exports/databaseExport");
const { io } = require("../SocketServer");

module.exports.run = async (bot, message, args) => {
	var discordID = message.member.id;
    var discordName = message.author.tag;

	io.sockets.sockets.forEach((socket) => {
        if(socket.id === args[0]){
            message.reply("socket found ");
        }
    });
};

module.exports.help = {
  name: "findsocket"
}