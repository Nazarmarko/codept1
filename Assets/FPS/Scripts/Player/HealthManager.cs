using Utilites;
using UnityEngine;
using UnityEngine.UI;
public class HealthManager : Singleton<HealthManager>
{
     Slider healthSlider;


    public Gradient gradient;
    public Image fill;

   new void Awake()
    {
        healthSlider = GetComponent<Slider>();
    }
    public void SetMaxValue(int sliderValue)
    {
            healthSlider.value = sliderValue;
            healthSlider.maxValue = sliderValue;

            fill.color = gradient.Evaluate(1f);
    }

    public void SetValue(int sliderVal)
    {
        healthSlider = GetComponent<Slider>();
        healthSlider.value = sliderVal;

        fill.color = gradient.Evaluate(healthSlider.normalizedValue);
    }

}
