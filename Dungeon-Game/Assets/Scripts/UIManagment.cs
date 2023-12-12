using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManagment : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartGame()
    {

        StartCoroutine(FadeIn("Game"));
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    IEnumerator FadeIn(string name)
    {
        //Corrutina
        SceneManager.LoadScene(name);
        return null;
    }
}