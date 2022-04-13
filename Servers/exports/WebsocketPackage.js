const dbexport = require("./databaseExport");
const { io } = require("../SocketServer");

var clientPackage;

var packageSenderCallback = function(packageType, tokenId, message, discordID, callback){
    var sql0 = "SELECT * FROM users WHERE DiscordId = " + discordID;

    switch (packageType) {
        case 'send_characters':
            dbexport.pool.query(sql0, function (err0, result0) {
                //change scenes + send character data
                clientPackage = {
                    packageType : "chardata",
                    socketData : [{
                        socketId : tokenId
                    }],
                    dataPack : [{
                        id : result0[0].Id,
                        discordName : result0[0].DiscordName,
                        discordId : result0[0].DiscordId,
                        patreon : result0[0].Patreon,
                        char1 : result0[0].Char_Slot_1,
                        char2 : result0[0].Char_Slot_2,
                        char3 : result0[0].Char_Slot_3,
                        char4 : result0[0].Char_Slot_4
                    }]
                };
                io.to(tokenId).emit("package", clientPackage);
            });
            break;

        case 'char_creation':
            message.reply("Entering Character Creation. " + tokenId);
            clientPackage = {
                packageType : "charcreation"
            };
            io.to(tokenId).emit("package", clientPackage);
            break;
            
        default:
            console.log("ERROR, not a correct message type.");
    }

    callback(true);
};

module.exports = {packageSenderCallback};