using UnityEngine;

public class StickMan : MonoBehaviour
{
    [SerializeField] protected Rigidbody[] _rootRigidbodies;
    [SerializeField] protected GameObject _sceletRigibody;
    [SerializeField] private TrolleyHead _trolleyHead;

    protected void Awake()
    {
        _sceletRigibody.SetActive(false);
    }

    private void OnEnable()
    {
        _trolleyHead.EncounteredObstacleHeadTrolley += Destruction;
    }

    private void OnDisable()
    {
        _trolleyHead.EncounteredObstacleHeadTrolley -= Destruction;
    }

    protected void Destruction()
    {
        _sceletRigibody.SetActive(true);
    }
}
