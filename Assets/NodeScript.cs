using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NodeScript : MonoBehaviour
{

    public Button btn;
    public Button demo;
    public Color hoverColor;
    private Renderer rd;
    private Color startColor;
    GameObject tower;
    Vector3 offset = new Vector3(0, 1, 0);
    bool hasTower = false;
    public bool unbuildable = false;
    public bool buildMode = false;
    PlayerScript player;
    public bool demoMode = false;
    private GameObject nodeTower;

    private void OnMouseEnter()
    {
        if (buildMode && !unbuildable) { 
        rd.material.color = hoverColor;
        }
    }

    private void OnMouseExit()
    {
        if (buildMode && !unbuildable)
        {
            rd.material.color = startColor;
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
        if (demoMode && hasTower)
        {
            Destroy(nodeTower);
            hasTower = false;
            player.addToGold(5);
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
            demoMode = false;
            buildMode = true;
        }
    }

    void toggleDemoMode()
    {
        if(demoMode)
        {
            demoMode = false;
        }
        else
        {
            demoMode = true;
            buildMode = false;
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
        demo.onClick.AddListener(toggleDemoMode);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
