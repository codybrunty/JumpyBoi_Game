using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ServiceLocator {
    private static readonly Dictionary<System.Type, object> services = new();

    public static void RegisterService<T>(T service) {
        services[typeof(T)] = service;
    }

    public static T GetService<T>() {
        if (services.TryGetValue(typeof(T), out var service)) {
            return (T)service;
        }
        throw new System.Exception($"Service of type {typeof(T)} not found!");
    }
}
