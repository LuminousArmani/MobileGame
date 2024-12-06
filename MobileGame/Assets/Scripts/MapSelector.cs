using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MapSelector : MonoBehaviour
{
    [SerializeField]
    string levelToLoad = "EarthEasy";

    // Start is called before the first frame update

    public void Earth()
    {
        SceneManager.LoadSceneAsync(3);
    }
    public void Mars()
    {
        SceneManager.LoadSceneAsync(4);
    }
    public void Neptune()
    {
        SceneManager.LoadSceneAsync(5);
    }
}