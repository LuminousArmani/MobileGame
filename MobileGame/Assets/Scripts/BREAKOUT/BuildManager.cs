using TMPro;
using UnityEngine;

public class BuildManager : MonoBehaviour
{
    public static BuildManager main;

    [Header("References")]
    //[SerializeField] private GameObject[] towerPrefabs;
    [SerializeField] private Tower[] towers;
    [SerializeField] private TextMeshProUGUI selectedTowerText;

    private int selectedTower = 0;

    private void Awake()
    {
        main = this;
    }
    public Tower GetSelectedTower()
    {
        return towers[selectedTower];
    }
    public void SetSelectedTower(int _selectedTower)
    {
        selectedTower = _selectedTower;
        if (selectedTowerText != null)
        {
            selectedTowerText.text = $"Selected Tower: {towers[selectedTower].name}";
        }
    }
}
