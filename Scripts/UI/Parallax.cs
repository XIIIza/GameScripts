using UnityEngine;

public class Parallax : MonoBehaviour
{


    [SerializeField] private Transform _followingTarget;
    [SerializeField , Range(0f, 1f)] float _parallaxStrength = 0.1f;
    [SerializeField] bool _disableVerticalParallax;
    private Vector3 _targetPreviousPosition;

    void Start()
    {
        if(!_followingTarget)
            _followingTarget = Camera.main.transform;

        _targetPreviousPosition = _followingTarget.position;
    }

    void Update()
    {
        Vector3 delta = _followingTarget.position - _targetPreviousPosition;

        if (_disableVerticalParallax)
            delta.y = 0;

        _targetPreviousPosition = _followingTarget.position;
        transform.position += delta * _parallaxStrength;
    }
}