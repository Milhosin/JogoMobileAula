using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadSceneHelper : MonoBehaviour
{
    // Carrega a cena usando o índice (ID no Build Settings)
    public void Load(int i)
    {
        SceneManager.LoadScene(i);
    }

    // Carrega a cena usando o nome (String)
    public void Load(string s)
    {
        SceneManager.LoadScene(s);
    }
}
