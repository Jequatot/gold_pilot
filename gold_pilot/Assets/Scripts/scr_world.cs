using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scr_world : MonoBehaviour
{
    scr_player player;
    Transform hullbar;
    Transform shieldbar;

    int sp = 10;
    int hp = 10;

    int shield_timer = 0;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player").GetComponent<scr_player>();
        hullbar = GameObject.Find("Hull_Bar").GetComponent<Transform>();
        shieldbar = GameObject.Find("Shield_Bar").GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        if(shield_timer < 120)
            shield_timer++;

        if (sp < 100 && shield_timer > 60) sp++;
        shieldbar.localScale = new Vector3(4, (float)(sp * 30f / 10f), 1);
        hullbar.localScale = new Vector3(4, (float)(hp * 30f / 10f), 1);
    }

    public void Damage_Player(int dmg) {
        if (shield_timer > 10) {
            shield_timer = 0;

            if (sp > 0) {
                sp = (sp - dmg < 0 ? 0 : sp - dmg);
            } else {
                hp -= dmg;
            }
        }
    }
}
