using UnityEngine;

public class Plot : MonoBehaviour
{


    [Header("References")]
    [SerializeField] private SpriteRenderer sr;
    [SerializeField] private Color hoverColor;

    private GameObject tower;
    private Color startColor;

    private void Start()
    {
        startColor = sr.color;
    }

    private void OnMouseEnter()
    {
        if (tower != null) return;
        sr.color = hoverColor;
    }

    private void OnMouseExit()
    {
        sr.color = startColor;
    }

    private void OnMouseDown()
    {
        if (tower != null) return;

        Tower towerToBuild = BuildManager.main.GetSelectedTower();
        
        if (towerToBuild.cost > TDLevelManager.main.currency)
        {
            Debug.Log("Broke");
            return;
        }

        TDLevelManager.main.SpendCurrency(towerToBuild.cost);

        tower = Instantiate(towerToBuild.prefab, transform.position, Quaternion.identity);

    }
}