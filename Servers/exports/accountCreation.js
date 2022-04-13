const dbexport = require("./databaseExport");

function runAccountCreation(discordID, discordName, callbackValue){
    var sql0 = 'INSERT INTO users (DiscordName, DiscordId) VALUES (?,?)';

    dbexport.pool.query(sql0, [discordName, discordID], function (err, result0) {
        if(!err){
            console.log("Account successfully Created!");
            callbackValue(true);
        }
        else{
            console.log("Account creation ERROR.  Please try again later using the (!startgame) command. " + err);
        }
    }); 
}

module.exports = {runAccountCreation};