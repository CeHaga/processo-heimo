using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CustomizeCar : MonoBehaviour
{
    [SerializeField] private MeshRenderer meshRenderer;
    [SerializeField] private Material[] materials;
    [SerializeField] private GameObject[] bullbars;
    [SerializeField] private GameObject[] wheels;
    [SerializeField] private GameObject[] spoilers;
    [SerializeField] private UnityEvent<ModsEnum, int> onModSelected;

    private void Awake()
    {
        int color = PlayerPrefs.GetInt("Color", 0);
        int bullbar = PlayerPrefs.GetInt("Bullbar", 0);
        int wheels = PlayerPrefs.GetInt("Wheels", 0);
        int spoiler = PlayerPrefs.GetInt("Spoiler", 0);
        ChangeMod(ModsEnum.COLOR, color);
        ChangeMod(ModsEnum.BULLBAR, bullbar);
        ChangeMod(ModsEnum.WHEELS, wheels);
        ChangeMod(ModsEnum.SPOILER, spoiler);
    }

    public void ChangeMod(ModsEnum mod, int index)
    {
        switch (mod)
        {
            case ModsEnum.COLOR:
                ChangeColor(index);
                break;
            case ModsEnum.BULLBAR:
                ChangeBullbar(index);
                break;
            case ModsEnum.WHEELS:
                ChangeWheels(index);
                break;
            case ModsEnum.SPOILER:
                ChangeSpoiler(index);
                break;
        }
        onModSelected.Invoke(mod, index);
    }

    private void ChangeColor(int index)
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

    private void ChangeBullbar(int index)
    {
        ChooseObject(bullbars, index);
        PlayerPrefs.SetInt("Bullbar", index);
    }

    private void ChangeWheels(int index)
    {
        ChooseObject(wheels, index);
        PlayerPrefs.SetInt("Wheels", index);
    }

    private void ChangeSpoiler(int index)
    {
        ChooseObject(spoilers, index);
        PlayerPrefs.SetInt("Spoiler", index);
    }
}
