using UnityEngine;
using TMPro;
using UnityEditor;

public class ShopMenu : MonoBehaviour
{
    [Header("References")]
    [SerializeField] TextMeshProUGUI currencyUI;
    [SerializeField] Animator anim;

    private bool isMenuOpen = true;

    public void ToggleMenu()
    {
        isMenuOpen = !isMenuOpen;
        anim.SetBool("MenuOpen", isMenuOpen);
    }

    private void OnGUI()
    {
        currencyUI.text = TDLevelManager.main.currency.ToString();
    }
}
