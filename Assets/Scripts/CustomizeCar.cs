using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomizeCar : MonoBehaviour
{
    public MeshRenderer meshRenderer;
    public Material[] materials;
    public GameObject[] bullbars;

    public void ChangeColor(int index)
    {
        Material[] newMaterials = meshRenderer.materials;
        newMaterials[0] = materials[index];
        meshRenderer.materials = newMaterials;
    }

    public void ChangeBullbar(int index)
    {
        for (int i = 0; i < bullbars.Length; i++)
        {
            if (!bullbars[i]) continue;
            bullbars[i].SetActive(false);
        }
        if (bullbars[index]) bullbars[index].SetActive(true);
    }
}
