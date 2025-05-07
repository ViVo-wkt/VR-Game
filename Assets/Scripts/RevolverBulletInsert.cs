using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine;

public class BulletChamber : MonoBehaviour
{
    public RevolverShoot revolverScript;

    public void OnBulletInserted(SelectEnterEventArgs args)
    {
        revolverScript.hasBullet = true;
    }

    public void OnBulletLeave(SelectExitEventArgs args)
    {
        revolverScript.hasBullet = false;
    }
}
