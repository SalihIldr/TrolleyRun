using UnityEngine;
using UnityEngine.Events;

public class TrolleyHead : MonoBehaviour
{
    [SerializeField] private GameObject _fireRails;
    [SerializeField] private GameObject _dustTrails;
    [SerializeField] private Animator _animator;

    private bool _isObtacle = false;

    public event UnityAction EncounteredObstacleHeadTrolley;
    public event UnityAction TurnedLong;
    public event UnityAction RidingOnRails;
    public event UnityAction RodeOnGround;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.TryGetComponent(out Obstacle obstacle))
        {
            EncounteredObstacleHeadTrolley?.Invoke();
            _fireRails.SetActive(false);
            _dustTrails.SetActive(false);
            _isObtacle = true;
        }
        if (collision.gameObject.TryGetComponent(out Floor floor))
        {
            if (_isObtacle == false)
            {
                _dustTrails.SetActive(true);
            }
        }
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.TryGetComponent(out Rails rails))
        {
            if (_isObtacle == false)
            {
                _fireRails.SetActive(true);
                _dustTrails.SetActive(false);
                _animator.enabled = true;
                RidingOnRails?.Invoke();
            }
        }
        if (collision.gameObject.TryGetComponent(out LongTurn turn))
        {
            TurnedLong?.Invoke();
        }
    }

    private void OnTriggerExit(Collider collision)
    {
        if (collision.gameObject.TryGetComponent(out Rails rails))
        {
            if (_isObtacle == false)
            {
                _fireRails.SetActive(false);
                _dustTrails.SetActive(true);
                _animator.enabled = false;
                RodeOnGround?.Invoke();
            }
        }
    }
}
