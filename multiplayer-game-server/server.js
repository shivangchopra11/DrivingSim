var uuid = require('uuid-random')
const WebSocket = require('ws')

var clients = []

const wss = new WebSocket.WebSocketServer({port: 8080}, () => {
    console.log('Server Started')
})

var playersData = {
    "type": "playerData"
}

wss.on('connection', function connection(client) {
    client.id = uuid();

    console.log(`Client ${client.id} Connected!`)

    var currentClient = playersData[""+client.id]

    clients.push(client)
    

    //Send default client data back to client for reference
    client.send(`{"id": "${client.id}"}`)

    //Method retrieves message from client
    client.on('message', (data) => {        
        var dataJSON = JSON.parse(data)

        // console.log("Player Message")
        // console.log(dataJSON)
        // clients.forEach((c) => {
        //     console.log(c.id)
        //     c.send(JSON.stringify(dataJSON))
        // })
        wss.broadcast(JSON.stringify(dataJSON));

    })

    //Method notifies when client disconnects
    client.on('close', () => {
        console.log('This Connection Closed!')
        console.log("Removing Client: " + client.id)
    })

})

wss.on('listening', () => {
    console.log('listening on 8080')
})

wss.broadcast = function broadcast(msg) {
    wss.clients.forEach(function each(client) {
        if (client.id != msg.id)
            client.send(msg)
    })
}