const { Server } = require("socket.io");
const port = 8080;
var clientPackage;

const clientList = new Map();
const io = new Server(port, { 
    cors: {
        origin: ["http://localhost:8080"]
    }
});


io.on("connection", (socket) => {
    console.log(socket.id + " has JOINED the server.");

    socket.on("disconnect", (reason) => {
        socket.disconnect();
        if(clientList.has(socket.id)){
            clientList.delete(socket.id);
            console.log(socket.id + " was remove from clientList");
        }
        console.log(socket.id + " has LEFT the server. " + reason);
    });

    socket.on("error", (err) => {    
        if (err && err.message === "unauthorized event") {      
            socket.disconnect();    
            console.log("Error at " +socket.id + " | " + err);
        }  
    });

    clientPackage = {
        packageType : "connection",
        socketData : [{
           socketId : socket.id
        }]
     };

    socket.emit("package", clientPackage);
});

module.exports = {io, clientList};
