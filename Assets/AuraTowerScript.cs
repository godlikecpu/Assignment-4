using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AuraTowerScript : MonoBehaviour
{
    public bool isUpgraded = false;
    public float range = 3f;
    public float damageBoost = 10f;
    

    // Start is called before the first frame update
    void Start()
    {
        GetComponent<SphereCollider>().radius = range;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Tower")
        {
            other.gameObject.GetComponent<TowerScript>().addToAuraList(this.gameObject);
        }
    }
     public bool upgrade()
    {
        if(!isUpgraded)
        {
            print("upgrading this sumbitch!");
            damageBoost += 5;
            isUpgraded = true;
            return true;
        }
        else
        {
            print("already upgraded");
            return false;
        }
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
