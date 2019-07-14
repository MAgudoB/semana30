using UnityEngine;
using SocketIO;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

public class LoginScript : MonoBehaviour {

    SocketIOComponent socket;
    InputField username;
    UserJson myObject;

    [Serializable]
    public class UserJson {
        public float speed;
        public string playerName;
    }

    void Start() {
        DontDestroyOnLoad(this);
        myObject = new UserJson();
        socket = gameObject.GetComponent<SocketIOComponent>();
        socket.On("logged", OnLogin);
        socket.On("noUsername", OnLoginFailed);
        socket.On("rivalPosition", OnRivalPosition);
    }
    
    public void LogIn() {
        username = GameObject.FindGameObjectWithTag("nameInput").GetComponent<InputField>();
        socket.Emit("login", JSONObject.CreateStringObject(username.text));
        
    }

    public void GameOver() {
        socket.Emit("gameOver");
    }

    public void PlayerPosition(float speed) {
        myObject.speed = speed;
        myObject.playerName = username.text;
        string json = myObject.speed.ToString();
        socket.Emit("userPosition", JSONObject.CreateStringObject(json));
    }

    private void OnLogin(SocketIOEvent e) {
        Debug.Log("User logged correctly");
        SceneManager.LoadScene("HorseRacing");
    }

    private void OnLoginFailed(SocketIOEvent e) {
        Debug.Log("Please input a username");
    }

    private void OnRivalPosition(SocketIOEvent obj) {
        myObject = JsonUtility.FromJson<UserJson>(obj.data.ToString());
        GameObject rival = GameObject.FindGameObjectWithTag("enemy");
        EnemyController rivalController = rival.GetComponent<EnemyController>();
        if (myObject.speed - 1 >= 0) {
            rivalController.speed = myObject.speed - 1;
        } else {
            rivalController.speed = 1;
        }
    }

    
}