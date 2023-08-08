using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ServiceLocatorGameManager : MonoBehaviour
{
    private void Awake()
    {
        var audioManager = new AudioManager();

        ServiceLocator.RegisterService<IAudioManager>(audioManager);
    }
}
