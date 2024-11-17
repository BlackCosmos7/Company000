using UnityEngine;
using System;

public class EventManager : MonoBehaviour
{
    public static event Action OnPlayerDeath;
    public static event Action OnEnemyDeath;
    public static event Action OnPlayerDetected;

    public static void TriggerPlayerDeath()
    {
        OnPlayerDeath?.Invoke();
    }

    public static void TriggerEnemyDeath()
    {
        OnEnemyDeath?.Invoke();
    }

    public static void TriggerPlayerDetected()
    {
        OnPlayerDetected?.Invoke();
    }
}
