using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlsPanel : MonoBehaviour
{
    private void Start()
    {
        StartCoroutine(ClosePanel());
    }

    private IEnumerator ClosePanel()
    {
        yield return new WaitForSeconds(5f);
        gameObject.SetActive(false);
    }
}
