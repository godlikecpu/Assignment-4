using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;

public class AIScript : MonoBehaviour
{
    public AudioSource asource;
    public bool isDead = false;
    public float hp;
    public float orighp;
    Renderer rend;
    Color origcol;
    public float goldReward = 10f;
    PlayerScript player;
    public bool isBoss = false;
    public Image healthBar;

    // Start is called before the first frame update
    void Start()
    {
        asource = GetComponent<AudioSource>();
        if (!isBoss) { 
        GetComponentInChildren<Image>().gameObject.SetActive(false);
        }
        else
        {
            goldReward = 100f;
        }

        player = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<PlayerScript>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public float getGoldReward()
    {
        float goldR = goldReward;
        goldReward = 0f;
        return goldR;
    }

    public void setSpeed(float speed)
    {
        GetComponent<NavMeshAgent>().speed = speed;
    }


    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "endNode")
        {
            player.TakeDamage(0.1f);
        }
    }

    public void setHp(int HP)
    {
        hp = HP;
        orighp = HP;
    }

}
