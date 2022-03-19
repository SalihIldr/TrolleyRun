using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicsMovement : MonoBehaviour
{
    [SerializeField] private Rigidbody _rigibody;
    [SerializeField] private SurfaceSlider _surfaceSlider;
    [SerializeField] private float _speed;
    [SerializeField] private Transform _transform;
    [SerializeField] private TrolleyHead _trolleyHead;
    [SerializeField] private PlayerInput _playerInput;

    private bool _isTurn = false;
    private int value = 0;
    private float oldSpeed;

    public void Move(Vector3 direction)
    {
        //if(_isTurn == true)
        //{
        //    oldSpeed = _speed;
        //    _speed = 20;
        //    value = value + 1;
        //    Debug.Log("Поворот");
        //    if(value == 5)
        //    {
        //        _speed = oldSpeed;
        //        value = 0;
        //        _isTurn = false;
        //        Debug.Log("Поворот всё");
        //    }

        //}

        if (_trolleyHead.Rails == true)
        {
            _speed = 150;
        }
        else
        {
            _speed = 120;
        }
        Vector3 directionAlongSurface = _surfaceSlider.Project(direction.normalized);
        Vector3 offset = directionAlongSurface * (_speed * Time.fixedDeltaTime);
        _rigibody.MovePosition(_rigibody.position + offset);
        //_rigibody.AddForce(_rigibody.position + offset);
        //_transform.Translate(transform.position + offset);
    }

    private void OnEnable()
    {

       
        _playerInput.PressedButtonRight += Drag;
        _playerInput.PressedButtonLeft += Drag;
    }

    private void OnDisable()
    {
       
        _playerInput.PressedButtonRight -= Drag;
        _playerInput.PressedButtonLeft -= Drag;

    }

    private void Drag()
    {
        _isTurn = true;

    }
}
