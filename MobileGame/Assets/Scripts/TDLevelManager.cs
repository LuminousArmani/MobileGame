using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TDLevelManager : MonoBehaviour
{
    public static TDLevelManager main;

    [SerializeField] TextMeshProUGUI healthUI;

    public Transform startPoint;
    public Transform[] path;


    public int health = 100;
    public int power;


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
        power = 100;
        healthUI.text = health.ToString();
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
