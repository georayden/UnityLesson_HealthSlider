using UnityEngine;
using UnityEngine.Events;

public class Player : MonoBehaviour
{
    [SerializeField] private int _maxHealth;

    private int _health;

    public event UnityAction<int> HealthChanged;

    public int MaxHealth => _maxHealth;

    private void Start()
    {
        _health = _maxHealth;

        HealthChanged?.Invoke(_health);
    }

    private void OnEnable()
    {
        HealthChanged += Die;
    }

    private void OnDisable()
    {
        HealthChanged -= Die;
    }

    public void TakeDamage(int damage)
    {  
        if(_health <= 0)
        {
            return;
        }

        _health -= damage;

        HealthChanged?.Invoke(_health);
    }

    public void Heal(int heal)
    {
        if (_health >= _maxHealth)
        {
            return;
        }

        _health += heal;

        HealthChanged?.Invoke(_health);
    }

    private void Die(int health)
    {
        if(health <= 0)
        {
            gameObject.SetActive(false);
        }
    }
}
