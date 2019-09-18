using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerScript : MonoBehaviour
{

    GameObject target;
    public GameObject arrow;
    public float damage = 50f;
    public float range = 3f;
    public float reload = 2f;
    Vector3 offset = new Vector3(0, 1, 0);



    // Start is called before the first frame update
    void Start()
    {
        GetComponent<SphereCollider>().radius = range;
    }

    // Update is called once per frame
    void Update()
    {

    }

    IEnumerator checkIfDead(GameObject target)
    {
        yield return new WaitForSeconds(reload);
        if (!target)
        {
            target = null;
        }
        else
        {
            shoot(target);
        }

    }

    void shoot(GameObject target)
    {
       GameObject arrowFired = Instantiate(arrow, transform.position + offset, transform.rotation);
       arrowFired.transform.LookAt(target.transform.position);
       arrowFired.GetComponent<ArrowScript>().damage = damage;
       arrowFired.GetComponent<Rigidbody>().AddForce((target.transform.position - transform.position) * 100);
       StartCoroutine(checkIfDead(target));
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!target && other.gameObject.tag == "Enemy")
        {
            target = other.gameObject;
            shoot(target);
        }
    }

}
