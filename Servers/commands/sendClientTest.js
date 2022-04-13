const { io } = require("../SocketServer");
const accountCheck = require("../exports/accountCheck");

module.exports.run = async (bot, message, args) => {
  var tokenId = args[0];

  dataPackage = JSON.stringify({
    packageType : "connect",
    socketData : [{
        socketId : "test message"
    }]
  });

  message.reply("Sending message to socket..");
  io.sockets.to(hold).emit("package", dataPackage);
};

module.exports.help = {
  name: "sendclient"
}
