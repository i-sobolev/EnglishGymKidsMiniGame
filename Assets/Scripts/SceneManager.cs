using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneManager : MonoBehaviour
{
    public void RestartScene() => UnityEngine.SceneManagement.SceneManager.LoadScene(0);
    public void Exit() => Application.Quit();
}
