using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Slider))]
public class HealthBarView : MonoBehaviour
{
    private HealthBar _healthBar;
    private Slider _slider;

    private void Awake()
    {
        _slider = GetComponent<Slider>();
        _healthBar = GetComponentInParent<HealthBar>();
    }

    private void OnEnable()
    {
        _healthBar.HealthChanged += SetValue;
    }

    private void OnDisable()
    {
        _healthBar.HealthChanged -= SetValue;
    }

    private void SetValue(float value)
    {
        _slider.value = Mathf.Clamp01(value);
    }
}