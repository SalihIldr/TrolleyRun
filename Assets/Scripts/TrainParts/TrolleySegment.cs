using UnityEngine;
using UnityEngine.Events;

public class TrolleySegment : MonoBehaviour
{    
    [SerializeField] private ParticleSystem _particleSystem;
    [SerializeField] private GameObject _fireRails;
    [SerializeField] private GameObject _dustTrails;
    [SerializeField] private Animator _animator;

    private Vector3 _target;
    private bool _isJamp = false;
    private bool _isStop = false;
    private bool _isObtacleActiv = false;
    private int _fixedUpdateCount = 0;
    private int _jumpLimit = 4;

    public bool IsStop => _isStop;

    public event UnityAction EncounteredObstacle;

    public void InitDirection(Vector3 target)
    {
        _target = target;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.TryGetComponent(out Floor floor))
        {
            if (_isObtacleActiv == false)
            {
                _dustTrails.SetActive(true);
            }
        }       
        if (collision.gameObject.TryGetComponent(out Obstacle obstacle))
        {
            _isStop = true;
            _dustTrails.SetActive(false);
            _fireRails.SetActive(false);
            if (_isObtacleActiv == false)
            {
                EncounteredObstacle?.Invoke();
                _isObtacleActiv = true;
            }
        }
    }

    private void FixedUpdate()
    {
        if (_isStop == false)
        {
            transform.LookAt(_target);
            if (_isJamp == true)
            {
                _dustTrails.SetActive(false);
                ++_fixedUpdateCount;
                if (_fixedUpdateCount == _jumpLimit)
                {
                    _isJamp = false;
                    _fixedUpdateCount = 0;
                }
            }
        }
    }

    public void Obtacle()
    {
        EncounteredObstacle?.Invoke();
        _dustTrails.SetActive(false);
        _fireRails.SetActive(false);
        _isObtacleActiv = true;
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.TryGetComponent(out JampPlatform jamp))
        {
            _isJamp = true;
        }
        if (collision.gameObject.TryGetComponent(out Rails rails))
        {
            if (_isObtacleActiv == false)
            {
                _fireRails.SetActive(true);
                _dustTrails.SetActive(false);
            }
            _animator.enabled = true;
        }
    }

    private void OnTriggerExit(Collider collision)
    {
        if (collision.gameObject.TryGetComponent(out Rails rails))
        {
            if (_isObtacleActiv == false)
            {
                _fireRails.SetActive(false);
                _dustTrails.SetActive(true);
            }
            _animator.enabled = false;
        }
    }

    public void PlayParticle()
    {
        _particleSystem.Play();
    }
}
