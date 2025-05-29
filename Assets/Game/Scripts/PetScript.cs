using System;
using UnityEngine;
using UnityEngine.UI;

public class PetScript : MonoBehaviour
{
	// things
	[SerializeField] private Image sr;

	// images
	[SerializeField] private Sprite egg;
	[SerializeField] private Sprite baby;
	[SerializeField] private Sprite child;
	[SerializeField] private Sprite teen;
	[SerializeField] private Sprite adult;
	[SerializeField] private Sprite rip;

	[SerializeField] private AudioClip[] sounds;
	[SerializeField] private AudioSource audioSource;

	// stats
	public float lifeStage = 0;
	public float lifeTimer = 3;
	public float timer = 10;

	private void Start()
	{
		sr.sprite = egg;
	}

	private void FixedUpdate()
	{
		timer -= Time.deltaTime;
		//if (timer < 0)
		if (Input.GetKeyDown(KeyCode.Space) || timer < 0)
		{
			lifeStage++;
			timer = lifeTimer;
			switch (lifeStage)
			{
				case 0:
					audioSource.PlayOneShot(sounds[0]);
					sr.sprite = egg;
					break;
				case 1:
					audioSource.PlayOneShot(sounds[1]);
					sr.sprite = baby;
					break;
				case 2:
					audioSource.PlayOneShot(sounds[2]);
					sr.sprite = child;
					break;
				case 3:
					audioSource.PlayOneShot(sounds[3]);
					sr.sprite = teen;
					break;
				case 4:
					audioSource.PlayOneShot(sounds[4]);
					sr.sprite = adult;
					break;
				case 10:
					audioSource.PlayOneShot(sounds[5]);
					sr.sprite = rip;
					lifeStage = -1;
					break;
				default:
					sr.sprite = adult;
					break;
			}
		}
	}
}
