using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour {
    public float maxSpeed = 15;
    public float speed = 0;

    public Animator anim;

    // Start is called before the first frame update
    void Start() {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update() {
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        anim.speed = speed / maxSpeed;
        rb.velocity = new Vector2(speed, 0.0f);
    }
}
