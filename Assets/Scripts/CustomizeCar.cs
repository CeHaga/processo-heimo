using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomizeCar : MonoBehaviour
{
    public MeshRenderer meshRenderer;
    public Material[] materials;
    public GameObject[] bullbars;
    public GameObject[] wheels;
    public GameObject[] spoilers;

    public void ChangeColor(int index)
    {
        Material[] newMaterials = meshRenderer.materials;
        newMaterials[0] = materials[index];
        meshRenderer.materials = newMaterials;
        PlayerPrefs.SetInt("Color", index);
    }

    private void ChooseObject(GameObject[] gameObjects, int index)
    {
        for (int i = 0; i < gameObjects.Length; i++)
        {
            if (!gameObjects[i]) continue;
            gameObjects[i].SetActive(false);
        }
        if (gameObjects[index]) gameObjects[index].SetActive(true);
    }

    public void ChangeBullbar(int index)
    {
        ChooseObject(bullbars, index);
        PlayerPrefs.SetInt("Bullbar", index);
    }

    public void ChangeWheels(int index)
    {
        ChooseObject(wheels, index);
        PlayerPrefs.SetInt("Wheels", index);
    }

    public void ChangeSpoiler(int index)
    {
        ChooseObject(spoilers, index);
        PlayerPrefs.SetInt("Spoiler", index);
    }
}
