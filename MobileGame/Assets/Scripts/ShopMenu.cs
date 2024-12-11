using UnityEngine;
using TMPro;

public class ShopMenu : MonoBehaviour
{
    [Header("References")]
    [SerializeField] TextMeshProUGUI currencyUI;

    private void OnGUI()
    {
        currencyUI.text = LevelManager.main.power.ToString();
    }

    public void SetSelected()
    {

    }






}
