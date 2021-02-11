using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBar : MonoBehaviour
{
    [SerializeField]
    private RectTransform _bar = null;

    public void UpdateBar(int health, int maxHealth)
    {
        float normalizedHealth = (float)health / maxHealth;
        _bar.localScale = new Vector3(normalizedHealth, 1, 1);
    }
}
