using System;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UiManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI HpText;
    [SerializeField] private Image hpBarImage;

    private Player player;
    void Awake()
    {
        player = GameObject.FindAnyObjectByType<Player>();
        EventManager.OnPlayerHPChanged += OnPlayerHPChanged;
    }

    void OnPlayerHPChanged()
    {
        HpText.text = $"{player.GetHealth()[0]}/{player.GetHealth()[1]}";
    }
}
