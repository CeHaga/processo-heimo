using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GarageManager : MonoBehaviour
{
    public void BackToSandBox()
    {
        SceneManager.LoadScene("Sandbox");
    }
}
