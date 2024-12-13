using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class PlayerHealth : MonoBehaviour
{
    [SerializeField]
    float health = 10;
    [SerializeField]
    string levelToLoad = "Lose";
    float maxHP;
    [SerializeField]
    Image healthBar;
    // Start is called before the first frame update
    void Start()
    {
        maxHP = health;
        healthBar.fillAmount = health / maxHP;
    }
    // Update is called once per frame
    void Update()
    {

    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log(collision.gameObject.name);
        //IF we hit an enemy, reduce player hp
        if (collision.gameObject.tag == "Enemy" || collision.gameObject.tag == "EnemyBullet")
        {
            health -= 1;
            healthBar.fillAmount = health / maxHP;
            GetComponent<AudioSource>().Play();
            //add consequences
            //IF health gets too low, reload the current level
            if (health <= 0)
            {
                SceneManager.LoadSceneAsync(7);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy" || collision.gameObject.tag == "EnemyBullet")
        {
            health -= 1;
            healthBar.fillAmount = health / maxHP;
            GetComponent<AudioSource>().Play();
            //add consequences
            //IF health gets too low, go to lose scene
            if (health <= 0)
            {
                SceneManager.LoadSceneAsync(7);
            }
        }
    }
}