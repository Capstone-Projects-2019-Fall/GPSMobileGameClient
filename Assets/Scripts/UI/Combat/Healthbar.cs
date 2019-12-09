using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Healthbar : MonoBehaviour
{
    private Image healthBarFill;
    private float _currentFill;

    public float CurrentFill {
        get => _currentFill;
    }
    
    private void Awake()
    {
        healthBarFill = transform.Find("fill").GetComponent<Image>();
        _currentFill = healthBarFill.fillAmount;
    }

    public void updateHealthbar(float fill)
    {
        healthBarFill.fillAmount = fill;
        _currentFill = fill;
    }
}
