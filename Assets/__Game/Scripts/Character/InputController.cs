using UnityEngine;
using UnityEngine.InputSystem;

public class InputController : MonoBehaviour
{
	private PlayerController _playerController;
	private PlayerShoot _playerShoot;
	private PlayerInput _input;

	private void Awake(){
		_playerController = GetComponent<PlayerController>();
		_playerShoot = GetComponent<PlayerShoot>();
		_input = new PlayerInput();
		
		_input.PlayerInputs.MovementDirection.performed += ctx => _playerController.inputMovement = ctx.ReadValue<Vector2>();
		_input.PlayerInputs.MovementDirection.canceled += ctx => _playerController.inputMovement = Vector2.zero;

		
		_input.PlayerInputs.Jump.performed += context => _playerController.isJumpingTriggered = true; 
		_input.PlayerInputs.Jump.canceled += context => _playerController.isJumpingTriggered = false; 
		
		_input.PlayerInputs.Shoot.performed += ctx => _playerShoot.fire = true;
		_input.PlayerInputs.Shoot.canceled += ctx => _playerShoot.fire = false;
		_input.PlayerInputs.Pause.performed += Pause;

		_input.PlayerInputs.RightStick.performed +=
			ctx => _playerController.controllerLookDirection = ctx.ReadValue<Vector2>();
		_input.PlayerInputs.RightStick.canceled +=
			ctx => _playerController.controllerLookDirection = Vector2.zero;

		_input.PlayerInputs.UsingController.performed += ctx => _playerController.usingController = true;
		_input.PlayerInputs.UsingKeyboard.performed += ctx => _playerController.usingController = false;
	}

	private void Shoot(InputAction.CallbackContext ctx)
	{
		_playerShoot.fire = true;
	}

	private void Pause(InputAction.CallbackContext ctx){
		PauseEventInfo pei = new PauseEventInfo();
		EventManager.SendNewEvent(pei);
	}
	
	private void OnEnable(){
		_input.PlayerInputs.MovementDirection.Enable();
		_input.PlayerInputs.Jump.Enable();
		_input.PlayerInputs.Shoot.Enable();
		_input.PlayerInputs.Pause.Enable();
		_input.PlayerInputs.RightStick.Enable();
		_input.PlayerInputs.UsingKeyboard.Enable();
		_input.PlayerInputs.UsingController.Enable();
	}

	private void OnDisable(){
		_input.PlayerInputs.MovementDirection.Disable();
		_input.PlayerInputs.Jump.Disable();
		_input.PlayerInputs.Shoot.Disable();
		_input.PlayerInputs.Pause.Disable();
		_input.PlayerInputs.RightStick.Disable();
		_input.PlayerInputs.UsingKeyboard.Disable();
		_input.PlayerInputs.UsingController.Disable();
	}
}
