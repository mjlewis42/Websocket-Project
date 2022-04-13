const dbexport = require("./databaseExport");
const accountCreation = require("./accountCreation");
var value = false;

var accountCheckCallback = function(discordID, discordName, callback){
    var sql0 = 'SELECT * FROM users WHERE DiscordId = ' + discordID;
    
    dbexport.pool.query(sql0, function (err, result0) {
        if(result0.length != 0){
            console.log("User has an account, return additional data");
            callback(result0[0].Balance);
        }
        else{
            console.log("User does not have an account, create one then return data");
            accountCreation.runAccountCreation(discordID, discordName, function(callbackValue){
                if(callbackValue){
                    //return additional data
                    callback(result0[0].Balance);
                }
            });
        }
    }); 
}

module.exports = {accountCheckCallback};