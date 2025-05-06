using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine;

public class BulletChamber : MonoBehaviour
{
    public RevolverShoot revolverScript;

    public void OnBulletInserted(SelectEnterEventArgs args)
    {
        revolverScript.hasBullet = true;
        Destroy(args.interactableObject.transform.gameObject); // патрон исчезает при вставке
    }
}
