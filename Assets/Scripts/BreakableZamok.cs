using UnityEngine;
using UnityEngine.Events;

public class BreakableZamok : MonoBehaviour
{
    public GameObject brokenBoxPrefab;
    public float breakForceThreshold = 4f;

    [Header("Events")]
    public UnityEvent onBreak;

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

        if (brokenBoxPrefab != null)
            Instantiate(brokenBoxPrefab, transform.position, transform.rotation);

        onBreak.Invoke();

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
