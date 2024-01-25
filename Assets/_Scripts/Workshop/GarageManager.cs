using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GarageManager : MonoBehaviour
{
    private void Start()
    {
        ChangeScreenResolution(PlayerPrefs.GetInt("ScreenResolution", 1));
    }

    public void BackToGame()
    {
        SceneManager.LoadScene("Waterfront");
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void ChangeScreenResolution(int index)
    {
        switch (index)
        {
            case 0:
                Screen.SetResolution(1920, 1080, false);
                break;
            case 1:
                Screen.SetResolution(1280, 720, false);
                break;
            default:
                break;
        }
        PlayerPrefs.SetInt("ScreenResolution", index);
    }

#if UNITY_EDITOR
    [EasyButtons.Button]
    public void ResetPlayerPrefs()
    {
        PlayerPrefs.DeleteAll();
    }
#endif
}
