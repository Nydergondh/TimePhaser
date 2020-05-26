using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class UISliderController : MonoBehaviour
{
    private Slider _slider;
    private Text _textComponent;

    private int _value;

    [SerializeField]
    private SliderType _sliderType;

    // Start is called before the first frame update
    void Start()
    {
        _slider = GetComponent<Slider>();
        _textComponent = GetComponentInChildren<Text>();

        int.TryParse(_textComponent.text,out _value);

        PlayerStatus.player.attUI += UpdateUI;


        if (_sliderType == SliderType.Health) {
            _slider.maxValue = PlayerStatus.player.health;
            _slider.value = PlayerStatus.player.health;
            _textComponent.text = PlayerStatus.player.health.ToString();
        }
        else if (_sliderType == SliderType.Energy) {
            _slider.maxValue = PlayerStatus.player.energy;
            _slider.value = PlayerStatus.player.energy;
            _textComponent.text = PlayerStatus.player.energy.ToString();
        }
    }

    public void UpdateUI(int value, SliderType type) {
        if (_sliderType == type) {

            if (value > 0) {
                _textComponent.text = value.ToString();
                _slider.value = value;
            }
            else {
                _textComponent.text = 0.ToString();
                _slider.value = 0;
            }

            _textComponent.text = value.ToString();
        }
    }

    public enum SliderType {
        Energy,
        Health
    }
}
