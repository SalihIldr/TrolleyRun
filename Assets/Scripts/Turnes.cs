using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turnes : MonoBehaviour
{
    [SerializeField] private Rigidbody _rigidbody;

    [SerializeField] private PlayerInput _playerInput;
    [SerializeField] private TrolleyHead _trolleyHead;

    private Camera _camera;

    private Vector3 _turn;
    private Vector3 _rotate;
    private Vector3 _rotateDefaul;
    private bool _isTurn = false;
    private bool _isJamp = false;
    private int _value = 0;
    private Vector3 _turnProba;
    private Quaternion _quaternion;
    private bool _isMoveStop = false;
    private bool _isPhisicalRatation = true;

    private void Start()
    {
        _quaternion = Quaternion.Euler(0, 0, 0);
        _rotateDefaul = new Vector3(/*transform.rotation.x*/0, 0, 0);
        _camera = Camera.main;
    }

    private void OnEnable()
    {
        _playerInput.PressedButtonRight += MoveRight;
        _playerInput.PressedButtonLeft += MoveLeft;
        _trolleyHead.EncounteredObstacleHeadTrolley += MoveStop;
    }

    private void OnDisable()
    {
        _playerInput.PressedButtonRight -= MoveRight;
        _playerInput.PressedButtonLeft -= MoveLeft;
        _trolleyHead.EncounteredObstacleHeadTrolley -= MoveStop;
    }

    private void MoveRight()
    {
        //if (_isPhisicalRatation == true)
        //{
            _turn = new Vector3(1, 0, 0);
            _rotate = new Vector3(0, 20, 0);
            _isTurn = true;
            _turnProba = new Vector3(20, 0, 0);
            _quaternion = Quaternion.Euler(0, 20, 0);
            //_rigidbody.rotation = new Quaternion
        //}


    }
    private void MoveLeft()
    {
        //if(_isPhisicalRatation == true)
        //{
            _turnProba = new Vector3(-20, 0, 0);
            _rotate = new Vector3(0, -20, 0);
            _turn = new Vector3(-1, 0, 0);
            _isTurn = true;
            _quaternion = Quaternion.Euler(0, -20, 0);
        //}
        
    }

    private void MoveStop()
    {
        _isMoveStop = true;
    }



    private void FixedUpdate()
    {

        //////if(_isTurn == true)
        //////{
        //////    _rigidbody.velocity = /*new Vector3(105, 0, 0)*/ _turn;
        //////    _isTurn = false;

        //////}

        if (_isMoveStop == false)
        {
            _rigidbody.rotation = Quaternion.RotateTowards(transform.rotation, _quaternion, 60 * Time.fixedDeltaTime);

            if (_isTurn == true)
            {
                _rigidbody.AddRelativeForce(_turn * 14, ForceMode.VelocityChange);
                Debug.Log("hhhповернул физически");

                _value = _value + 1;

                if (_value == 24)
                {

                    _isTurn = false;
                    _value = 0;
                    _quaternion = Quaternion.Euler(0, 0, 0);
                }



                ///////* _/**/rigidbody.velocity = _turnProba;

                //////_rigidbody.velocity = Vector3.ClampMagnitude(_turn, 13);
                //////_rigidbody.velocity = _turn;
                //////Quaternion deltaRotation = Quaternion.Euler(_rotate * Time.fixedDeltaTime);





                //////_rigidbody.AddRelativeTorque(_rotate);
                //////_rigidbody.AddTorque(_rotate);


                //////_rigidbody.MoveRotation(_rigidbody.rotation*deltaRotation);







                //////}
                //////if (_isJamp == true)
                //////{
                //////    _rigidbody.AddRelativeForce(_turn * 13, ForceMode.VelocityChange);
                //////}
                //////if (transform.position.x == 8 )
                //////{


                //////    _isTurn = false;
                //////    Debug.Log("ggg");
                //////    Debug.Log(transform.position.x);
                //////}
                //////if (transform.position.x == 0 )
                //////{

                //////}
                //////if (transform.position.x >= -8)
                //////{

            }
            if (_isTurn == true && _isPhisicalRatation == false)
            {
                _rigidbody.AddRelativeForce(_turn *2, ForceMode.VelocityChange);
                Debug.Log("Повернул в воздухе");

                _value = _value + 1;

                if (_value == 3)
                {
                    _isTurn = false;
                    _value = 0;
                    _quaternion = Quaternion.Euler(0, 0, 0);
                }
            }

        }

        

    }

    public void TurnOffPhysicalRotation(bool PhisicalRotation)
    {
        _isPhisicalRatation = PhisicalRotation;
        Debug.Log("Chf,jnfkj");
    }

    //private void FixedUpdate()
    //{
    //    Vector3 screenMousePosition = Input.mousePosition;
    //    Vector3 worldMousePosition = _camera.ScreenToWorldPoint(screenMousePosition);
    //    transform.LookAt(worldMousePosition);
    //}

    //private void OnCollisionEnter(Collision collision)
    //{
    //    if (collision.gameObject.TryGetComponent<Wall>(out Wall cottonBlock))
    //    {
    //        _isTurn = false;
    //    }
    //}


}
