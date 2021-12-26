using UnityEngine;

[RequireComponent(typeof(ParticleSystem))]
public class ParticleOnEnable : MonoBehaviour{
	private ParticleSystem _particle = null;

	private void Awake()
	{
		_particle = GetComponent<ParticleSystem>();
	}

	private void Start()
	{
		_particle.Play();
	}
}

