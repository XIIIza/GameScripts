using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class RestartPanel : MonoBehaviour
{
    [SerializeField] private RectTransform _restartPanel;
    [SerializeField] private Player _player;

    private void Start()
    {
        _player.Dying += MovePanelDown;
    }

    private void OnDisable()
    {
        _player.Dying -= MovePanelDown;
    }

    private void MovePanelDown()
    {
        _restartPanel.DOLocalMoveY(-40f, 2);
    }
}
