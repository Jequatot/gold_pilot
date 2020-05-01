using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scr_enemy : MonoBehaviour
{
    string[] kinds = { "raindrop" };
    public int kind = 0;
    scr_world world;

    // direction the player is moving this frame
    Vector3 speed = new Vector3(0, 0, 0);

    // deltaspeed - how much the speed is changed by with input
    float dspeed = .02f;

    // Start is called before the first frame update
    void Start()
    {
        world = GameObject.Find("Tomato").GetComponent<scr_world>();

        Typify();
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
        Fire();
    }

    void Typify() {
        switch (kind) {
            case 0:
                speed.Set(0, -dspeed, speed.z);
                break;
        }
    }

    void Movement() {
        switch (kind) {
            case 0:
                this.transform.Translate(speed);
                break;
        }
    }
    
    void Fire() {

    }

    void OnTriggerStay2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Projectile") {
            Destroy(collider.gameObject);
            Destroy(gameObject);
        }
        if (collider.gameObject.tag == "Player") {
            world.Damage_Player(1);
        }
    }
}
