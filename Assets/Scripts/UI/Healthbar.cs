using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Healthbar : MonoBehaviour
{
    [SerializeField] private Slider _slider;

    private IHealth _health;
    private float _startInputValue;

    public void Construct(IHealth health)
    {
        _health = health;
    }

    private void Start()
    {
        _slider.value = _slider.maxValue;
        _startInputValue = _health.GetHealth();
        _health.Damaged += OnChangeSliderValue;
        _health.Died += OnDeactivate;
    }

    private void OnDisable()
    {
        _health.Damaged -= OnChangeSliderValue;
        _health.Died -= OnDeactivate;
    }

    private void OnChangeSliderValue()
    {
        StartCoroutine(ChangeSmoothlySliderValue(1f));
    }

    private IEnumerator ChangeSmoothlySliderValue(float duration)
    {
        float t = 0;
        while (t < duration)
        {
            t += Time.deltaTime;
            _slider.value = Mathf.Lerp(_slider.value, _health.GetHealth() / _startInputValue, t*t);
            yield return null;
        }
    }

    private void OnDeactivate()
    {
        Destroy(gameObject, 1f);
    }
}
