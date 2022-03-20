using UnityEngine;

public class SurfaceSlider : MonoBehaviour
{
    [SerializeField] private Rigidbody _rigidbody;
    [SerializeField] private TrolleyHead _trolleyHead;
    [SerializeField] private GameObject _dustTrails;

    private Vector3 _normal;
    private bool _isFall = false;
    private bool _jamp = false;
    private int _fixedUpdateCount = 0;
    private int _jumpLimit = 4;
    private float _forceDown = 500;

    public Vector3 Project(Vector3 forward)
    {
        return forward - Vector3.Dot(forward, _normal) * _normal;
    }

    private void OnCollisionEnter(Collision collision)
    {
        _normal = collision.contacts[0].normal;
    }

    private void FixedUpdate()
    {
        if (_jamp == false && _isFall == false)
        {
            _rigidbody.AddForce(-_normal * _forceDown);
        }
        if (_jamp == true)
        {
            _dustTrails.SetActive(false);
            ++_fixedUpdateCount;
            if (_fixedUpdateCount == _jumpLimit)
            {
                _jamp = false;
                _fixedUpdateCount = 0;
            }
        }
    }

    private void OnEnable()
    {
        _trolleyHead.EncounteredObstacleHeadTrolley += Destruction;
    }

    private void OnDisable()
    {
        _trolleyHead.EncounteredObstacleHeadTrolley -= Destruction;
    }

    private void Destruction()
    {
        _isFall = true;
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.TryGetComponent(out JampPlatform jamp))
        {
            _jamp = true;
        }
    }
}
