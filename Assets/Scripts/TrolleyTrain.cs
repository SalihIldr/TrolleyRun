using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrolleyTrain : MonoBehaviour
{
   [SerializeField] private TrolleyHead _trolleyHead;
    private List<TrolleySegment> _trolleySegments;
   [SerializeField] private TrolleyGenerator _trolleyGenerator;
    [SerializeField] private float _segmentPrentess;
    [SerializeField] private TrolleySegment _trolleySegment;

    private bool _isFall = false;
    private bool _isFall2 = false;

    private void Awake()
    {
        _trolleySegments = new List<TrolleySegment>();
        //_trolleySegments = _trolleyGenerator.Generate();
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
            Move(_trolleyHead.transform.position);
        }
        else
        {
            MoveStop();
        }
        //if(_isMan == true)
        //{

        //}

    }

    private void AddMen()
    {


        //_trolleyGenerator._trolleyTrainSize = _trolleyGenerator._trolleyTrainSize+1;
        _trolleySegments.Add(Instantiate(_trolleySegment, transform));
       
        _trolleySegments[_trolleySegments.Count - 1].PlayParticle();


    }

    private void Detection()
    {
        if(_isFall == false)
        {
            foreach (var segment in _trolleySegments)
            {
                _isFall = segment.IsStop;
            }
        }
        
    }

    private void Move(Vector3 nextPosition)
    {
        Vector3 previosPosition = _trolleyHead.transform.position;
        Quaternion previosRotation = _trolleyHead.transform.rotation;
        foreach (var segment in _trolleySegments)
        {
            Vector3 tempPosistion = segment.transform.position;
            Quaternion tempRotation = segment.transform.rotation;
            segment.transform.position = Vector3.Lerp(segment.transform.position, previosPosition, _segmentPrentess*Time.fixedDeltaTime);
            segment.InitDirection(previosPosition);
            //segment.transform.rotation = Quaternion./*RotateTowards*/Lerp(segment.transform.rotation, previosRotation, 20 * Time.fixedDeltaTime);
            //transform.LookAt(previosPosition*20*Time.fixedDeltaTime);
            previosPosition = tempPosistion;
            previosRotation = tempRotation;
        }
    }

    private void MoveStop()
    {
        if(_isFall2 == false)
        {
            foreach (var segment in _trolleySegments)
            {
                segment.TryGetComponent<Rigidbody>(out Rigidbody rigidbody);
                segment.Obtacle();
                rigidbody.freezeRotation = false;
                rigidbody.AddForce(Vector3.up * 500);
                rigidbody.AddTorque(0f, 800f, 0f);
                rigidbody.mass = 100;
                Debug.Log("fff1");
            }
            _isFall2 = true;
        }

       
    }

    private void Destruction()
    {
        _isFall = true;
       
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.TryGetComponent<WhiteMan>(out WhiteMan addMen))
        {
            AddMen();
            Destroy(addMen.gameObject);
            foreach (var segment in _trolleySegments)
            {
                segment.TryGetComponent<Rigidbody>(out Rigidbody rigidbody);

                rigidbody.AddForce(Vector3.up * 1000);
                StartCoroutine(Jump(rigidbody));
               
                Debug.Log("33333");
            }

        }


    }

    private IEnumerator Jump(Rigidbody rigidbody)
    {
        WaitForSeconds wait = new WaitForSeconds(0.1f);
        yield return wait;
        rigidbody.AddForce(-Vector3.up * 1500);

    }


}
