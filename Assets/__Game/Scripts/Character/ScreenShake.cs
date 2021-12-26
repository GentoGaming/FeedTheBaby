using System.Collections;
using UnityEngine;

public class ScreenShake : MonoBehaviour
{
	public static ScreenShake Instance;

	[Header("Screen Shake Options")] 
	[SerializeField] private bool _disableScreenShake = false;
	[SerializeField] private float _screenShakeDelay = 0;
	
	private Transform _transform;
	private Coroutine _shake;
	
	private void Awake()
	{
		if (Instance) Destroy(gameObject);
		else Instance = this;

		_transform = transform;
		
		EventManager.RegisterListener<ScreenShakeEventInfo>(ShakeEvent);
	}

	private void OnDisable()
	{
		EventManager.UnregisterListener<ScreenShakeEventInfo>(ShakeEvent);
	}

	private void ShakeEvent(EventInfo ei)
	{
		var ssei = (ScreenShakeEventInfo) ei;
		Shake(ssei.Duration, ssei.Magnitude);
	}
	
	public void Shake(float duration, float magnitude)
	{
		if(_shake != null) StopCoroutine(_shake);
		_shake = StartCoroutine(CameraShake(duration, magnitude));
	}

	private IEnumerator CameraShake(float duration, float magnitude) {
		if (!_disableScreenShake) {
			float elapsedTime = 0f;

			yield return new WaitForSeconds(_screenShakeDelay);
			
			while (elapsedTime < duration) {
				if (!_disableScreenShake){
					float x = Random.Range(-1f, 1f) * magnitude;
					float y = Random.Range(-1f, 1f) * magnitude;

					var position = _transform.position;
					position = new Vector3(position.x + x,
						position.y + y,
						position.z);
					_transform.position = position;

					elapsedTime += Time.deltaTime;
				}
				yield return new WaitForSeconds(0);
			}
		}
		else yield return new WaitForSeconds(0);
	}
}
