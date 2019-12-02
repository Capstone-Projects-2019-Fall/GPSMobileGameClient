using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Healthbar : MonoBehaviour
{
    private Image healthBarFill;
    
    private void Awake()
    {
        healthBarFill = transform.Find("fill").GetComponent<Image>();
    }

    public void updateHealthbar(float fill)
    {
        healthBarFill.fillAmount = fill;
    }
}
