using UnityEngine;
using UnityEngine.Events;

public class PlayerInput : MonoBehaviour
{
    public event UnityAction PressedButtonRight;
    public event UnityAction PressedButtonLeft;

    public void PressingButtonRight()
    {
        PressedButtonRight?.Invoke();
    }

    public void PressingButtonLeft()
    {
        PressedButtonLeft?.Invoke();
    }
}
