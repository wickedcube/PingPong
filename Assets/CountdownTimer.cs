using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CountdownTimer : MonoBehaviour {

	public Coroutine countdownTimeCoroutine;

	public void StartCountdown(int time_to_fill)
	{
		if (countdownTimeCoroutine != null)
			StopCoroutine(countdownTimeCoroutine);

		gameObject.SetActive(true);
		countdownTimeCoroutine = StartCoroutine(Countdown(time_to_fill));
	}

	IEnumerator Countdown(float time) {
		float total_time = time;
		while (time > 0) {
			time -= Time.deltaTime;
			GetComponent<Image>().fillAmount = time / total_time;
			GetComponentInChildren<Text>().text = time.ToString("0.00");
			yield return null;
		}
		ResetCountdown();
	}

	public void ResetCountdown() {
		GetComponent<Image>().fillAmount = 1;
		//gameObject.SetActive(false);
	}
}
