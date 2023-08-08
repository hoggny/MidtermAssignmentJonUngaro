using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Builder : MonoBehaviour
{
    [Header("Loot Box Properties")]
    [SerializeField] private GameObject _lootPrefab;
    [SerializeField] private Transform _placementPoint;

    public void Build()
    {
        Instantiate(_lootPrefab, _placementPoint.position, _placementPoint.rotation);
        Destroy(gameObject);
    }
}
