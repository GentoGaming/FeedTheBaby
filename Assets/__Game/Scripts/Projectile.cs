using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Projectile : MonoBehaviour{
	[Header("Projectile")]
	[SerializeField][Range(0.1f, 50f)] private float _projectileSpeed = 10f;
	[SerializeField][Range(0, 30f)] private float _lifeTime = 8;
	[SerializeField] private AnimationCurve bubbleRiseCurve = default;

	[Header("Particle")] 
	[SerializeField] private GameObject _fireParticle;
	[SerializeField]private GameObject _particlePop;

	[Header("Audio")] 
	[SerializeField] private float popVolume = 1f;
	
	private bool _targetHit = false;
	private Rigidbody _rigidbody = default;
	private Transform _transform = default;
	private float _time = 0;
	private Coroutine _bubbleRise = default;
	private Coroutine _lifetimer = default;
	private int counter = 0;

	private void Awake()
	{
		_rigidbody = GetComponent<Rigidbody>();
		_transform = GetComponent<Transform>();
	}
	
	private void OnEnable()
	{
		_targetHit = false;
		_rigidbody.velocity = Vector3.zero;
		if(_bubbleRise != null) StopCoroutine(_bubbleRise);
		if(_lifetimer != null) StopCoroutine(_lifetimer);

		if (counter == 0){
			counter++;
			return;
		}

		Instantiate(_fireParticle, transform.position, Quaternion.identity);
		_rigidbody.velocity = Vector3.zero;

		_bubbleRise = StartCoroutine(BubbleRise());
		_lifetimer = StartCoroutine(LifeTimer());

		_rigidbody.AddForce(_transform.forward * _projectileSpeed, ForceMode.Impulse);
	}

	private IEnumerator BubbleRise()
	{
		_time = 0;
		while (bubbleRiseCurve.length < _time){
			
			var position = _transform.position;
			position = new Vector3(position.x, bubbleRiseCurve.Evaluate(_time), position.z);
			_transform.position = position;

			_time += Time.deltaTime;
			
			yield return new WaitForEndOfFrame();
		}
	}
	
	private IEnumerator LifeTimer()
	{
		yield return new WaitForSeconds(_lifeTime);
		if (_targetHit) yield break;
		
		BubblePop();
	}

	private void OnTriggerEnter(Collider other)
	{
		if (other.CompareTag("Environment"))
		{
			BubblePop();
		}
		
		IBubbleTarget target = other.GetComponent<IBubbleTarget>();

		if (target == null)
		{
			return;
		}
		
		target.TargetByBubble();
		gameObject.SetActive(false);
	}

	public void BubblePop()
	{
		Instantiate(_particlePop, transform.position, Quaternion.identity);
		_rigidbody.velocity = Vector3.zero;
		Invoke(nameof(DelayedPop), 0.08f);
	}

	private void DelayedPop()
	{
		SoundManager.SoundManagerInstance.PlaySound(SoundManager.BubblePop, popVolume, _transform.position);
		gameObject.SetActive(false);
	}
}
