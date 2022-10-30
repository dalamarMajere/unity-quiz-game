using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    [RequireComponent(typeof(Slider))]
    public class SliderController : MonoBehaviour
    {
        private Slider _slider;

        private void Awake()
        {
            _slider = GetComponent<Slider>();
        }

        public void SetMaxValue(int maxValue)
        {
            _slider.maxValue = maxValue;
        }

        public void UpdateSlider(int currentValue)
        {
            _slider.value = currentValue;
        }
    }
}