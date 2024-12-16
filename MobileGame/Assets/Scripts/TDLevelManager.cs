using UnityEngine;
using UnityEngine.SceneManagement;

public class TDLevelManager : MonoBehaviour
{
    public static TDLevelManager main;

    public Transform startPoint;
    public Transform[] path;


    public int health = 100;
    public int power;


    public int Health
    {
        get => health;
        set
        {
            if (health <= 0)
            {
                SceneManager.LoadScene("lose");
            }
        }
    }
    private void Awake()
    {
        main = this;
    }
    private void Start()
    {
        power = 100;
    }
    public void IncreaseCurrency(int amount)
    {
        power += amount;
    }
    public bool SpendCurrency(int amount)
    {
        if (amount <= power)
        {
            // buy item
            power -= amount;
            return true;
        }
        else
        {
            Debug.Log("no more troops to send on the battle field");
            return false;
        }
        
    }
}
// i want my health int to be able to be callled and changed by other scripts