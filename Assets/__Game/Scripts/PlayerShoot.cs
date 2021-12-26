using System.Collections;
using UnityEngine;

public class PlayerShoot : MonoBehaviour{
	[Header("Fire")] 
	public bool fire;
	
	[Header("Audio")] 
	[SerializeField] private float shotVolume = 1f;
	
	[Header("Projectile")]
	[SerializeField] private int _projectileID = default;
	[Range(0, 5f)][SerializeField] private float _cooldownTime = 1f;

	[Header("MultiFire")] 
	public bool multiShot = false;
	[Range(1, 7)][SerializeField] private int shotCount = 3;
	[Range(1f, 30f)][SerializeField] private float spreadAngle = 10;

	[Header("BubbleExplosion")] 
	[SerializeField] private bool _bubbleExplosion = false;
	[Range(1f, 90f)][SerializeField] private int angleBetweenShots = 5;
	
	[Header("BigBubble")] 
	[SerializeField] private bool _bigBubble = false;
	
	[Header("RapidFire")]
	[SerializeField] private bool _rapidFire = false;
	
	[Header("Offset")] 
	[Range(1f, 3f)][SerializeField] private float projectileOffset = 1.2f;
	
	private bool onCooldown = false;
	private Transform _childTransform = default;
	private Coroutine _multishotTimer = default;
	private Coroutine _rapidTimer = default;
	private Animator _animator = default;
	private static readonly int ShootTrigger = Animator.StringToHash("Shoot");
	private PlayerController _playerController = default;
	private Vector3 shotPos = Vector3.zero;
	private Quaternion shotRot = Quaternion.identity;
	private Transform _transform;
	
	private void Awake()
	{
		_childTransform = transform.GetChild(0).transform;
		_animator = GameManagement.GetPlayer().GetComponentInChildren<Animator>();     
		EventManager.RegisterListener<ActivatePowerUpEventInfo>(ActivatePowerUp);
		_playerController = GameManagement.GetPlayer().GetComponent<PlayerController>();
		_transform = transform;
	}

	private void OnDisable()
	{
		EventManager.UnregisterListener<ActivatePowerUpEventInfo>(ActivatePowerUp);
	}

	private void ActivatePowerUp(EventInfo apuei)
	{
		var eventInfo = (ActivatePowerUpEventInfo)apuei;

		switch ((int)eventInfo.PowerType)
		{
			// BigBubble
			case 0:
				_bigBubble = true;
				break;
			
			// RapidFire
			case 1:
				if(_rapidTimer != null)
                {
					StopCoroutine(_rapidTimer);
                }
				_rapidTimer = StartCoroutine(RapidFireTimer(eventInfo.Duration, eventInfo.SpeedPercentage));
				break;
			
			// MultiShot
			case 2:
				if (_multishotTimer != null)
				{
					StopCoroutine(_multishotTimer);
				}
				_multishotTimer = StartCoroutine(MultiShotTimer(eventInfo.Duration));
				break;
			
			// BubbleExplosion
			case 4:
				_bubbleExplosion = true;
				break;
		}
	}

	private void Update(){
		if(fire && !onCooldown) Shoot();
	}

	private void Shoot()
	{
		_animator.SetTrigger(ShootTrigger);

		SoundManager.SoundManagerInstance.PlaySound(SoundManager.BubbleSound, shotVolume, _transform.position);
		
		if (!_playerController.bounced)
		{
			shotPos = _childTransform.position + (_childTransform.forward * projectileOffset);
			shotRot = _childTransform.rotation;
		}
		else
		{
			shotPos = _childTransform.position + (_childTransform.forward * projectileOffset);
			shotRot = _childTransform.rotation * Quaternion.Euler(90,0,0);
		}
		
		if (_bigBubble)
		{
			ObjectPool.Instance.GetObjectFromPool(2, shotPos,
				_childTransform.rotation);
			_bigBubble = false;
			UsedPowerupEventInfo Upei = new UsedPowerupEventInfo(PowerupType.BigBubble);
			EventManager.SendNewEvent(Upei);
			StartCoroutine(Cooldown());
			return;
		}
		
		if (multiShot)
		{
			MultiShoot();
			return;
		}

		if (_bubbleExplosion)
		{
			BubbleExplosion();
			UsedPowerupEventInfo Upei = new UsedPowerupEventInfo(PowerupType.Explosion);
			EventManager.SendNewEvent(Upei);
			return;
		}
		
		ObjectPool.Instance.GetObjectFromPool(_projectileID, shotPos,
			shotRot);
		StartCoroutine(Cooldown());
	}

	private void MultiShoot()
	{
		ObjectPool.Instance.GetObjectFromPool(_projectileID, shotPos,
			_childTransform.rotation);

		if (shotCount > 1){
			int count = 0;
			int i = 1;
			while (shotCount - 1 > count){
				i *= -1;

				ObjectPool.Instance.GetObjectFromPool(_projectileID,
					_childTransform.position + (_childTransform.forward * projectileOffset),
					_childTransform.rotation * Quaternion.Euler(0, 1 * spreadAngle * i, 0));

				count++;
				if(count % 2 != 0) continue;
				if (i < 0) i += -1;
				else i += 1;
			}
		}
		StartCoroutine(Cooldown());
	}

	private void BubbleExplosion()
	{
		_bubbleExplosion = false;
		int count = 0;
		int i = 1;
		while (360 >= count){
			
			ObjectPool.Instance.GetObjectFromPool(_projectileID,
				_childTransform.position + (_childTransform.forward * projectileOffset),
				_childTransform.rotation * Quaternion.Euler(0, angleBetweenShots + count, 0));

			count += angleBetweenShots;
		}
	}
	
	private IEnumerator MultiShotTimer(float time)
	{
		multiShot = true;
		yield return new WaitForSeconds(time);
		multiShot = false;
	}

	private IEnumerator RapidFireTimer(float time, float speed)
	{
		float temp = _cooldownTime;
		_cooldownTime *= (speed * 0.01f);
		yield return new WaitForSeconds(time);
		_cooldownTime = temp;
	}
	
	private IEnumerator Cooldown()
	{
		onCooldown = true;
		yield return new WaitForSeconds(_cooldownTime);
		onCooldown = false;
	}
}