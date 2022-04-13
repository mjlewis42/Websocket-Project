const { io } = require("../SocketServer");
const accountCheck = require("../exports/accountCheck");
const WebsocketPackage = require("../exports/WebsocketPackage");
const dbexport = require("../exports/databaseExport");

module.exports.run = async (bot, message, args) => {
    var discordID = message.member.id;
    var discordName = message.author.tag;
    var tokenId = args[0];
    var packageType = "send_characters";
    var sql0 = "SELECT DiscordId FROM users WHERE DiscordId = " + "'" + tokenId + "'";
    var sql1 = "UPDATE users SET SessionToken = " + "'" + tokenId + "'" +", Status = 'char_select' WHERE DiscordId = " + discordID;
    var sql2 = "SELECT * FROM users WHERE DiscordId = " + discordID;

    var newGame = false;
    accountCheck.accountCheckCallback(discordID, discordName, message, newGame, function(callbackValue){
        if(callbackValue){
            dbexport.pool.query(sql0, function (err0, result0) {
                if(result0.length == 0 ){
                    io.sockets.sockets.forEach((socket) => {
                        if(socket.id === tokenId && socket.id != discordID){
                            socket.id = discordID;

                            dbexport.pool.query(sql1, function (err1, result1) {
                                if(!err1){
                                    dbexport.pool.query(sql2, function (err2, result2) {
                                        //change scenes + send character data
                                        WebsocketPackage.packageSenderCallback(packageType, tokenId, message, discordID, function(callback){
                                            if(callback){
                                                message.reply("Successfully linked to game client!");  
                                            }
                                        });
                                    });
                                }
                                else{
                                    console.log("ERROR setting sessiontoken + status in connection command " + err1);
                                }
                            });
                        }
                        else if(socket.id == discordID){
                            //user already has active websocket
                            message.reply("Your session key is invalid.  Try pasting it again or restarting / disconnecting your game client to recieve a new one.");
                        }
                    }); 
                }
                else{
                    message.reply("Your session key is invalid.  Try pasting it again or restarting / disconnecting your game client to recieve a new one.");
                }
            });
        }
    });
};

module.exports.help = {
  name: "connect"
}
