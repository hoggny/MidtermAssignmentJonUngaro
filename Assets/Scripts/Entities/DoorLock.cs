using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorLock : MonoBehaviour
{

    [SerializeField] private float dissolveTime = 2.0f;

    public void DissolveLock()
    {
        StartCoroutine(Dissolve());
    }
    private IEnumerator Dissolve()
    {
        Renderer renderer = GetComponent<Renderer>();
        if (renderer != null)
        {
            float elapsed = 0;
            float currentDissolve = renderer.material.GetFloat("_Dissolve");

            while (elapsed < dissolveTime)
            {
                elapsed += Time.deltaTime;
                float newValue = Mathf.Lerp(currentDissolve, 1, elapsed / dissolveTime);
                renderer.material.SetFloat("_Dissolve", newValue);
                yield return null;
            }
        }

        Destroy(gameObject);
    }
}
