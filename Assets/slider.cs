using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class slider : MonoBehaviour
{
    [SerializeField]
    private float _playerPos = 0;
    [SerializeField, Header("ステージの長さ")]
    private float _stageLength = 1;
    Slider _slider;
    float _value = 0;

    private void Start()
    {
        _slider = gameObject.GetComponent<Slider>();
    }
    void Update()
    {
        var pValue = _playerPos / _stageLength;
        _slider.value = pValue;
        

    }
}
