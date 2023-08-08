using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Fire : MonoBehaviour
{
    private float lastDamageTime;
    private float damageCooldown = 0.1f;
    [SerializeField] private float damage = 0.05f;

    public AudioSource fireAudioSource;
    public AudioClip fireSoundEffect;
    //private bool isFlashing = false;

    [SerializeField] private GameObject loot;

    private IAudioManager audioManager;


    // Start is called before the first frame update
    void Start()
    {

        audioManager = ServiceLocator.GetService<IAudioManager>();
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void OnTriggerEnter(Collider other)
    {
        // Play the fire sound effect
        fireAudioSource.clip = fireSoundEffect;
        fireAudioSource.Play();
    }

    private void OnTriggerStay(Collider other)
    {
        Debug.Log("Direct hit 1");
        Health playerHealth = other.transform.GetComponent<Health>();
        if (playerHealth != null)
        {
            Debug.Log("Direct hit 2");
            if (Time.time >= lastDamageTime + damageCooldown)
            {

                playerHealth.DeductHealth(damage);
                lastDamageTime = Time.time;
            }
        }
    }


}
