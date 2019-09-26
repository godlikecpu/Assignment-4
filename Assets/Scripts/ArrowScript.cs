using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ArrowScript : MonoBehaviour
{

    bool dangerous = true;
    public ParticleSystem expl;
    Text txt;
    public float damage;


    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(cleanArrow());

    }

    IEnumerator cleanArrow()
    {
        yield return new WaitForSeconds(3f);
        Destroy(this.gameObject);
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnCollisionEnter(Collision collision)
    {

        AIScript enemy = collision.gameObject.GetComponent<AIScript>();
        if ((collision.gameObject.tag == "Enemy" || collision.gameObject.tag == "Boss") && dangerous)
        {
            if (enemy.isBoss)
            {
                enemy.healthBar.fillAmount = enemy.hp / enemy.orighp;
            }
            enemy.hp -= damage;
            if (enemy.hp <= 0)
            {
                enemy.asource.enabled = true;
                enemy.asource.Play();
                Destroy(collision.gameObject);
                float goldReward = enemy.getGoldReward();
                GameObject.FindGameObjectWithTag("MainCamera").GetComponent<PlayerScript>().addToGold(goldReward);
                Instantiate(expl, collision.gameObject.transform.position, Quaternion.identity);
                
            }
        }

        if (collision.gameObject.tag == "Nodes")
        {
            dangerous = false;
        }
    }
}
