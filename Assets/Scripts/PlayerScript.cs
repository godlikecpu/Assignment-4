using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class PlayerScript : MonoBehaviour
{
    ScoreManager sm;
    public GameObject tower;
    public float gold = 50f;
    TextMeshProUGUI gtxt;
    public float startHealth = 100;
    private float health;
    public Image healthBar;


    // Start is called before the first frame update
    void Start()
    {
        sm = GetComponent<ScoreManager>();
        gtxt = GameObject.FindGameObjectWithTag("goldtext").GetComponent<TextMeshProUGUI>();
        health = startHealth;
        gtxt.text = "Gold: " + gold;
    }

    // Update is called once per frame
    void Update()
    {
    }

    public float addToGold(float goldReward)
    {

        gold += goldReward;
        gtxt.text = "Gold: " + gold;
        return gold;
    }

    public void TakeDamage(float amount)
    {
        health -= amount;

        healthBar.fillAmount = health / startHealth;

        if(health <= 0)
        {
            sm.setScore(GameObject.FindGameObjectWithTag("EnemySpawner").GetComponent<EnemySpawner>().level);
            SceneManager.LoadScene("GameOver");
        }
    }



    //public float barDisplay; //current progress
    //Vector2 pos = new Vector2(900, 40);
    //Vector2 size = new Vector2(200, 20);
    //public Texture2D emptyTex;
    //public Texture2D fullTex;

    //void OnGUI()
    //{
    //    //draw the background:
    //    GUI.BeginGroup(new Rect(pos.x, pos.y, size.x, size.y));
    //    GUI.Box(new Rect(0, 0, size.x, size.y), emptyTex);

    //    //draw the filled-in part:
    //    GUI.BeginGroup(new Rect(0, 0, size.x * barDisplay, size.y));
    //    GUI.Box(new Rect(0, 0, size.x, size.y), fullTex);
    //    GUI.EndGroup();
    //    GUI.EndGroup();
    //}
}
