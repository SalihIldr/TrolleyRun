using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class GameCamera : MonoBehaviour
{
    [SerializeField] private SurfaceSlider _surfaceSlider;
    //[SerializeField] private PositionConstraint _positionConstraint;
    [SerializeField] private Transform _target;
    [SerializeField] private Animator _animator;
    [SerializeField] private PositionConstraint _positionConstraint;
    [SerializeField] private TrolleyHead _trolleyHead;

    //[SerializeField] private Axis _axis;

    private float _speed=60;
    private bool _isMove = false;


    private void OnEnable()
    {
      
        _trolleyHead.PressedAnyKey += LongTurn;
       
    }

    private void OnDisable()
    {
       
        _trolleyHead.PressedAnyKey -= LongTurn;
        

    }


    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.TryGetComponent(out Ramp ramp))
        {

            _animator.SetBool("Lift", true);
            //_animator.StopPlayback();
            //_positionConstraint.enabled = false;
            //_positionConstraint.translationAxis = Axis.None;
            //StartCoroutine(Turn());

            //_jamp = true;
            //_target = 
            Debug.Log("LIFT");
        }
        if (collision.gameObject.TryGetComponent(out Turn turn))
        {
            //_positionConstraint.translationAxis = Axis.None;
            //_positionConstraint.enabled = true;
        }




    }

    private void LongTurn()
    {
        _positionConstraint.enabled = false;
    }



    //IEnumerator  Turn()
    //{
    //    WaitForSeconds wait = new WaitForSeconds(0.3f);
    //    yield return wait;
    //    _positionConstraint.translationAxis = Axis.Z;
    //}
    //private void FixedUpdate()
    //{


    //    if (_isMove == true)
    //    {
    //        //transform.LookAt(_target);
    //        transform.position = Vector3.MoveTowards(transform.position, transform.position + new Vector3(0, 0, 20), _speed * Time.fixedDeltaTime);
    //        //transform.Translate(new Vector3(0, 0, 1) * _speed * Time.deltaTime);
    //    }

    //}




    //private void OnEnable()
    //{
    //    _surfaceSlider.Jamped += JumpingCameraBehavior;
    //    _surfaceSlider.Landed += LandedCameraBehavior;
    //}

    //private void OnDisable()
    //{
    //    _surfaceSlider.Jamped += JumpingCameraBehavior;
    //    _surfaceSlider.Landed += LandedCameraBehavior;
    //}

    //private void FixedUpdate()
    //{


    //    if (_isMove == true)
    //    {
    //        //transform.LookAt(_target);
    //        transform.position = Vector3.MoveTowards(transform.position, transform.position+new Vector3(0,0,20), _speed * Time.fixedDeltaTime);
    //        //transform.Translate(new Vector3(0, 0, 1) * _speed * Time.deltaTime);
    //    }

    //}

    //private void JumpingCameraBehavior()
    //{
    //    //transform.localRotation = Quaternion.Euler(0, 0, 0);
    //    _positionConstraint.enabled = false;
    //    _isMove = true;

    //}
    //private void LandedCameraBehavior()
    //{
    //    //transform.localRotation = Quaternion.Euler(18f, 0, 0);
    //    _positionConstraint.enabled = true;
    //    _isMove = false;
    //}


}
