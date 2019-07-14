using UnityEngine;
using SocketIO;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

public class LoginScript : MonoBehaviour {

    SocketIOComponent socket;
    InputField username;

    void Start() {
        DontDestroyOnLoad(this);
        socket = gameObject.GetComponent<SocketIOComponent>();
        socket.On("logged", OnLogin);
        socket.On("noUsername", OnLoginFailed);
    }

    public void LogIn() {
        username = GameObject.FindGameObjectWithTag("nameInput").GetComponent<InputField>();
        socket.Emit("login", JSONObject.CreateStringObject(username.text));
        
    }

    private void OnLogin(SocketIOEvent e) {
        Debug.Log("User logged correctly");
        SceneManager.LoadScene("HorseRacing");
    }

    private void OnLoginFailed(SocketIOEvent e) {
        Debug.Log("Please input a username");
    }

    public  void GameOver() {
        socket.Emit("gameOver");
    }
}