using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class HealthSlider : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private Slider _healthSlider;

    [SerializeField] private float _changeStep = 0.05f;

    private float _lastValue = 0.0f;
    private float _targetValue;

    private Coroutine _fillCoroutine;

    private void OnEnable()
    {        
        _targetValue = _player.MaxHealth;
        _healthSlider.maxValue = _targetValue;

        _player.HealthChanged += OnHealthChanged;
    }

    private void OnDisable()
    {
        _player.HealthChanged -= OnHealthChanged;
    }

    private void OnHealthChanged(int health)
    {
        Fill(health);
    }

    public void Fill(int targetValue)
    {
        if(_fillCoroutine != null)
        {
            StopCoroutine(_fillCoroutine);
        }

        _targetValue = targetValue;

        _fillCoroutine = StartCoroutine(FillCoroutine());
    }

    private IEnumerator FillCoroutine()
    {
        float _changeValue = 0.0f;

        _lastValue = _healthSlider.value;        

        while (_healthSlider.value != _targetValue)
        {
            _healthSlider.value = Mathf.Lerp(_lastValue, _targetValue, _changeValue);

            _changeValue += _changeStep * Time.deltaTime;

            yield return null;
        }        
    }
}
