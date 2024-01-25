using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GarageManager : MonoBehaviour
{
    public void BackToGame()
    {
        SceneManager.LoadScene("Waterfront");
    }

    public void QuitGame()
    {
        Application.Quit();
    }

#if UNITY_EDITOR
    [EasyButtons.Button]
    public void ResetPlayerPrefs()
    {
        PlayerPrefs.DeleteAll();
    }
#endif
}
