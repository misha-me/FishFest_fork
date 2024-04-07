using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterLevelScript : MonoBehaviour
{
    //[SerializeField] private float waterLevelChangeTime;
    //[SerializeField] private float waterLevelChangeSpeed;
    [SerializeField] private float waterLevelDifference;
    private float _currentTime = 0;
    //private Coroutine _coroutine;
    private Vector3 _startTransformPosition;

    private void Awake()
    {
        _startTransformPosition = transform.position;
    }
    //private void Start()
    //{
    //    _coroutine = StartCoroutine(IncreaseWaterLevel());
    //}
    private void Update()
    {
        //if (transform.position == _startTransformPosition + new Vector3(0, waterLevelDifference, 0))
        //{
        //    if (_coroutine != null)
        //        StopCoroutine(_coroutine);
        //    _coroutine = StartCoroutine(DecreaseWaterLevel());
        //}
        //else if (transform.position == _startTransformPosition - new Vector3(0, waterLevelDifference, 0))
        //{
        //    if (_coroutine != null)
        //        StopCoroutine(_coroutine);
        //    _coroutine = StartCoroutine(IncreaseWaterLevel());
        //}
        _currentTime += Time.deltaTime;
        transform.position = _startTransformPosition + new Vector3(0, waterLevelDifference * Mathf.Sin(_currentTime), 0);
    }
    //private IEnumerator IncreaseWaterLevel() 
    //{
    //    Vector3 targetPosition = _startTransformPosition + new Vector3(0, waterLevelDifference, 0);
    //    _currentTime = 0;
    //    while (_currentTime < waterLevelChangeTime) 
    //    {
    //        _currentTime += Time.deltaTime;
    //        transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * waterLevelChangeSpeed * transform.position.y / targetPosition.y);
    //        yield return null;
    //    }
    //    //transform.position = _startTransformPosition + new Vector3(0, waterLevelDifference, 0);
    //    StartCoroutine(DecreaseWaterLevel());
    //}
    //private IEnumerator DecreaseWaterLevel()
    //{
    //    Vector3 targetPosition = _startTransformPosition - new Vector3(0, waterLevelDifference, 0);
    //    _currentTime = 0;
    //    while (_currentTime < waterLevelChangeTime)
    //    {
    //        _currentTime += Time.deltaTime;
    //        transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * waterLevelChangeSpeed * transform.position.y / targetPosition.y);
    //        yield return null;
    //    }
    //    //transform.position = _startTransformPosition - new Vector3(0, waterLevelDifference, 0);
    //    StartCoroutine(IncreaseWaterLevel());
    //}
}
