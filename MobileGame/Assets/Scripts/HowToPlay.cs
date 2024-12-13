using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HowToPlay : MonoBehaviour
{
    [SerializeField]
    string levelToLoad = "HowToPlay";

    public void howToPlay()
    {
        SceneManager.LoadSceneAsync(1);
    }

    public void howToPlay2()
    {
        SceneManager.LoadSceneAsync(8);
    }    
}