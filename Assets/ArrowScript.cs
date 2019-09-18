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
        if (collision.gameObject.tag == "Enemy" && dangerous)
        {

            collision.gameObject.GetComponent<AIScript>().hp -= damage;
            if (collision.gameObject.GetComponent<AIScript>().hp <= 0)
            {
                
                float goldReward = collision.gameObject.GetComponent<AIScript>().goldReward;
                GameObject.FindGameObjectWithTag("MainCamera").GetComponent<PlayerScript>().addToGold(goldReward);
                Instantiate(expl, collision.gameObject.transform.position, Quaternion.identity);
                Destroy(collision.gameObject);
            }
            else
            {
                collision.gameObject.GetComponent<AIScript>().changeColorOnHit();
            }
        }

        if (collision.gameObject.tag == "Nodes")
        {
            dangerous = false;
        }
    }
}
