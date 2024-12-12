using UnityEngine;

public class TDLevelManager : MonoBehaviour
{
    public static TDLevelManager main;

    public Transform startPoint;
    public Transform[] path;

    public int power;

    private void Awake()
    {
        main = this;
    }
    private void Start()
    {
        power = 100;
    }
    /*public void IncreaseCurrency(int amount)
    {
        power += amount;
    }*/
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