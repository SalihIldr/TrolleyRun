using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StickMan : MonoBehaviour
{
    [SerializeField] Rigidbody[] _rigidbodies;
    //[SerializeField] Collider[] _colliders;
    [SerializeField] protected GameObject _sceletRigibody;
    [SerializeField] private TrolleyHead _trolleyHead;

    protected void Awake()
    {
        _sceletRigibody.SetActive(false);

       
        //for (int i = 0; i < _colliders.Length; i++)
        //{
        //    _colliders[i].isTrigger = true;
        //}
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
        //for (int i = 0; i < _rigidbodies.Length; i++)
        //{
        //    _rigidbodies[i].AddForce(Vector3.up * 8000);
        //}

    }
    //{
    //    for (int i = 0; i < _rigidbodies.Length; i++)
    //    {
    //        _rigidbodies[i].isKinematic = true;
    //        _rigidbodies[i].useGravity = false;
    //    }
        //for (int i = 0; i < _colliders.Length; i++)
        //{
        //    _colliders[i].isTrigger = true;
        //}
    
}
