using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scr_world : MonoBehaviour
{
    scr_player player;
    Transform playertransform;
    Transform hullbar;
    Transform shieldbar;
    public Vector2 screensize = new Vector2(9, 6);

    GameObject enemy;

    int sp = 10;
    int hp = 10;

    int timer = 0;
    int phase = 0;
    public int shield_timer = 0;

    string[] board = {
            "              1     ",
            "              1     ",
            "              1     ",
            "              1     ",
            "              1     ",
            "5                  5",
            "                    ",
            "                    ",
            "                    ",
            "                    ",
            "                    ",
            "                    ",
            "                    ",
            "                    ",
            "                    ",
            "                    ",
            "                    ",
            "                    ",
            "     2              ",
            "              2     ",
            "      2             ",
            "             2      ",
            "                    ",
            "                    ",
            "          3         ",
            "                    ",
            "          4         ",
            "                    ",
            "          4         ",
            "     2              ",
            "              2     ",
            "      2             ",
            "             2      ",
            "                    ",
            "                    ",
            "                    ",
            "                    ",
            "                    ",
            "                    ",
            "                    ",
            "                    ",
            "                    ",
            "                    ",
            "                   5",
            "                   5",
            "                   5"
    };

    int spawnrate = 30;

    // Start is called before the first frame update
    void Start()
    {

        player = GameObject.Find("Player").GetComponent<scr_player>();
        playertransform = GameObject.Find("Player").GetComponent<Transform>();
        hullbar = GameObject.Find("Hull_Bar").GetComponent<Transform>();
        shieldbar = GameObject.Find("Shield_Bar").GetComponent<Transform>();

        enemy = Resources.Load<GameObject>("Objects/obj_enemy");
    }

    // Update is called once per frame
    void Update()
    {
        if(shield_timer < 25565)
            shield_timer++;

        if (shield_timer > 120 && shield_timer % 60 == 0 && sp < 10) sp++;
        shieldbar.localScale = new Vector3(0.25f, (float)(sp * 5f / 10f), 1);
        hullbar.localScale = new Vector3(0.25f, (float)(hp * 5f / 10f), 1);

        if (timer % spawnrate == 0) {
            int i = -10;
            foreach(char c in board[timer/ spawnrate]) {
                if(c != ' ')
                    makeEnemy(c - '0', i);
                i++;
            }
            phase++;
        }

        timer++;
    }

    public void Damage_Player(int dmg) {
        if (shield_timer > 5) {
            shield_timer = 0;

            if (sp > 0) {
                sp = (sp - dmg < 0 ? 0 : sp - dmg);
            } else {
                hp = (hp - dmg < 0 ? 0 : hp - dmg);
                if (hp <= 0)
                    Destroy(player.gameObject);
            }
        }
    }

    public Vector3 getPlayerPos() {
        return playertransform.position;
    }

    public void makeEnemy(int kind, int x) {
        var e = Instantiate(enemy, new Vector3(x, 6, 0), Quaternion.identity);
        e.GetComponent<scr_enemy>().kind = kind;
    }
}
