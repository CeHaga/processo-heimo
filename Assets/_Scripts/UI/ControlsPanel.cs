using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlsPanel : MonoBehaviour
{
    [SerializeField] private float activeSeconds;

    private void Start()
    {
        Invoke("ClosePanel", activeSeconds);
    }

    private void ClosePanel()
    {
        gameObject.SetActive(false);
    }
}
