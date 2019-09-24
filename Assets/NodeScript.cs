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
        demo.gameObject.SetActive(false);
        demo.onClick.RemoveListener(demolishTower);
        upgradeBtn.gameObject.SetActive(false);
        upgradeBtn.onClick.RemoveListener(upgradeTower);
    }
    void upgradeTower()
    {

        print("Trying to upgrade");
        TowerScript[] arrowList = nodeTower.GetComponents<TowerScript>();
        AuraTowerScript[] auraList = nodeTower.GetComponents<AuraTowerScript>();
        if(arrowList.GetLength(0) > 0)
        {
            if(arrowList[0].upgrade() && player.gold >= 10*upgradeCost())
            {
                player.addToGold(-10);
            }
        }
        if(auraList.GetLength(0) > 0 && player.gold >= 10*upgradeCost())
        {
             if(auraList[0].upgrade())
            {
                player.addToGold(-10);
            }
        }
            
    }
    
    int upgradeCost()
    {
        GameObject[] a = GameObject.FindGameObjectsWithTag("Nodes");
        int amount = 0;
        foreach(GameObject x in a)
        {
            if(x.GetComponent<NodeScript>().isSelected)
            {
                try {
                    if(!x.GetComponent<NodeScript>().nodeTower.GetComponent<TowerScript>().isUpgraded)
                    {
                        amount++;
                    }
                }
                catch{
                    print("Error ocurred in upgrade cost calc");
                }
                try {
                    if(!x.GetComponent<NodeScript>().nodeTower.GetComponent<AuraTowerScript>().isUpgraded)
                    {
                        amount++;
                    }
                }
                catch{
                    print("Error ocurred in upgrade cost calc");
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
        demo.gameObject.SetActive(false);
        upgradeBtn.gameObject.SetActive(false);
        auraBtn.onClick.AddListener(toggleAuraMode);
        
    }

    // Update is called once per frame
    void Update()
    {

    }
}
