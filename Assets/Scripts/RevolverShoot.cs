using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactors;
using static UnityEngine.Rendering.GPUSort;

public class RevolverShoot : MonoBehaviour
{
    [Header("Input")]
    public InputActionReference triggerAction;

    [Header("Bullet")]
    public GameObject LoadbulletPrefab;
    public GameObject ShootbulletPrefab;
    public Transform shootPoint;
    public Transform spawnPoint;

    [Header("Audio")]
    public AudioSource audioSource;
    public AudioClip shootClip;
    public AudioClip missClip;

    [Header("XR")]
    public XRSocketInteractor bulletSocket;

    public bool hasBullet = false;

    private GameObject loadedBulletObject;

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
        else
        {
            audioSource.PlayOneShot(missClip);
        }
    }

    private void Shoot()
    {
        Debug.Log("БАХ!");

        if (ShootbulletPrefab && shootPoint)
        {
            GameObject bullet = Instantiate(ShootbulletPrefab, shootPoint.position, shootPoint.rotation);

            Rigidbody rb = bullet.GetComponent<Rigidbody>();
            if (rb != null)
            {
                float bulletSpeed = 50f;
                rb.linearVelocity = shootPoint.forward * bulletSpeed;
                bulletSocket.EndManualInteraction();

                var interactable = bulletSocket.interactablesSelected.FirstOrDefault();
                if (interactable != null)
                {
                    Destroy(loadedBulletObject);
                    loadedBulletObject = null;
                }
            }
        }

        if (audioSource && shootClip)
            audioSource.PlayOneShot(shootClip);
    }
    public void SpawnBullet(SelectEnterEventArgs args)
    {
        if (loadedBulletObject = null)
        {
            loadedBulletObject = args.interactableObject.transform.gameObject;
            Instantiate(loadedBulletObject, spawnPoint.position, Quaternion.identity);
        }
        
    }
}