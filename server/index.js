var app = require('express')();
var http = require('http').createServer(app);
var io = require('socket.io')(http);

app.get('/', function(req, res){
  //res.sendFile(__dirname + '/index.html');
});

//TODO Handler de mensajes
//Cuando te llegue el mensaje de login conectar a DB, crear usuario si no existe
//Cuando te llegue el mensaje de fin conectar a DB, +1Victorias, Comparar BestTime, si mejor, cambiar.

io.on('connection', function(socket){
	console.log('a user connected');
	socket.on('login', function(data){
		console.log(data+' logged');
		if(data==""){
			socket.emit('noUsername');
		}
		else{
			socket.emit('logged');
		}
	});
	socket.on('gameOver', function(data){
		console.log("gameOver");
	});
	socket.on('userPosition', function(data){
		socket.emit('rivalPosition',{ speed: data });
	});
});

http.listen(3000, function(){
  console.log('listening on *:3000');
});





