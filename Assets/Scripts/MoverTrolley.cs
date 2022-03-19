using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoverTrolley : MonoBehaviour
{
    [SerializeField] private PlayerInput _playerInput;
    [SerializeField] private Rigidbody _rigidbody;
    [SerializeField] private PhysicsMovement _movement;
    //[SerializeField] private float _moveSpeed;
    [SerializeField] private float _stepSize;
    [SerializeField] private TrolleyHead _trolleyHead;

    
    private Vector3 _turn;
    private Vector3 _longTurn = new Vector3(0, 0, 0);
    private bool _isTurn = false;
    private bool _isLongTurn = false;
    private int _value = 0;
    private Quaternion _quaternion;
    private float _x=0;

    [SerializeField] private SurfaceSlider _surfaceSlider;
    [SerializeField] private float _speed;

    private bool _isStop = false;
   

   



    private void OnEnable()
    {
        _playerInput.PressedButtonRight += MoveRight;
        _playerInput.PressedButtonLeft += MoveLeft;
        _trolleyHead.PressedAnyKey += LongTurn;
        _trolleyHead.EncounteredObstacleHeadTrolley += Destruction;
    }

    private void OnDisable()
    {
        _trolleyHead.EncounteredObstacleHeadTrolley -= Destruction;
        _trolleyHead.PressedAnyKey -= LongTurn;
        _playerInput.PressedButtonRight -= MoveRight;
        _playerInput.PressedButtonLeft -= MoveLeft;
        
    }

    private void Start()
    {
      
        _turn = new Vector3(0, 0, 0);
    }

    private void LongTurn()
    {
        //_turn = new Vector3(-1, 0, 0);
        //_quaternion = Quaternion.Euler(0, -100, 0);
        _isLongTurn = true;
    }

    

    private void MoveLeft()
    {
       
        Debug.Log("Left");
        _turn = new Vector3(-0.2f, 0, 0);
        _quaternion = Quaternion.Euler(0, -20, 0);
        _isTurn = true;
    }
    private void MoveRight()
    {
        _isTurn = true;
        _turn = new Vector3(0.2f, 0, 0);
        _quaternion = Quaternion.Euler(0, 20, 0);
       
    }

    private void Destruction()
    {
        _isStop = true;
    }

   

    private void FixedUpdate()
    {


       
        if(_isStop == false)
        {
            _movement.Move(Vector3.forward+_turn + _longTurn);
            
            _rigidbody.rotation = Quaternion.RotateTowards(transform.rotation, _quaternion, _speed * Time.fixedDeltaTime);

            if (_isTurn == true)
            {
                _value = _value + 1;
                //_turn = new Vector3(_x, 0, 0);
                //_x = _x + 0.1f;
                //_rigidbody.AddForce(-100f*(Vector3.forward + _turn + _longTurn));
                if (_value == 17 /*_x >= 1*/)
                {
                    //_x = 0;
                    _isTurn = false;
                    _value = 0;
                    _quaternion = Quaternion.Euler(0, 0, 0);
                    _turn = new Vector3(0, 0, 0);
                }


            }
            if(_isLongTurn == true)
            {
                ////_value = _value + 1;
                ////if (_value == 30)
                ////{
                ////_rigidbody.AddForce(_longTurn * 2);
                //_isLongTurn = false;
                ////_value = 0;
                ////_quaternion = Quaternion.Euler(0, 0, 0);
                _longTurn = new Vector3(-0.5f, 0, 0);
                _quaternion = Quaternion.Euler(0, -20, 0);
                //}
            }



        }
        
        }
}
