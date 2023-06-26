using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class LastSceneMinotaur : MonoBehaviour
{
    [SerializeField] private GameObject _minotaur;
    [SerializeField] private GameObject _endPointMove;
    [SerializeField] private Animator _animator;

    private void OnEnable()
    {
        _animator.SetBool("IsRun", true);
        _minotaur.transform.DOMove(_endPointMove.transform.position, 3f);
    }

    private void Update()
    {
        if (_minotaur.transform.position != _endPointMove.transform.position)
            return;
        else
            _animator.SetBool("IsRun", false);
    }
}
