var app = require('express')();
var http = require('http').createServer(app);
var io = require('socket.io')(http);
const sqlite3 = require('sqlite3').verbose();

app.get('/', function(req, res){
  //res.sendFile(__dirname + '/index.html');
});

//TODO Handler de mensajes
//Cuando te llegue el mensaje de login conectar a DB, crear usuario si no existe
//Cuando te llegue el mensaje de fin conectar a DB, +1Victorias, Comparar BestTime, si mejor, cambiar.

io.on('connection', function(socket){
	console.log('socket connected');
    let db = new sqlite3.Database("./semana30.db", 
            sqlite3.OPEN_READWRITE | sqlite3.OPEN_CREATE, 
            (err) => { 
                if (err) {
                    console.log(err.messsage);
                } 
            });
	socket.on('login', function(data){
		console.log(data+' logged');
		if(data==""){
			socket.emit('noUsername');
		}
		else{
			socket.emit('logged');
		}
        var sql = `CREATE TABLE IF NOT EXISTS games (
                     user_name TEXT PRIMARY KEY,
                     number_of_game INTEGER
                    );`
        db.run(sql, (err) => {
                        if (err) {
                            console.log(err.message)
                        } else {
                            console.log("Table games created")
                        }
                    })
	});
	socket.on('gameOver', function(data){
		console.log("gameOver");
        db.close()
	});
	socket.on('userPosition', function(data){
		socket.emit('rivalPosition',{ speed: data });
	});
});

http.listen(3000, function(){
  console.log('listening on *:3000');
});





