using UnityEngine;

public class MoverTrolley : MonoBehaviour
{
    [SerializeField] private PlayerInput _playerInput;
    [SerializeField] private Rigidbody _rigidbody;
    [SerializeField] private PhysicsMovement _movement;
    [SerializeField] private TrolleyHead _trolleyHead;
    [SerializeField] private SurfaceSlider _surfaceSlider;
    [SerializeField] private float _speed;

    private Vector3 _turn = new Vector3(0, 0, 0);
    private Vector3 _longTurn = new Vector3(0, 0, 0);
    private Quaternion _quaternion;
    private bool _isTurn = false;
    private bool _isLongTurn = false;
    private bool _isStop = false;
    private int _fixedUpdateCount = 0;
    private int _directionOfMovement = 1;
    private int _rotationRestriction = 17;

    private void OnEnable()
    {
        _playerInput.PressedButtonRight += MoveRight;
        _playerInput.PressedButtonLeft += MoveLeft;
        _trolleyHead.TurnedLong += LongTurn;
        _trolleyHead.EncounteredObstacleHeadTrolley += Destruction;
    }

    private void OnDisable()
    {
        _trolleyHead.EncounteredObstacleHeadTrolley -= Destruction;
        _trolleyHead.TurnedLong -= LongTurn;
        _playerInput.PressedButtonRight -= MoveRight;
        _playerInput.PressedButtonLeft -= MoveLeft;
    }

    private void LongTurn()
    {
        _isLongTurn = true;
    }

    private void MoveLeft()
    {
        Turn(-_directionOfMovement);
    }

    private void MoveRight()
    {
        Turn(_directionOfMovement);
    }

    private void Turn(int directionOfMovement)
    {
        _isTurn = true;
        _turn = new Vector3(directionOfMovement * 0.2f, 0, 0);
        _quaternion = Quaternion.Euler(0, directionOfMovement * 20, 0);
    }

    private void Destruction()
    {
        _isStop = true;
    }

    private void FixedUpdate()
    {
        if (_isStop == false)
        {
            _movement.Move(Vector3.forward + _turn + _longTurn);
            _rigidbody.rotation = Quaternion.RotateTowards(transform.rotation, _quaternion, _speed * Time.fixedDeltaTime);
            if (_isTurn == true)
            {
                ++_fixedUpdateCount;
                if (_fixedUpdateCount == _rotationRestriction)
                {
                    _isTurn = false;
                    _fixedUpdateCount = 0;
                    _quaternion = Quaternion.Euler(0, 0, 0);
                    _turn = new Vector3(0, 0, 0);
                }
            }
            if (_isLongTurn == true)
            {
                _longTurn = new Vector3(-0.5f, 0, 0);
                _quaternion = Quaternion.Euler(0, -20, 0);
            }
        }
    }
}
