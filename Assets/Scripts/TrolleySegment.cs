using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TrolleySegment : MonoBehaviour
{
    [SerializeField] private Rigidbody _rigidbody;
    [SerializeField] private ParticleSystem _particleSystem;
    [SerializeField]  private GameObject _fireRails;
    [SerializeField] private GameObject _dustTrails;
    [SerializeField] private Animator _animator;
    //private TrolleyHead _trolleyHead;
    Vector3 _target;
    private Vector3 _normal;
    private bool _jamp = false;
    private int _value = 0;
    private float _mass = 0.1f;
    private bool _isStop = false;
    private bool _isObtacleActiv = false;

    public bool IsStop => _isStop;

    public event UnityAction EncounteredObstacle;

    //private void Awake()
    //{
    //    _trolleyHead = null;
    //}

    //private void OnEnable()
    //{

    //    _trolleyHead.EncounteredObstacleHeadTrolley += Destruction;
    //}

    //private void OnDisable()
    //{
    //    _trolleyHead.EncounteredObstacleHeadTrolley -= Destruction;

    //}

    //private void Destruction()
    //{
    //    _isStop = true;
    //}

    public void InitDirection(Vector3 target)
    {
        _target = target;
       
        
        
    }
    //public void InitHead(TrolleyHead trolleyHead)
    //{
    //    _trolleyHead = trolleyHead;
    //}

    //private void Start()
    //{
    //    _particleSystem.Play();
    //}

    private void OnCollisionEnter(Collision collision)
    {
       
        _normal = collision.contacts[0].normal;
        if (_isObtacleActiv == false)
        {
            _dustTrails.SetActive(true);
        }
        

        if (collision.gameObject.TryGetComponent<Obstacle>(out Obstacle obstacle))
        {
            _isStop = true;
            _dustTrails.SetActive(false);
            _fireRails.SetActive(false);
            if (_isObtacleActiv == false)
            {
                EncounteredObstacle?.Invoke();

                ////_rigidbody.AddForce(Vector3.up * 1000);
                //_rigidbody.freezeRotation = false;
                ////_rigidbody.AddTorque(0f, 800f, 0f);
                ////_rigidbody.drag = 100;
                //_rigidbody.mass = 500;

                ////_rigidbody.AddTorque(0f, 1000f, 0f);
                _isObtacleActiv = true;
            }

            //_rigidbody.AddTorque(600f, 0f, 0f);


        }

    }
    //private void OnCollisionExit(Collision collision)
    //{
    //    _dustTrails.SetActive(false);
    //}

    private void FixedUpdate()
    {
        if (_isStop == false)
        {
            transform.LookAt(_target);
            if (_jamp == true)
            {
                _dustTrails.SetActive(false);
                //_rigidbody.mass = 0.1f;
                Debug.Log("kkkk");
                _rigidbody.AddForce(_normal * 2f);
                _value = _value + 1;
                //}
                //if (_value == 40)
                //{
                //    _rigidbody.mass = 1;
                //    _jamp = false;
                //    _value = 0;
                //_rigidbody.mass = _mass;
                //_mass = _mass + 0.01f;
                if (_value == 3) /*(_mass >= 1)*/
                {
                    Debug.Log("k---kkk");
                    //_rigidbody.mass = 1;
                    //_mass = 0.1f;
                    _jamp = false;
                    _value = 0;
                    
                }

            }
        }
        //if(_isStop==true&& _isObtacleActiv == false)
        //{
        //    Debug.Log("fff");
        //    EncounteredObstacle?.Invoke();
        //    _rigidbody.AddForce(Vector3.up * 1000);
        //    _rigidbody.freezeRotation = false;
        //    _rigidbody.AddTorque(0f, 800f, 0f);
        //    //_rigidbody.drag = 100;
        //    _rigidbody.mass = 500;
        //    _isObtacleActiv = true;
        //}

        ////_rigidbody.AddForce(_normal * 50);
        //_value = _value + 1;
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
        if (collision.gameObject.TryGetComponent<JampPlatform>(out JampPlatform jamp))
        {
            _jamp = true;
            Debug.Log("hhh");
        }
        if (collision.gameObject.TryGetComponent<Rails>(out Rails rails))
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
        if (collision.gameObject.TryGetComponent<Rails>(out Rails rails))
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

    //[SerializeField] Rigidbody _rigidbody;
    //private Vector3 _normal;
    //private void FixedUpdate()
    //{

    //    _rigidbody.AddForce(-_normal * 500);



    //}

    //private void OnCollisionEnter(Collision collision)
    //{
    //    _normal = collision.contacts[0].normal;
    //}
    //private void OnTriggerEnter(Collider collision)
    //{
    //    if (collision.gameObject.TryGetComponent<AddMen>(out AddMen addMen))
    //    {
    //        AddMen();
    //        Destroy(addMen.gameObject);
    //        Debug.Log("aaaaahhh");
    //    }
    //    if (collision.gameObject.TryGetComponent<Obstacle>(out Obstacle obstacle))
    //    {
    //        _isFall = true;
    //        //AddMen();
    //        //Destroy(addMen.gameObject);
    //        //Debug.Log("aaaaahhh");
    //    }

    //}

}
