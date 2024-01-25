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
    [Header("SFX")]
    [SerializeField] private AudioClip selectClip;
    private AudioSource audioSource;
    private int colorIndex;
    private int bullbarIndex;
    private int wheelsIndex;
    private int spoilerIndex;

    private void Awake()
    {
        colorIndex = PlayerPrefs.GetInt("Color", 0);
        bullbarIndex = PlayerPrefs.GetInt("Bullbar", 0);
        wheelsIndex = PlayerPrefs.GetInt("Wheels", 0);
        spoilerIndex = PlayerPrefs.GetInt("Spoiler", 0);
        RestoreMods();

        audioSource = GetComponent<AudioSource>();
    }

    public void SelectMod(ModsEnum mod, int index)
    {
        ChangeMod(mod, index, preview: false, playAudio: true);
    }

    public void BuyMod(ModsEnum mod, int index)
    {
        ChangeMod(mod, index, preview: false, playAudio: false);
    }

    public void ChangeMod(ModsEnum mod, int index, bool preview, bool playAudio)
    {
        switch (mod)
        {
            case ModsEnum.COLOR:
                ChangeColor(index, preview);
                break;
            case ModsEnum.BULLBAR:
                ChangeBullbar(index, preview);
                break;
            case ModsEnum.WHEELS:
                ChangeWheels(index, preview);
                break;
            case ModsEnum.SPOILER:
                ChangeSpoiler(index, preview);
                break;
        }
        if (!preview) onModSelected.Invoke(mod, index);
        if (playAudio) audioSource.PlayOneShot(selectClip);
    }

    public void PreviewMod(ModsEnum mod, int index)
    {
        ChangeMod(mod, index, preview: true, playAudio: false);
    }

    public void RestoreMods()
    {
        ChangeMod(ModsEnum.COLOR, colorIndex, preview: false, playAudio: false);
        ChangeMod(ModsEnum.BULLBAR, bullbarIndex, preview: false, playAudio: false);
        ChangeMod(ModsEnum.WHEELS, wheelsIndex, preview: false, playAudio: false);
        ChangeMod(ModsEnum.SPOILER, spoilerIndex, preview: false, playAudio: false);
    }

    private void ChangeColor(int index, bool preview)
    {
        Material[] newMaterials = meshRenderer.materials;
        newMaterials[0] = materials[index];
        meshRenderer.materials = newMaterials;

        if (preview) return;
        colorIndex = index;
        PlayerPrefs.SetInt("Color", colorIndex);
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

    private void ChangeBullbar(int index, bool preview)
    {
        ChooseObject(bullbars, index);

        if (preview) return;
        bullbarIndex = index;
        PlayerPrefs.SetInt("Bullbar", bullbarIndex);
    }

    private void ChangeWheels(int index, bool preview)
    {
        ChooseObject(wheels, index);

        if (preview) return;
        wheelsIndex = index;
        PlayerPrefs.SetInt("Wheels", wheelsIndex);
    }

    private void ChangeSpoiler(int index, bool preview)
    {
        ChooseObject(spoilers, index);

        if (preview) return;
        spoilerIndex = index;
        PlayerPrefs.SetInt("Spoiler", spoilerIndex);
    }
}
