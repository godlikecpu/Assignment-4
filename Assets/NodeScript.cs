using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NodeScript : MonoBehaviour
{
    public Button upgradeBtn;
    public Button auraBtn;
    public Button btn;
    public Button demo;
    public Color hoverColor;
    private Renderer rd;
    private Color startColor;
    GameObject tower;
    public GameObject auraTower;
    Vector3 offset = new Vector3(0, 0.4f, 0);
    public bool hasTower = false;
    public bool unbuildable = false;
    public bool buildMode = false;
    PlayerScript player;
    private GameObject nodeTower;
    private bool auraMode = false;
    public bool isSelected = false;
    EnemySpawner es;

    private void OnMouseEnter()
    {
        if (!unbuildable)
        {
            rd.material.color = hoverColor;
        }
    }

    private void OnMouseExit()
    {
        if (!unbuildable && !isSelected)
        {
            rd.material.color = startColor;
        }
    }


    private void OnMouseDown()
    {
        if (buildMode && !hasTower && !unbuildable && player.gold >= 10)
        {
            nodeTower = Instantiate(tower, transform.position + offset, transform.rotation);
            nodeTower.GetComponent<TowerScript>().node = this.gameObject;
            hasTower = true;
            player.addToGold(-10);

        }

        if (auraMode && !hasTower && !unbuildable && player.gold >= 10)
        {
            nodeTower = Instantiate(auraTower, transform.position + offset, transform.rotation);
            hasTower = true;
            player.addToGold(-10);

        }

        if (hasTower && !(buildMode || auraMode) && !isSelected && !unbuildable)
        {
            rd.material.color = hoverColor;
            demo.gameObject.SetActive(true);
            demo.onClick.AddListener(demolishTower);
            upgradeBtn.gameObject.SetActive(true);
            upgradeBtn.onClick.AddListener(upgradeTower);
            isSelected = true;
        }
        else if (isSelected)
        {
            rd.material.color = startColor;
            isSelected = false;
            demo.onClick.RemoveListener(demolishTower);
            upgradeBtn.onClick.RemoveListener(upgradeTower);
        }
    }

    void toggleBuildMode()
    {
        if (buildMode)
        {
            buildMode = false;
        }
        else
        {
            buildMode = true;
            auraMode = false;
        }
    }
    void toggleAuraMode()
    {
        if (auraMode)
        {
            auraMode = false;
        }
        else
        {
            auraMode = true;
            buildMode = false;
        }
    }

    void demolishTower()
    {
        Destroy(nodeTower);
        hasTower = false;
        player.addToGold(5);
        rd.material.color = startColor;
        demo.onClick.RemoveListener(demolishTower);
        upgradeBtn.onClick.RemoveListener(upgradeTower);
    }
    void upgradeTower()
    {

        print("Trying to upgrade");
        TowerScript[] arrowList = nodeTower.GetComponents<TowerScript>();
        AuraTowerScript[] auraList = nodeTower.GetComponents<AuraTowerScript>();
        if(arrowList.GetLength(0) > 0)
        {
            if( player.gold >= 10*upgradeCost())
            {
                player.addToGold(-10*arrowList[0].upgradeLvl);
                arrowList[0].upgrade();
            }
        }
        if(auraList.GetLength(0) > 0)
        {
             if(player.gold >= 10*upgradeCost())
            {
                player.addToGold(-10*auraList[0].upgradeLvl);
                auraList[0].upgrade();
            }
        }
            
    }
    
    int upgradeCost()
    {
        GameObject[] nodeList = GameObject.FindGameObjectsWithTag("Nodes");
        int amount = 0;
        foreach(GameObject node in nodeList)
        {
            if(node.GetComponent<NodeScript>().isSelected)
            {
                try {
                    amount += node.GetComponent<NodeScript>().nodeTower.GetComponent<TowerScript>().upgradeLvl;
                        
                }
                catch{
                    //Non error
                }
                try {
                    amount += node.GetComponent<NodeScript>().nodeTower.GetComponent<AuraTowerScript>().upgradeLvl;
                }
                catch{
                    //Non error
                }
            }
        }
        print(amount);
        return amount;
    }

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<PlayerScript>();
        tower = player.tower;
        rd = GetComponent<Renderer>();
        startColor = rd.material.color;
        btn.onClick.AddListener(toggleBuildMode);
        auraBtn.onClick.AddListener(toggleAuraMode);
        
    }

    // Update is called once per frame
    void Update()
    {

    }
}
