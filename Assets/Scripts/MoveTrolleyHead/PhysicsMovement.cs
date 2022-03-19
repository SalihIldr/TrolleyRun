using UnityEngine;

public class PhysicsMovement : MonoBehaviour
{
    [SerializeField] private Rigidbody _rigibody;
    [SerializeField] private SurfaceSlider _surfaceSlider;    
    [SerializeField] private TrolleyHead _trolleyHead;   

    private float _speed = 120;

    public void Move(Vector3 direction)
    {
        Vector3 directionAlongSurface = _surfaceSlider.Project(direction.normalized);
        Vector3 offset = directionAlongSurface * (_speed * Time.fixedDeltaTime);
        _rigibody.MovePosition(_rigibody.position + offset);
    }

    private void OnEnable()
    {
        _trolleyHead.RidingOnRails += IncreaseSpeed;
        _trolleyHead.RodeOnGround += LowerSpeed;
    }

    private void OnDisable()
    {
        _trolleyHead.RidingOnRails -= IncreaseSpeed;
        _trolleyHead.RodeOnGround -= LowerSpeed;
    }

    private void IncreaseSpeed()
    {
        _speed = 150;
    }

    private void LowerSpeed()
    {
        _speed = 120;
    }
}
