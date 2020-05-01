using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scr_player : MonoBehaviour
{
    // direction the player is moving this frame
    Vector3 speed = new Vector3(0, 0, 0);

    // deltaspeed - how much the speed is changed by with input
    float dspeed = .15f;

    public Object bullet;
    int timer_fire = 0;

    int max_shots_onscreen = 3;

    // Start is called before the first frame update
    void Start() {
        LoadResources();
    }

    // Update is called once per frame
    void Update() {
        Movement();
        Fire();
    }

    void LoadResources() {
        bullet = Resources.Load<Object>("Objects/obj_bullet");
    }

    // Change speed based on input
    void Movement() {
        speed.Set(Input.GetAxisRaw("Horizontal") * dspeed, Input.GetAxisRaw("Vertical") * dspeed, speed.z);
        this.transform.Translate(speed);
    }

    void Fire() {
        if (Input.GetButton("Fire1")) {
            if (timer_fire++ % 30 == 0) {
                Instantiate(bullet, new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z + 1), Quaternion.identity);
            }
        }

        if (Input.GetButtonUp("Fire1")) {
            timer_fire = 0;
        }
    }

    public void Take_Damage(int dmg) {
    }
}
