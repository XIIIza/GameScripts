using UnityEngine;
using DG.Tweening;

public class FinalSceneScript : Trigger
{
    [SerializeField] private GameObject _milos;
    [SerializeField] private float _milosUpValue;
    [SerializeField] private Enemy _enemy;
    [SerializeField] private Player _player;

    [Header("Bomb settings")]
    [SerializeField] private float _dropHeightBobm;
    [SerializeField] private Bomb _bomb;
    [SerializeField] private float _timeBetweenBombs;
    private bool _canFall = true;

    [SerializeField] private GameObject _enemyHealthBar;
    [SerializeField] private LastSceneMinotaur _lastSceneMinotaur;

    private float _time;

    private void OnEnable()
    {
        DialogueManager.GetInstance().ExitDialogueMode();
        MoveYAxis(_milos, _milosUpValue, 2.5f);
        _enemy.SetReady();
        _enemyHealthBar.SetActive(true);
        _enemy.Dying += BossDie;
    }

    private void Update()
    {
        if (_player == null)
            return;

        _time += Time.deltaTime;

        if (_time >= _timeBetweenBombs && _canFall)
        {
            DropBomb();
            _time = 0;
        }
    }

    private void MoveYAxis(GameObject gameObject, float endValue, float duration)
    {
        gameObject.transform.DOMoveY(endValue, duration);
    }

    private void DropBomb()
    {
        Vector2 bombPostion = new Vector2(_player.transform.position.x, _dropHeightBobm);

        Instantiate(_bomb, bombPostion, Quaternion.identity);
    }

    private void BossDie()
    {
        _canFall = false;
        MoveYAxis(_milos, 0, 2.5f);
        _enemy.Dying -= BossDie;
        _lastSceneMinotaur.enabled = true;
        _enemyHealthBar.SetActive(false);
    }
}