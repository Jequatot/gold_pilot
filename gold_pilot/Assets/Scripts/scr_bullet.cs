using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scr_bullet : MonoBehaviour
{
    public Sprite[] sprites;
    SpriteRenderer r;

    Vector3 speed = new Vector3(0, .1f, 0);
    public int timer = 0;
    public int sprite_counter = 0;

    Vector2 screensize = new Vector2(9, 6);

    // Start is called before the first frame update
    void Start()
    {
        r = this.GetComponent<SpriteRenderer>();
        sprites = Resources.LoadAll<Sprite>("Sprites/spr_bullet");
    }

    // Update is called once per frame
    void Update()
    {
        CheckPos();

        timer++;

        sprite_counter = (timer/10)%2;

        r.sprite = sprites[sprite_counter] as Sprite;

        this.transform.Translate(speed);
    }

    void CheckPos() {
        if (this.transform.position.x > screensize.x || this.transform.position.x < -screensize.x || this.transform.position.y > screensize.y || this.transform.position.y < -screensize.y) {
            Destroy(gameObject);
        }

    }
}
