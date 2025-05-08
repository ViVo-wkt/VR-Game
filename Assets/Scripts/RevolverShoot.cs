using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;
using static UnityEngine.Rendering.GPUSort;

public class RevolverShoot : MonoBehaviour
{
    [Header("Input")]
    public InputActionReference triggerAction;

    [Header("Bullet")]
    public GameObject bulletPrefab;
    public Transform shootPoint;
    public Transform spawnPoint;

    [Header("Audio")]
    public AudioSource audioSource;
    public AudioClip shootClip;

    public bool hasBullet = false;

    private void OnEnable()
    {
        triggerAction.action.performed += OnTriggerPressed;
        triggerAction.action.Enable();
    }

    private void OnDisable()
    {
        triggerAction.action.performed -= OnTriggerPressed;
        triggerAction.action.Disable();
    }

    private void OnTriggerPressed(InputAction.CallbackContext context)
    {
        if (hasBullet)
        {
            Shoot();
            hasBullet = false;
        }
    }

    private void Shoot()
    {
        Debug.Log("БАХ!");

        if (bulletPrefab && shootPoint)
        {
            GameObject bullet = Instantiate(bulletPrefab, shootPoint.position, shootPoint.rotation);

            Rigidbody rb = bullet.GetComponent<Rigidbody>();
            if (rb != null)
            {
                float bulletSpeed = 50f;
                rb.linearVelocity = shootPoint.forward * bulletSpeed;

            }
        }

        if (audioSource && shootClip)
            audioSource.PlayOneShot(shootClip);
    }
    public void SpawnBullet(SelectEnterEventArgs args)
    {
        Instantiate(bulletPrefab, spawnPoint.position, Quaternion.identity);
    }
}