using UnityEngine;

public class PoweupObject : MonoBehaviour, IInteractPowerUps
{
	[SerializeField] private PowerupStats _stats = null;
	[SerializeField] private MeshRenderer[] _meshes = new MeshRenderer[5];
	private void OnTriggerEnter(Collider other){
		if (other.gameObject.layer == 8){
			ExecuteSpecialPower();
		}
	}

	public void ExecuteSpecialPower(){
		ActivatePowerUpEventInfo apuei = new ActivatePowerUpEventInfo(_stats.PowerupType, _stats.Duration, _stats.SpeedIncrease, gameObject);
		EventManager.SendNewEvent(apuei);
		_meshes[(int)_stats.PowerupType].enabled = false;
	}

	public void ChangePowerUp(PowerupStats newStats)
    {
		_stats = newStats;
		_meshes[(int)_stats.PowerupType].enabled = true;
    }
}
