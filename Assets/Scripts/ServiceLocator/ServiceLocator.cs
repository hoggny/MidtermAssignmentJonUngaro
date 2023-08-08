using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ServiceLocator
{
    private static Dictionary<Type, object> services = new Dictionary<Type, object>();

    public static void RegisterService<T>(T service)
    {
        services[typeof(T)] = service;
    }

    public static T GetService<T>()
    {
        if (services.ContainsKey(typeof(T)))
        {
            return (T)services[typeof(T)];
        }

        return default(T);
    }
}
