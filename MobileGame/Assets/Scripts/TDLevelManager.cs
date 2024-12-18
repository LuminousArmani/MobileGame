using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TDLevelManager : MonoBehaviour
{
    public static TDLevelManager main;

    [SerializeField] TextMeshProUGUI healthUI;

    public Transform startPoint;//where the enemy will spawn
    public Transform[] path;// an array of empty game objects 


    public int health = 10;
    public int currency;


    public int Health
    {
        get => health;
        set
        {
            health = value; // Update the internal health value
            healthUI.text = health.ToString(); // Update the health UI text

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
        currency = 100;
        healthUI.text = health.ToString();
    }
    public void IncreaseCurrency(int amount)
    {
        currency += amount;
    }
    public bool SpendCurrency(int amount)
    {
        if (amount <= currency)
        {
            // buy item
            currency -= amount;
            return true;
        }
        else
        {
            Debug.Log("no more troops to send on the battle field");
            return false;
        }
        
    }
}
