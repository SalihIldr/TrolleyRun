using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SurfaceSlider : MonoBehaviour
{
    [SerializeField] Rigidbody _rigidbody;
    [SerializeField] TrolleyHead _trolleyHead;
    [SerializeField] BoxCollider _collider;
    [SerializeField] Turnes _phisicTurnes;
    [SerializeField] PlayerInput _playerInput;
    [SerializeField] GameObject _dustTrails;
    [SerializeField] private float _stepSize;

    private bool _isFall = false;
    private bool _isTurn = false;
    private Vector3 _normal;
    private bool _jamp = false;
    private int _value = 4;
    private int _valueTurnDrag = 5;
    private float _mass = 10f;
    private bool _collision = true;
    private Vector3 _offsetCollider = new Vector3(0, 1f, 0);
    private Vector3 _previosCollider;
    private bool _phisicalRotation = true;
    private Vector3 _targetPosition;
    private bool _isRigibodyMass = false;
    private int _force = 2;

    public event UnityAction Jamped;
    public event UnityAction Landed;

    private void Start()
    {
        _previosCollider = _collider.size;
    }

    public Vector3 Project(Vector3 forward)
    {
        return forward - Vector3.Dot(forward, _normal) * _normal;
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.TryGetComponent(out Floor floor))
        {
            //_rigidbody.isKinematic = false;
            if(_isFall == false)
            {
                _dustTrails.SetActive(true);
                Debug.Log("Коснулся пола");
            }
          
        }

        //_collision = true;
        //_collider.size = _previosCollider;
       
        _normal = collision.contacts[0].normal;
    }
    //private void OnCollisionExit(Collision collision)
    //{
    //    if (collision.gameObject.TryGetComponent<Floor>(out Floor floor))
    //    {
    //        //_rigidbody.isKinematic = true;

    //        Debug.Log("Улетел с пола");
    //    }

    //    //_collision = true;
    //    //_collider.size = _previosCollider;

       
    //}


    //private void Update()
    //{
        
    //}

    private void FixedUpdate()
    {
        if (_jamp == false && _isFall == false)
        {
            _rigidbody.AddForce(-_normal * 500);
            
        }
        else if (_jamp == true)
        {
            //if(_isRigibodyMass == false)
            //{
            //    _rigidbody.mass = _mass;
            //    _isRigibodyMass = true;
            //}
            //_phisicalRotation = false;
            //_phisicTurnes.TurnOffPhysicalRotation(_phisicalRotation);

            _dustTrails.SetActive(false);
            //_rigidbody.mass = _mass;

            //_rigidbody.drag = 5.5f;
            //_rigidbody.mass = _rigidbody.mass+ 0.001f;
            //_mass = _mass + 0.02f;
            Jamped?.Invoke();
            //_rigidbody.AddForce(_normal *0.2f);

            _value = _value - 1;
            
            if (_value == 0)
            {
               
                //_phisicalRotation = true;
                //_phisicTurnes.TurnOffPhysicalRotation(_phisicalRotation);
                _rigidbody.mass = 1;
               
                _mass = 10f;
                _jamp = false;
                _isRigibodyMass = false;
                StartCoroutine(Landen());
                _value = 4;
                //_collision = true;
                //_collider.size = _previosCollider;

                //_value = 0;
            }
          
        }
       

    }

    

    private void OnEnable()
    {

        _trolleyHead.EncounteredObstacleHeadTrolley += Destruction;
        _playerInput.PressedButtonRight += Drag;
        _playerInput.PressedButtonLeft += Drag;
    }

    private void OnDisable()
    {
        _trolleyHead.EncounteredObstacleHeadTrolley -= Destruction;
        _playerInput.PressedButtonRight -= Drag;
        _playerInput.PressedButtonLeft -= Drag;

    }

    private void Drag()
    {
        _isTurn = true;

    }
   
    private void Destruction()
    {
        _isFall = true;
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.TryGetComponent<JampPlatform>(out JampPlatform jamp))
        {
            _jamp = true;
           
        }
    }

    private IEnumerator Landen()
    {
        WaitForSeconds wait = new WaitForSeconds(0.3f);
        yield return wait;
        Landed?.Invoke();

    }


   
}
