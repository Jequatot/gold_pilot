using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scr_bullet : MonoBehaviour
{
    public Vector3 speed = new Vector3(0, .3f, 0);

    scr_world world;
    Vector2 screensize;

    // Start is called before the first frame update
    void Start()
    {
        world = GameObject.Find("Tomato").GetComponent<scr_world>();
        screensize = new Vector2(world.screensize.x + 3, world.screensize.y + 3);
    }

    // Update is called once per frame
    void Update()
    {
        CheckPos();

        this.transform.Translate(speed);
    }

    void CheckPos() {
        if (this.transform.position.x > screensize.x || this.transform.position.x < -screensize.x || this.transform.position.y > screensize.y || this.transform.position.y < -screensize.y) {
            Destroy(gameObject);
        }
    }
}
