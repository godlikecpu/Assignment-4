using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIScript : MonoBehaviour
{

    public bool isDead = false;
    public float hp;
    float orighp;
    Renderer rend;
    Color origcol;
    public float goldReward = 10f;
    PlayerScript player;

    // Start is called before the first frame update
    void Start()
    {
        rend = GetComponentInChildren<Renderer>();
        origcol = rend.material.color;

        player = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<PlayerScript>();
        hp = player.enemyHp;
        orighp = hp;
    }

    // Update is called once per frame
    void Update()
    {

    }


    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "endNode")
        {
            player.takeDamage();
        }
    }

    public void changeColorOnHit()
    {
        float redValue = hp / orighp;
        rend.material.color = new Color(redValue, 0, 0);
    }
}
