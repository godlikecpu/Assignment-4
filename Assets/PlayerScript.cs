using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class PlayerScript : MonoBehaviour
{

    public GameObject tower;
    public float gold = 50f;
    Text txt;


    // Start is called before the first frame update
    void Start()
    {
        txt = GameObject.FindGameObjectWithTag("goldtext").GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
    }

    public float addToGold(float goldReward)
    {

        gold += goldReward;
        txt.text = "Gold: " + gold;
        return gold;
    }

    public void takeDamage()
    {
        barDisplay += 0.001f;

        if(barDisplay > 1)
        {
            SceneManager.LoadScene("GameOver");
        }
    }



    public float barDisplay; //current progress
    Vector2 pos = new Vector2(900, 40);
    Vector2 size = new Vector2(200, 20);
    public Texture2D emptyTex;
    public Texture2D fullTex;

    void OnGUI()
    {
        //draw the background:
        GUI.BeginGroup(new Rect(pos.x, pos.y, size.x, size.y));
        GUI.Box(new Rect(0, 0, size.x, size.y), emptyTex);

        //draw the filled-in part:
        GUI.BeginGroup(new Rect(0, 0, size.x * barDisplay, size.y));
        GUI.Box(new Rect(0, 0, size.x, size.y), fullTex);
        GUI.EndGroup();
        GUI.EndGroup();
    }
}
