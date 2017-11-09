using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AdjustSliderVolume : MonoBehaviour {

	public Slider mainSlider;
	public Slider[] childSliders; 

	void Start () {
		mainSlider.onValueChanged.AddListener(delegate { UpdateChildSliders(); });
	}

	private void UpdateChildSliders()
	{
		foreach (var child in childSliders)
		{
			child.value = mainSlider.value;
		}
	}
}
