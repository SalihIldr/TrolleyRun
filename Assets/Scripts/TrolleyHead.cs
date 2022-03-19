using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TrolleyHead : MonoBehaviour
{
    [SerializeField] private Rigidbody _rigidbody;
    [SerializeField] private CinemachineVirtualCamera _virtualCamera;
    [SerializeField] private GameObject _fireRails;
    [SerializeField] private GameObject _dustTrails;
    [SerializeField] private Animator _animator;
    

    private bool _isObtacleActiv = false;
    public bool Rails = false;

    public event UnityAction EncounteredObstacleHeadTrolley;
    public event UnityAction PressedAnyKey;



    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.TryGetComponent<Obstacle>(out Obstacle obstacle))
        {
            if (_isObtacleActiv == false)
            {
                EncounteredObstacleHeadTrolley?.Invoke();
                _fireRails.SetActive(false);
                _dustTrails.SetActive(false);
                _rigidbody.AddForce(Vector3.up * 1000);
                _rigidbody.freezeRotation = false;
                _rigidbody.AddTorque(0f, 800f, 0f);
                _virtualCamera.Follow = null;
                _virtualCamera.LookAt = null;
                //_rigidbody.drag = 100;
                _rigidbody.mass = 100;

                //_rigidbody.AddTorque(0f, 1000f, 0f);
                _isObtacleActiv = true;
            }

            //_rigidbody.AddTorque(600f, 0f, 0f);


        }
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.TryGetComponent<Rails>(out Rails rails))
        {
            _fireRails.SetActive(true);
            _dustTrails.SetActive(false);
            Rails = true;
            _animator.enabled = true;


        }

        if(collision.gameObject.TryGetComponent<Turn>(out Turn turn))
        {
            PressedAnyKey?.Invoke();
        }
    }
    private void OnTriggerExit(Collider collision)
    {
        if (collision.gameObject.TryGetComponent<Rails>(out Rails rails))
        {
            _fireRails.SetActive(false);
            _dustTrails.SetActive(true);
            Rails = false;
            _animator.enabled = false;
           
            //_rigidbody.AddForce(-Vector3.forward);
        }

    }
}
