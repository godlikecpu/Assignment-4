using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AuraTowerScript : MonoBehaviour
{
    [HideInInspector] public int upgradeLvl = 1;
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
     public void upgrade()
    {
            damageBoost += 5;
            upgradeLvl += 1;
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
