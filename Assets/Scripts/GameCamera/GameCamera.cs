using UnityEngine;
using UnityEngine.Animations;

public class GameCamera : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    [SerializeField] private PositionConstraint _positionConstraint;
    [SerializeField] private TrolleyHead _trolleyHead;

    private void OnEnable()
    {
        _trolleyHead.TurnedLong += DisablePositionConstraint;
    }

    private void OnDisable()
    {
        _trolleyHead.TurnedLong -= DisablePositionConstraint;
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.TryGetComponent(out Lift lift))
        {
            _animator.SetBool("Lift", true);
        }
    }

    private void DisablePositionConstraint()
    {
        _positionConstraint.enabled = false;
    }
}
