using System;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    public static Action OnPlayerHPChanged;
    public static Action OnPlayerDeath;
    public static Action OnEscPressed;
}
