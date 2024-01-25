using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectibleController : MonoBehaviour
{
    [SerializeField] private float rotationSpeed = 1f;
    [SerializeField] private float rotationHeight = 0.005f;
    [SerializeField] private AudioClip collectibleSound;
    private float startTime;
    private AudioSource audioSource;

    private void Awake()
    {
        startTime = Time.time;
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        float t = Time.time - startTime;
        float height = Mathf.Abs(transform.position.y + Mathf.Sin(t) * rotationHeight);
        transform.position = new Vector3(transform.position.x, height, transform.position.z);
        transform.Rotate(0, rotationSpeed, 0);
    }

    public void Collect()
    {
        audioSource.PlayOneShot(collectibleSound);
        GetComponent<Collider>().enabled = false;
        GetComponent<MeshRenderer>().enabled = false;
        Invoke("DestroyCollectible", collectibleSound.length);
    }
}
