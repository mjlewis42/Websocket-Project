const dbexport = require("../exports/databaseExport");
const { io } = require("../SocketServer");

module.exports.run = async (bot, message, args) => {
	var discordID = message.member.id;
    var discordName = message.author.tag;
    var userId = args[0];
	
    io.sockets.sockets.forEach((socket) => {
        if(socket.id === userId){
            socket.disconnect(true);
            message.reply("socket found ");
        }
    });
};

module.exports.help = {
  name: "disconnect"
}