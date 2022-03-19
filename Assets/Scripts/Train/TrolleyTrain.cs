using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrolleyTrain : MonoBehaviour
{
    [SerializeField] private TrolleyHead _trolleyHead;
    [SerializeField] private float _segmentPrentess;
    [SerializeField] private TrolleySegment _trolleySegment;

    private List<TrolleySegment> _trolleySegments;
    private bool _isFall = false;
    private float _forceUp = 1000;
    private float _forceDown = 1500;

    private void Awake()
    {
        _trolleySegments = new List<TrolleySegment>();
    }

    private void OnEnable()
    {
        _trolleyHead.EncounteredObstacleHeadTrolley += Destruction;
    }

    private void OnDisable()
    {
        _trolleyHead.EncounteredObstacleHeadTrolley -= Destruction;
    }

    private void FixedUpdate()
    {
        if (_isFall == false)
        {
            Move();
        }
        else
        {
            MoveStop();
        }
    }

    private void AddMen()
    {
        _trolleySegments.Add(Instantiate(_trolleySegment, transform));
        _trolleySegments[_trolleySegments.Count - 1].PlayParticle();
    }

    private void Move()
    {
        Vector3 nextPosition = _trolleyHead.transform.position;
        foreach (var segment in _trolleySegments)
        {
            Vector3 tempPosistion = segment.transform.position;
            segment.transform.position = Vector3.Lerp(segment.transform.position, nextPosition, _segmentPrentess * Time.fixedDeltaTime);
            segment.InitDirection(nextPosition);
            nextPosition = tempPosistion;
        }
    }

    private void MoveStop()
    {
        foreach (var segment in _trolleySegments)
        {
            segment.Obtacle();
        }
    }

    private void Destruction()
    {
        _isFall = true;
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.TryGetComponent(out WhiteStickMan addMen))
        {
            AddMen();
            Destroy(addMen.gameObject);
            foreach (var segment in _trolleySegments)
            {
                segment.TryGetComponent(out Rigidbody rigidbody);
                rigidbody.AddForce(Vector3.up * _forceUp);
                StartCoroutine(Bouncing(rigidbody));
            }
        }
    }

    private IEnumerator Bouncing(Rigidbody rigidbody)
    {
        WaitForSeconds wait = new WaitForSeconds(0.1f);
        yield return wait;
        rigidbody.AddForce(-Vector3.up * _forceDown);
    }
}
