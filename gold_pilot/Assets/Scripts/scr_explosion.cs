using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scr_explosion : MonoBehaviour
{
    int timer = 0;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (timer++ == 16)
            Destroy(gameObject);
    }
}
