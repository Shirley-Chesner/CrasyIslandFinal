using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FloatingHealthBar : MonoBehaviour
{
    [SerializeField]
    private Slider slider;

    [SerializeField]
    private Image fill;

    public Gradient gradient;

    public void UpdateHealthBar(float currentValue, float maxValue)
    {
        slider.value = currentValue / maxValue;
        fill.color = gradient.Evaluate(slider.normalizedValue);
    }

    //private void Update()
    //{
    //    fill.color = gradient.Evaluate(1f);

    //}
}
