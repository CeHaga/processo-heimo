using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonClick : MonoBehaviour
{
    [SerializeField] private Sprite defaultImage;
    [SerializeField] private Sprite clickImage;
    private Image image;
    private Button button;

    private void Start()
    {
        image = GetComponent<Image>();
        image.sprite = defaultImage;

        button = GetComponent<Button>();
        button.onClick.AddListener(OnClick);
    }

    public void OnClick()
    {
        image.sprite = clickImage;
        Invoke("OnRelease", 0.1f);
    }

    public void OnRelease()
    {
        image.sprite = defaultImage;
    }
}
