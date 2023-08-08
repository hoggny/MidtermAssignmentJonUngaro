using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private IAudioManager audioManager;

    [SerializeField] AudioClip clip;
    // Start is called before the first frame update
    void Start()
    {
        audioManager = ServiceLocator.GetService<IAudioManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            audioManager.PlayAudio(clip);
        }
    }
}
