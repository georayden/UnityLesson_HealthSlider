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

    private Coroutine _changeFloatCoroutine;

    private void OnEnable()
    {        
        _targetValue = _player.Health;
        _healthSlider.maxValue = _targetValue;

        _player.HealthChanged += OnHealthChanged;
    }

    private void OnDisable()
    {
        _player.HealthChanged -= OnHealthChanged;
    }

    private void OnHealthChanged(int health)
    {
        ChangeFloat(health);
    }

    public void ChangeFloat(int targetValue)
    {
        if(_changeFloatCoroutine != null)
        {
            StopCoroutine(_changeFloatCoroutine);
        }

        _targetValue = targetValue;

        _changeFloatCoroutine = StartCoroutine(ChangeFloatCoroutine());
    }

    private IEnumerator ChangeFloatCoroutine()
    {
        float _changeValue = 0.0f;
        var waitForFixedUpdate = new WaitForFixedUpdate();

        _lastValue = _healthSlider.value;        

        while (_healthSlider.value != _targetValue)
        {
            _healthSlider.value = Mathf.Lerp(_lastValue, _targetValue, _changeValue);

            _changeValue += _changeStep * Time.deltaTime;

            yield return waitForFixedUpdate;
        }        
    }
}
