using NUnit.Framework;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PetManager : MonoBehaviour
{
	// this is where the pet would be managed with games and activities but I don't have time for that
	// this will just have button functionality and switching screens around

	private bool isClockMenuOpen = false;
	private int selection = 0;

	[SerializeField] private Transform clockMenu;
	[SerializeField] private TextMeshProUGUI clockText;
	[SerializeField] private Transform petMenu;
	[SerializeField] private Image actionIcon;
	[SerializeField] private Sprite[] actionOptions;

	private void Start()
	{
		HideClockMenu();
		UpdateSelection();
	}

	private void Update()
	{
		DateTime now = DateTime.Now;
		clockText.text = "" + now.Hour.ToString("00") + ((now.Millisecond < 500) ? " " : ":") + now.Minute.ToString("00");
	}

	public void UpdateSelection()
	{
		actionIcon.sprite = actionOptions[selection];
	}

	public void ClickButtonA()
	{
		selection++;
		if (selection > actionOptions.Length - 1)
		{
			selection = 0;
		}
		UpdateSelection();
	}

	public void ClickButtonB()
	{
		selection = 0;
		UpdateSelection();
	}

	public void ClickButtonC()
	{
		if (isClockMenuOpen)
		{
			HideClockMenu();
		} else
		{
			ShowClockMenu();
		}
	}

	public void HideClockMenu()
	{
		clockMenu.gameObject.SetActive(false);
		petMenu.gameObject.SetActive(true);
		isClockMenuOpen = false;
	}

	public void ShowClockMenu()
	{
		clockMenu.gameObject.SetActive(true);
		petMenu.gameObject.SetActive(false);
		isClockMenuOpen = true;
	}
}
