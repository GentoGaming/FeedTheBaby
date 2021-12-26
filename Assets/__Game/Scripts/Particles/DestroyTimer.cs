using System.Collections;
using UnityEngine;

public class DestroyTimer : MonoBehaviour
{
	[SerializeField] private float _timer;

	private void Awake(){
		StartCoroutine(Destroytimer());
	}

	private IEnumerator Destroytimer(){
		yield return new WaitForSeconds(_timer);
		Destroy(gameObject);
	}
}