using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scr_enemy : MonoBehaviour
{
    string[] kinds = { "static", "raindrop", "loopdeloop", "shield", "gunner", "sidegunner", "switch" };
    public int kind = 0;
    scr_world world;
    Vector2 screensize;

    SpriteRenderer r;

    GameObject bullet;
    GameObject explosion;
    int state = 0;

    int hp = 1;
    List<Vector2> path;

    int shieldtimer = 0;

    int rof = 0;
    int firetimer = 0;

    bool invuln = false;

    float sidelimit;

    // deltaspeed - how much the speed is changed by with input
    float dspeed = -.02f;
    // direction the player is moving this frame
    public Vector3 speed;

    // Start is called before the first frame update
    void Start()
    {
        bullet = Resources.Load<GameObject>("Objects/obj_ball");
        explosion = Resources.Load<GameObject>("Objects/obj_explosion");
        world = GameObject.Find("Tomato").GetComponent<scr_world>();
        screensize = new Vector2(world.screensize.x + 5, world.screensize.y + 5);
        r = this.GetComponent<SpriteRenderer>();
        path = new List<Vector2>();
        Typify();
    }

    // Update is called once per frame
    void Update()
    {
        CheckPos();
        shieldtimer++;
        if(shieldtimer == 5)
            r.color = Color.white;

        Movement();
        Fire();
    }

    void Typify() {
        switch (kind) {
            case 0:
                invuln = true;
                break;
            case 1:
                speed = new Vector3(0, dspeed*2, 0);
                break;
            case 2:
                dspeed = .1f;
                path.Add(new Vector2(this.transform.position.x, 0f));
                path.Add(new Vector2(this.transform.position.x * 2 / 3, -3f));
                path.Add(new Vector2(0f, -4f));
                path.Add(new Vector2(-this.transform.position.x * 2 / 3, -3f));
                path.Add(new Vector2(-this.transform.position.x, 0f));
                path.Add(new Vector2(-this.transform.position.x * 2 / 3, 3f));
                path.Add(new Vector2(0f, 4f));
                path.Add(new Vector2(this.transform.position.x * 2 / 3, 3f));
                path.Add(new Vector2(this.transform.position.x, 0f));
                path.Add(new Vector2(this.transform.position.x, -6f));
                break;
            case 3:
                speed = new Vector3(0, dspeed, 0);
                hp = 5;
                break;
            case 4:
                speed = new Vector3(0, dspeed, 0);
                rof = 60;
                break;
            case 5:
                speed = new Vector3(dspeed * 4 * Mathf.Sign(this.transform.position.x), dspeed, 0);
                rof = 60;
                break;
            case 6:
                speed = new Vector3(0, dspeed, 0);
                invuln = true;
                break;
        }
    }

    void Movement() {
        if(path.Count > 0) { 
                float xdiff, ydiff;

                if (this.transform.position.x > path[0].x + dspeed) xdiff = path[0].x - this.transform.position.x;
                else if (this.transform.position.x < path[0].x - dspeed) xdiff = path[0].x - this.transform.position.x;
                else xdiff = 0;

                if (this.transform.position.y > path[0].y + dspeed) ydiff = path[0].y - this.transform.position.y;
                else if (this.transform.position.y < path[0].y - dspeed) ydiff = path[0].y - this.transform.position.y;
                else ydiff = 0;

                if (xdiff == 0 && ydiff == 0)
                    path.RemoveAt(0);
                else speed.Set(dspeed * xdiff / (Mathf.Abs(xdiff) + Mathf.Abs(ydiff)), dspeed * ydiff / (Mathf.Abs(xdiff) + Mathf.Abs(ydiff)), 0);
        }
        this.transform.Translate(speed);
    }
    
    void Fire() {
        if (rof > 0 && firetimer++ > rof) {
            firetimer = 0;
            var b = Instantiate(bullet, new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z + 1), Quaternion.identity);
            float xdiff, ydiff;
            xdiff = world.getPlayerPos().x - this.transform.position.x;
            ydiff = world.getPlayerPos().y - this.transform.position.y;
            b.GetComponent<scr_enemy>().speed.Set(.1f * xdiff / (Mathf.Abs(xdiff) + Mathf.Abs(ydiff)), .1f * ydiff / (Mathf.Abs(xdiff) + Mathf.Abs(ydiff)), 0);
        }
    }

    void OnTriggerStay2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Projectile") {
            if (!invuln) {
                Destroy(collider.gameObject);
                r.color = Color.red;
                shieldtimer = 0;

                if (--hp == 0) {
                    Instantiate(explosion, new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z + 1), Quaternion.identity);
                    Destroy(gameObject);
                }
            }
            else if(kind == 6) {
                Debug.Log("The gates are open");
            }
        }
        if (collider.gameObject.tag == "Player") {
            world.Damage_Player(1);
            if (kind == 0)
                Destroy(gameObject);
        }
    }

    void CheckPos()
    {
        if (this.transform.position.x > screensize.x || this.transform.position.x < -screensize.x || this.transform.position.y > screensize.y || this.transform.position.y < -screensize.y) {
            Destroy(gameObject);
        }
    }
}
