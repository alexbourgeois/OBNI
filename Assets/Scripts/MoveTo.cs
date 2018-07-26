using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveTo : MonoBehaviour {

    public Transform Target;

    public float Duration;
    public AnimationCurve curve;

    private Vector3 _startPosition;
    private void Start()
    {
        _startPosition = transform.position;
    }
    // Update is called once per frame
    void Update () {
        transform.position = Vector3.Lerp(_startPosition, Target.position, curve.Evaluate(Time.time / Duration));
	}
}
