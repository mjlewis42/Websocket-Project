const express = require('express');
const fetch = require('node-fetch');
const accountCheck = require("./exports/accountCheck");
const { io } = require("./SocketServer");
const socketServer = require("./SocketServer");
var port = 3000;
var userObj;
var discordId;
var discordName;
var discordAvatarHash;
var discordAvatar;

const app = express();
app.use(express.json());
app.get('/', async ({query, request}, response) => {
	const { code } = query;
	if (code) {
		try {
			const oauthResult = await fetch('https://discord.com/api/oauth2/token', {
				method: 'POST',
				body: new URLSearchParams({
					client_id: "910394836186701834",
					client_secret: "JTC5Fl6wyvU5tmKoGDofP07ODnM2Wkq1",
					code,
					grant_type: 'authorization_code',
					redirect_uri: "http://64.138.225.33:3000",
					scope: 'identify', 
					state: ''
				}),
				headers: {
					'Content-Type': 'application/x-www-form-urlencoded',
				},
			});

			const oauthData = await oauthResult.json();
			const userResult = await fetch('https://discord.com/api/users/@me', {
				headers: {
					authorization: `${oauthData.token_type} ${oauthData.access_token}`,
				},
			});
			userObj = await userResult.json();
			discordId = userObj.id;
			discordName = userObj.username + "#" + userObj.discriminator;
			discordAvatarHash = userObj.avatar;
			discordAvatar = "https://cdn.discordapp.com/avatars/" + discordId + "/" + discordAvatarHash + ".png";

			let loginPromise = new Promise(function(resolve, reject) {
				socketServer.clientList.forEach((value, key, map) => {
					if(value == discordId){
						reject(false);
					}	
				});
				resolve(true);
			});

			loginPromise.then(
				function(value) { 
					console.log("user was NOT found and should be allowed to login: " + value + " | " +  discordId); 
					socketServer.clientList.set(query.state, discordId);

					let socketPromise = new Promise(function(resolve, reject) {
						io.sockets.sockets.forEach((socket) => {
							if(socket.id == query.state){
								resolve(socket);
							}
						});
						reject();
					});

					socketPromise.then(
						function(socket) { 
							response.sendFile('index.html', { root: '.' });
							console.log("socket was found: " + value); 
							accountCheck.accountCheckCallback(discordId, discordName, function(callbackValue){
								if(callbackValue){
									var authorizedUser = {
										packageType : "authorizedUser",
										socketData : [{
											socketId : socket.id
										}],
										dataPack : [{
											discordId : discordId,
											discordName : discordName,
											discordAvatar : discordAvatar,
											discordUserBalance: callbackValue
										}]
									};
									io.sockets.to(socket.id).emit("package", authorizedUser);
								}
								else{
									console.log("Error encountered during login callback.  Send error message display");
								}
							});
						},
						function(error) { 
							console.log("socket was NOT found: " + error); 
						}
					);
				},
				function(error) {
					console.log("user was found and SHOULD NOT be allowed to login again: " + error + " | " +  discordId); 
				}
			);

		} catch (error) {
			console.error(error);
		}
	}
	else{
		response.sendFile('errorPage.html', { root: '.' });
	}
});

app.listen(port, () => console.log(`App listening at http://localhost:${port}`));

