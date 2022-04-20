using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class ToggleController : MonoBehaviour 
{
	public  bool isOn;

	public Color onColorBorder;
	public Color offColorBorder;

	public Image toggleBorderImage;
	public RectTransform toggle;

	public GameObject handle;
	private RectTransform handleTransform;

	private float handleSize;
	private float onPosX;
	private float offPosX;

	public float handleOffset;

	public float speed;
	
	private bool switching = false;
	private Image handleImage;
	private Action<bool> toggleAction = (status) => { Debug.Log(status); };


	void Awake()
	{
		handleTransform = handle.GetComponent<RectTransform>();
		RectTransform handleRect = handle.GetComponent<RectTransform>();
		handleImage = handle.GetComponent<Image>();
		handleSize = handleRect.sizeDelta.x;
		float toggleSizeX = toggle.sizeDelta.x;
		onPosX = (toggleSizeX / 2) - (handleSize/2) - handleOffset;
		offPosX = onPosX * -1;

	}

	public void SetAction(Action<bool> action) {
		toggleAction = action;
	}

	public void AddAction(Action<bool> action) {
		toggleAction += action;
	}


	void Start()
	{
		if(isOn)
		{
			toggleBorderImage.color = onColorBorder;
			handleImage.color = onColorBorder;
			handleTransform.localPosition = new Vector3(onPosX, 0f, 0f);
		}
		else
		{
			toggleBorderImage.color = offColorBorder;
			handleImage.color = offColorBorder;
			handleTransform.localPosition = new Vector3(offPosX, 0f, 0f);
		}
	}

	public void Switching()
	{
		Toggle(isOn);
	}

	public void Toggle(bool toggleStatus)
	{	
		if (switching) return;
		switching = true;
		if(toggleStatus)
		{
			StartCoroutine(SmoothColor(onColorBorder, offColorBorder));
			StartCoroutine(SmoothMove(handle, onPosX, offPosX));
		}
		else 
		{
			StartCoroutine(SmoothColor(offColorBorder, onColorBorder));
			StartCoroutine(SmoothMove(handle, offPosX, onPosX));
		}	
	}

	
	IEnumerator SmoothMove(GameObject toggleHandle, float startPosX, float endPosX)
	{
		float t = 0.0f;
		while (t < 1.0f) {
			handleTransform.localPosition = new Vector3 (Mathf.Lerp(startPosX, endPosX, t += speed * Time.deltaTime), 0f, 0f);
			yield return null;
		}
		switching = false;
		toggleAction(isOn = !isOn);
	}

	IEnumerator SmoothColor(Color startCol, Color endCol)
	{
		float t = 0.0f;
		while (t < 1.0f) {
			Color c = Color.Lerp(startCol, endCol, t += speed * Time.deltaTime);
			handleImage.color = c;
			toggleBorderImage.color = c;
			yield return null;
		}
	}
}
