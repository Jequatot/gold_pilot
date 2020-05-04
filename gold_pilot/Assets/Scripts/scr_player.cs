using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scr_player : MonoBehaviour
{
    // direction the player is moving this frame
    Vector3 speed = new Vector3(0, 0, 0);
    Vector2 screensize;

    // deltaspeed - how much the speed is changed by with input
    float dspeed = .15f;

    public Object bullet;
    int timer_fire = 0;

    int max_shots_onscreen = 3;

    // Start is called before the first frame update
    void Start() {
        LoadResources();
        screensize = GameObject.Find("Tomato").GetComponent<scr_world>().screensize;
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
        float xspeed = Input.GetAxisRaw("Horizontal") * dspeed;
        float yspeed = Input.GetAxisRaw("Vertical") * dspeed;
        if (this.transform.position.x + xspeed > screensize.x || this.transform.position.x + xspeed < -screensize.x)
            xspeed = 0f;
        if (this.transform.position.y + yspeed > screensize.y || this.transform.position.y + yspeed < -screensize.y)
            yspeed = 0f;
        speed.Set(xspeed, yspeed, speed.z);
        this.transform.Translate(speed);
    }

    void Fire() {
        if (Input.GetButton("Fire1")) {
            if (timer_fire++ % 10 == 0) {
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
