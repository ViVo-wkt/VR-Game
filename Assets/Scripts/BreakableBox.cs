using UnityEngine;

public class BreakableBox : MonoBehaviour
{
    public GameObject brokenBoxPrefab;
    public GameObject itemInsidePrefab;
    public float breakForceThreshold = 4f;

    private AudioSource audioSource;
    private bool broken = false;

    private void OnCollisionEnter(Collision collision)
    {
        if (broken) return;

        if (collision.relativeVelocity.magnitude > breakForceThreshold)
        {
            BreakBox();
        }
    }

    void BreakBox()
    {
        broken = true;

        Instantiate(brokenBoxPrefab, transform.position, transform.rotation);
        

        if (itemInsidePrefab != null)
        {
            Instantiate(itemInsidePrefab, transform.position + Vector3.up * 0.2f, Quaternion.identity);
        }

        if (audioSource != null && audioSource.clip != null)
        {
            GameObject tempAudio = new GameObject("TempAudio");
            AudioSource tempSource = tempAudio.AddComponent<AudioSource>();
            tempSource.clip = audioSource.clip;
            tempSource.spatialBlend = 1f;
            tempSource.transform.position = transform.position;
            tempSource.Play();

            Destroy(tempAudio, tempSource.clip.length);
        }
        
        Destroy(gameObject);
    }
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

}