using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NodeScript : MonoBehaviour
{
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

    private void OnMouseEnter()
    {
        if ((buildMode && !unbuildable)||(auraMode && !unbuildable)) { 
        rd.material.color = hoverColor;
        }
    }

    private void OnMouseExit()
    {
        if ((buildMode && !unbuildable) ||(auraMode && !unbuildable))
        {
            rd.material.color = startColor;
            demo.onClick.RemoveListener(demolishTower);
            demo.gameObject.SetActive(false);
        }
    }

    private void OnMouseDown()
    {
        if (buildMode && !hasTower && !unbuildable && player.gold >= 10)
        {
            nodeTower = Instantiate(tower, transform.position + offset, transform.rotation);
            hasTower = true;
            player.addToGold(-10);
        }
        if (auraMode && !hasTower && !unbuildable && player.gold >= 10)
        {
            nodeTower = Instantiate(auraTower, transform.position + offset, transform.rotation);
            hasTower = true;
            player.addToGold(-10);
        }
        if(hasTower && !(buildMode||auraMode))
        {
            rd.material.color = hoverColor;
            demo.gameObject.SetActive(true);
            demo.onClick.AddListener(demolishTower);
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
        auraBtn.onClick.AddListener(toggleAuraMode);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
