using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaminaBar : Bar
{
    [SerializeField] private CombatSystem _player;

    private void OnEnable()
    {
        _player.StaminaChanged += OnValueChanged;
        Slider.value = 1;
    }

    private void OnDisable()
    {
        _player.StaminaChanged -= OnValueChanged;
    }
}
