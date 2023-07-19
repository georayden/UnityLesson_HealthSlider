using UnityEngine;
using UnityEngine.Events;

public class Player : MonoBehaviour
{
    [SerializeField] private int _health;

    public event UnityAction<int> HealthChanged;

    public int Health => _health;

    private void Start()
    {
        HealthChanged?.Invoke(_health);
    }

    private void Update()
    {
        if(_health <= 0)
        {
            Die();
        }
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
        if (_health >= 5)
        {
            return;
        }

        _health += heal;

        HealthChanged?.Invoke(_health);
    }

    private void Die()
    {
        gameObject.SetActive(false);
    }
}
