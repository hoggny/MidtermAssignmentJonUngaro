using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Health : MonoBehaviour
{
    [SerializeField] private float _maxHealth;
    public Action<float> OnHealthUpdated;
    public Action OnDeath;
    [SerializeField] private float _health;
    public bool IsDead { get; private set; }
    // Start is called before the first frame update
    void Start()
    {
        _health = _maxHealth;
        OnHealthUpdated?.Invoke(_health);
    }

    public void DeductHealth(float value)
    {
        if (IsDead) return; //guard clause

        _health -= value;
        if (_health <= 0)
        {
            IsDead = true;
            OnDeath?.Invoke();
            _health = 0;
        }
        OnHealthUpdated?.Invoke(_health);
    }
}
