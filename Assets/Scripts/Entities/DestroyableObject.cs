using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DestroyableObject : MonoBehaviour, IDestroyable
{
    [SerializeField] private UnityEvent _onDestroyed;
    [SerializeField] private GameObject _loot;

    public void OnCollided()
    {
        Destroy(gameObject, 0.1f);
    }

    public static Object InstantiateLoot(Object _loot) => Instantiate(_loot);

}
