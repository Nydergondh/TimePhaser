using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossSlider : MonoBehaviour
{
    private Slider _slider;
    private Text _textComponent;

    private int _value;
    // Start is called before the first frame update
    void Start()
    {
        _slider = GetComponent<Slider>();

        _slider.maxValue = Boss.boss.health;
        _slider.value = _slider.maxValue;

        Boss.boss.attUI += UpdateUI;
    }

    public void UpdateUI(int value) {

        if (value > 0) {
            _slider.value = value;
        }
        else {
            _slider.value = 0;
        }
        
    }

}
