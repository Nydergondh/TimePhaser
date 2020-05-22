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

        PlayerStatus.player.playerCombat.takeDamage += UpdateHealth;

        if (_sliderType == SliderType.Health) {
            _slider.maxValue = PlayerStatus.player.playerCombat.health;
            _slider.value = PlayerStatus.player.playerCombat.health;
            _textComponent.text = PlayerStatus.player.playerCombat.health.ToString();
        }
        else if (_sliderType == SliderType.Energy) {
            _slider.maxValue = PlayerStatus.player.playerCombat.energy;
            _slider.value = PlayerStatus.player.playerCombat.energy;
            _textComponent.text = PlayerStatus.player.playerCombat.energy.ToString();
        }
    }

    public void UpdateHealth(int damage) {
        if (_sliderType == SliderType.Health) {
            int aux;
            int.TryParse(_textComponent.text, out _value);

            if (_value - damage > 0) {
                aux = _value - damage;
                _slider.value = _value  -damage;
            }
            else {
                aux = 0;
            }

            _textComponent.text = aux.ToString();
        }
    }

    public void UpdateEnergy(int energyWasted) {
        if (_sliderType == SliderType.Energy) {

            int aux;
            int.TryParse(_textComponent.text, out _value);

            if (_value - energyWasted > 0) {
                aux = _value - energyWasted;
                _textComponent.text = aux.ToString();
            }
        }
    }

    public enum SliderType {
        Energy,
        Health
    }
}
