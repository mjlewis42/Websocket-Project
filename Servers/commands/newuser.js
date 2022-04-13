const accountCheck = require("../exports/accountCheck");

module.exports.run = async (bot, message, args) => {
    var discordID = message.member.id;
    var discordName = message.author.tag;

    var newGame = true;
    accountCheck.accountCheckCallback(discordID, discordName, message, newGame);
};

module.exports.help = {
  name: "newuser"
}