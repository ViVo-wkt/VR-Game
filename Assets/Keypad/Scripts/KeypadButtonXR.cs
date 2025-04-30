using UnityEngine;


namespace NavKeypad
{
    [RequireComponent(typeof(UnityEngine.XR.Interaction.Toolkit.Interactables.XRSimpleInteractable))]
    public class KeypadButtonXR : MonoBehaviour
    {
        [SerializeField] private KeypadButton keypadButton;

        private void Awake()
        {
            var interactable = GetComponent<UnityEngine.XR.Interaction.Toolkit.Interactables.XRSimpleInteractable>();
            interactable.selectEntered.AddListener(_ => keypadButton.PressButton());
        }
    }
}
