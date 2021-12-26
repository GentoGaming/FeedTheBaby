using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class UISound : MonoBehaviour
{
	[SerializeField] private AudioClip _clip;

	private AudioSource _source;
	
	private void Awake()
	{
		_source = GetComponent<AudioSource>();
		_source.loop = false;
		_source.clip = _clip;
	}

	public void ButtonSound()
	{
		_source.Play();
	}
}
