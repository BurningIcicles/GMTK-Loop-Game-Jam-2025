using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderController : MonoBehaviour
{
    // Start is called before the first frame update
    private Slider _slider;
    void Start()
    {
        _slider = GetComponent<Slider>();
        _slider.value = PlayerPrefs.GetFloat("Volume");
        _slider.onValueChanged.AddListener(delegate { UpdateVolume();});
    }

    private void UpdateVolume()
    {
        PlayerPrefs.SetFloat("Volume", _slider.value);
    }
}
