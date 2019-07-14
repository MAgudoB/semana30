using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour {
    public float maxSpeed = 15;
    public float minSpeed = 1f;
    public float speedLowLimit = 7;
    public float speedUpperLimit = 12;
    public float maxEnergy = 1000;
    public float energy = 1000;
    public float speed = 0;
    public float speedInc = 1;
    private float speedDec = 0.05f;
    public Animator anim;
    private GameObject connection;
    private LoginScript connectionScript;
    private bool gameOver = false;

    // Start is called before the first frame update
    void Start() {
        anim = GetComponent<Animator>();
        connection = GameObject.FindGameObjectWithTag("connection");
        connectionScript = connection.GetComponent<LoginScript>();
    }


    // Update is called once per frame
    void Update() {
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        if (rb.IsTouching(GameObject.FindGameObjectWithTag("Finish").GetComponent<Collider2D>()) && !gameOver) {
            // TODO Mandar he acabado al servidor.
            //Pararlo todos
            //gameObject.transform.position;
            speed = 0;
            anim.speed = 0;
            print("He terminado la carrera");
            gameOver = true;
            connectionScript.GameOver();
        } else if (gameOver) {
            speed = 0;
            anim.speed = 0;
        } else {
            bool lash = Input.GetKeyDown("space");
            updateSpeed(lash);
            computePlayerStats();
            anim.speed = speed / maxSpeed;
            rb.velocity = new Vector2(speed, 0.0f);

        }
    }

    private void updateSpeed(bool lash) {
        if (lash && energy > 0) {
            speed = (speed + speedInc) > maxSpeed ? maxSpeed : speed + speedInc;
        } else {
            speed = (speed - speedDec) < minSpeed ? minSpeed : speed - speedDec;
        }
    }

    private void computePlayerStats() {
        if (speed < speedLowLimit && energy < maxEnergy) {
            energy = (energy + (speedLowLimit - speed)) > maxEnergy ? maxEnergy : energy + (speedLowLimit - speed);
        } else if (speed > speedUpperLimit && energy > 0) {
            energy = (energy - (speed - speedUpperLimit)) < 0 ? 0 : (energy - (speed - speedUpperLimit));
        } else {
            energy = energy - 0.1f > 0 ? energy - 0.1f : 0;
        }
    }
}
