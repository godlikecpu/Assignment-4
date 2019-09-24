using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AIScript : MonoBehaviour
{

    public bool isDead = false;
    public float hp;
    public float orighp;
    Renderer rend;
    Color origcol;
    public float goldReward = 10f;
    PlayerScript player;
    public bool isBoss = false;

    // Start is called before the first frame update
    void Start()
    {
        rend = GetComponentInChildren<Renderer>();
        origcol = rend.material.color;

        player = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<PlayerScript>();
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

    public void setHp(int HP)
    {
        hp = HP;
        orighp = HP;
    }

    public void changeColorOnHit()
    {
        float redValue = hp / orighp;
        rend.material.color = new Color(redValue, 0, 0);
    }
}
