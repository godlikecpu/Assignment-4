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
    bool hasTower = false;
    public bool unbuildable = false;
    public bool buildMode = false;
    PlayerScript player;
    private GameObject nodeTower;
    private bool auraMode = false;
    private bool isSelected = false;
    EnemySpawner es;

    private void OnMouseEnter()
    {
        if (!unbuildable) { 
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
            if(!es.checkPath())
            {
                Destroy(nodeTower);
            }else {
                hasTower = true;
            player.addToGold(-10);
            }
        }
        if (auraMode && !hasTower && !unbuildable && player.gold >= 10)
        {
            nodeTower = Instantiate(auraTower, transform.position + offset, transform.rotation);
            if(!es.checkPath())
            {
                Destroy(nodeTower);
            }else {
                hasTower = true;
            player.addToGold(-10);
            }
        }
     
        if(hasTower && !(buildMode||auraMode) && !isSelected && !unbuildable)
        {
            rd.material.color = hoverColor;
            demo.gameObject.SetActive(true);
            demo.onClick.AddListener(demolishTower);
            upgradeBtn.gameObject.SetActive(true);
            upgradeBtn.onClick.AddListener(upgradeTower);
            isSelected = true;
        }
        else if(isSelected)
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
    {print("Trying to upgrade");
        try {
            TowerScript[] a = nodeTower.GetComponents<TowerScript>();
            
        }
        catch {
            //NIL
            print("Not a Arrow Tower");
        }
        try {
            AuraTowerScript[] a = nodeTower.GetComponents<AuraTowerScript>();
        } catch {
            //NIL
            print("Not an Aura Tower");
        }
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
        es = GameObject.FindGameObjectWithTag("EnemySpawner").GetComponent<EnemySpawner>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
