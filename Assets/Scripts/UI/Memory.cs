using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MemoryBar : MonoBehaviour
{
	private Image memoryBarFill;

    private void Awake()
	{
		memoryBarFill = transform.Find("antifill").GetComponent<Image>();
	}

    public void updateMemory(float fill)
	{
        memoryBarFill.transform.Find("antifill").GetComponent<Image>().fillAmount = 1 - fill;
	}
}
