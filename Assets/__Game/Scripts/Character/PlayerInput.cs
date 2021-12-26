// GENERATED AUTOMATICALLY FROM 'Assets/__Game/Scripts/Character/PlayerInput.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class @PlayerInput : IInputActionCollection, IDisposable
{
    public InputActionAsset asset { get; }
    public @PlayerInput()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""PlayerInput"",
    ""maps"": [
        {
            ""name"": ""PlayerInputs"",
            ""id"": ""db9a669d-905b-4e96-a83e-d480222422aa"",
            ""actions"": [
                {
                    ""name"": ""MovementDirection"",
                    ""type"": ""Value"",
                    ""id"": ""b3cd7b43-12eb-4caf-b0a4-d491e23304f7"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": ""NormalizeVector2"",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Jump"",
                    ""type"": ""Button"",
                    ""id"": ""b5d6ab39-2e73-4183-9e92-7933de4347e7"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": ""NormalizeVector2"",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Look"",
                    ""type"": ""PassThrough"",
                    ""id"": ""b368f924-6dac-48ec-b413-32dd20ece24d"",
                    ""expectedControlType"": """",
                    ""processors"": ""NormalizeVector2"",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Shoot"",
                    ""type"": ""Button"",
                    ""id"": ""4a4665eb-e020-4a8e-b8bf-1679f4c8e637"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": ""NormalizeVector2"",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Pause"",
                    ""type"": ""Button"",
                    ""id"": ""8551ca2a-8b70-405e-9427-b64b6d105e86"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": ""NormalizeVector2"",
                    ""interactions"": """"
                },
                {
                    ""name"": ""UsingController"",
                    ""type"": ""Value"",
                    ""id"": ""07959512-6476-4b15-a9ad-3852713c549c"",
                    ""expectedControlType"": """",
                    ""processors"": ""NormalizeVector2"",
                    ""interactions"": """"
                },
                {
                    ""name"": ""UsingKeyboard"",
                    ""type"": ""Button"",
                    ""id"": ""1e7dbdfe-9ab5-4d6c-b073-f0d6b52e22a6"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": ""NormalizeVector2"",
                    ""interactions"": """"
                },
                {
                    ""name"": ""RightStick"",
                    ""type"": ""Value"",
                    ""id"": ""b4ae219a-77b1-470b-92e8-155236271542"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": ""NormalizeVector2"",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": ""2D Vector"",
                    ""id"": ""447296ef-04fd-413f-b983-36ebd84600f4"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": ""NormalizeVector2"",
                    ""groups"": """",
                    ""action"": ""MovementDirection"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""046a3201-659c-408f-bf3c-a8c04c4263e8"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MovementDirection"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""68d60f41-6415-484d-aae7-6d67eab6a6ab"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MovementDirection"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""ef9e20b7-6283-405d-9d84-78d6e4ae0f57"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MovementDirection"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""69cc6765-b7c6-4474-9507-72340ad995cd"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MovementDirection"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""5199b00b-c7ad-4032-b82b-468d876f8b96"",
                    ""path"": ""<Gamepad>/leftStick"",
                    ""interactions"": """",
                    ""processors"": ""StickDeadzone"",
                    ""groups"": """",
                    ""action"": ""MovementDirection"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""99f2e92e-dba5-4e2a-a5cf-ca3dbc8bce18"",
                    ""path"": ""<Joystick>/stick"",
                    ""interactions"": """",
                    ""processors"": ""StickDeadzone"",
                    ""groups"": """",
                    ""action"": ""MovementDirection"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""3da0abc6-bd42-4d88-aa70-b03010fd40d2"",
                    ""path"": ""<Keyboard>/space"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Jump"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""40100788-fbbd-4f3d-a336-330db84f807b"",
                    ""path"": ""<Gamepad>/buttonSouth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Jump"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""f7aa3c71-4d21-4b90-8120-9f61132a28fc"",
                    ""path"": ""<Mouse>/delta"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Look"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""f19c30b9-3178-4425-8323-3295f858ba5a"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Shoot"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""17940fff-97df-4f18-8be0-383b925c4142"",
                    ""path"": ""<Gamepad>/rightTrigger"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Shoot"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""7088979f-8443-4d83-83ca-757e699e1464"",
                    ""path"": ""<Keyboard>/escape"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Pause"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""ced7072f-fe6b-455b-942f-72ce781447c3"",
                    ""path"": ""<Gamepad>/start"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Pause"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""ea0b1210-9bba-45b2-8678-7a6a55817497"",
                    ""path"": ""<Gamepad>/buttonSouth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""UsingController"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""d230782b-ad7f-4b7b-9596-31279b41e976"",
                    ""path"": ""<Gamepad>/leftStick"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""UsingController"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""f2f1f75d-fac7-40bc-a3ea-e2641ab4b5ab"",
                    ""path"": ""<Gamepad>/rightStick"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""UsingController"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""a12e77b3-d07a-4d34-897f-3309b45e80dc"",
                    ""path"": ""<Gamepad>/dpad"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""UsingController"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""c8efa4ca-1e92-4f0b-b450-fd85ce8dce8d"",
                    ""path"": ""<Keyboard>/anyKey"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""UsingKeyboard"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""1a826468-90e9-4629-9fcd-8fb1e1713593"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""UsingKeyboard"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""8caf67a9-601f-4eec-bf5c-a8a0997a4a65"",
                    ""path"": ""<Mouse>/rightButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""UsingKeyboard"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""1844c4b2-21af-421f-84d0-9f7da4926a42"",
                    ""path"": ""<Gamepad>/rightStick"",
                    ""interactions"": """",
                    ""processors"": ""StickDeadzone"",
                    ""groups"": """",
                    ""action"": ""RightStick"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // PlayerInputs
        m_PlayerInputs = asset.FindActionMap("PlayerInputs", throwIfNotFound: true);
        m_PlayerInputs_MovementDirection = m_PlayerInputs.FindAction("MovementDirection", throwIfNotFound: true);
        m_PlayerInputs_Jump = m_PlayerInputs.FindAction("Jump", throwIfNotFound: true);
        m_PlayerInputs_Look = m_PlayerInputs.FindAction("Look", throwIfNotFound: true);
        m_PlayerInputs_Shoot = m_PlayerInputs.FindAction("Shoot", throwIfNotFound: true);
        m_PlayerInputs_Pause = m_PlayerInputs.FindAction("Pause", throwIfNotFound: true);
        m_PlayerInputs_UsingController = m_PlayerInputs.FindAction("UsingController", throwIfNotFound: true);
        m_PlayerInputs_UsingKeyboard = m_PlayerInputs.FindAction("UsingKeyboard", throwIfNotFound: true);
        m_PlayerInputs_RightStick = m_PlayerInputs.FindAction("RightStick", throwIfNotFound: true);
    }

    public void Dispose()
    {
        UnityEngine.Object.Destroy(asset);
    }

    public InputBinding? bindingMask
    {
        get => asset.bindingMask;
        set => asset.bindingMask = value;
    }

    public ReadOnlyArray<InputDevice>? devices
    {
        get => asset.devices;
        set => asset.devices = value;
    }

    public ReadOnlyArray<InputControlScheme> controlSchemes => asset.controlSchemes;

    public bool Contains(InputAction action)
    {
        return asset.Contains(action);
    }

    public IEnumerator<InputAction> GetEnumerator()
    {
        return asset.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public void Enable()
    {
        asset.Enable();
    }

    public void Disable()
    {
        asset.Disable();
    }

    // PlayerInputs
    private readonly InputActionMap m_PlayerInputs;
    private IPlayerInputsActions m_PlayerInputsActionsCallbackInterface;
    private readonly InputAction m_PlayerInputs_MovementDirection;
    private readonly InputAction m_PlayerInputs_Jump;
    private readonly InputAction m_PlayerInputs_Look;
    private readonly InputAction m_PlayerInputs_Shoot;
    private readonly InputAction m_PlayerInputs_Pause;
    private readonly InputAction m_PlayerInputs_UsingController;
    private readonly InputAction m_PlayerInputs_UsingKeyboard;
    private readonly InputAction m_PlayerInputs_RightStick;
    public struct PlayerInputsActions
    {
        private @PlayerInput m_Wrapper;
        public PlayerInputsActions(@PlayerInput wrapper) { m_Wrapper = wrapper; }
        public InputAction @MovementDirection => m_Wrapper.m_PlayerInputs_MovementDirection;
        public InputAction @Jump => m_Wrapper.m_PlayerInputs_Jump;
        public InputAction @Look => m_Wrapper.m_PlayerInputs_Look;
        public InputAction @Shoot => m_Wrapper.m_PlayerInputs_Shoot;
        public InputAction @Pause => m_Wrapper.m_PlayerInputs_Pause;
        public InputAction @UsingController => m_Wrapper.m_PlayerInputs_UsingController;
        public InputAction @UsingKeyboard => m_Wrapper.m_PlayerInputs_UsingKeyboard;
        public InputAction @RightStick => m_Wrapper.m_PlayerInputs_RightStick;
        public InputActionMap Get() { return m_Wrapper.m_PlayerInputs; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(PlayerInputsActions set) { return set.Get(); }
        public void SetCallbacks(IPlayerInputsActions instance)
        {
            if (m_Wrapper.m_PlayerInputsActionsCallbackInterface != null)
            {
                @MovementDirection.started -= m_Wrapper.m_PlayerInputsActionsCallbackInterface.OnMovementDirection;
                @MovementDirection.performed -= m_Wrapper.m_PlayerInputsActionsCallbackInterface.OnMovementDirection;
                @MovementDirection.canceled -= m_Wrapper.m_PlayerInputsActionsCallbackInterface.OnMovementDirection;
                @Jump.started -= m_Wrapper.m_PlayerInputsActionsCallbackInterface.OnJump;
                @Jump.performed -= m_Wrapper.m_PlayerInputsActionsCallbackInterface.OnJump;
                @Jump.canceled -= m_Wrapper.m_PlayerInputsActionsCallbackInterface.OnJump;
                @Look.started -= m_Wrapper.m_PlayerInputsActionsCallbackInterface.OnLook;
                @Look.performed -= m_Wrapper.m_PlayerInputsActionsCallbackInterface.OnLook;
                @Look.canceled -= m_Wrapper.m_PlayerInputsActionsCallbackInterface.OnLook;
                @Shoot.started -= m_Wrapper.m_PlayerInputsActionsCallbackInterface.OnShoot;
                @Shoot.performed -= m_Wrapper.m_PlayerInputsActionsCallbackInterface.OnShoot;
                @Shoot.canceled -= m_Wrapper.m_PlayerInputsActionsCallbackInterface.OnShoot;
                @Pause.started -= m_Wrapper.m_PlayerInputsActionsCallbackInterface.OnPause;
                @Pause.performed -= m_Wrapper.m_PlayerInputsActionsCallbackInterface.OnPause;
                @Pause.canceled -= m_Wrapper.m_PlayerInputsActionsCallbackInterface.OnPause;
                @UsingController.started -= m_Wrapper.m_PlayerInputsActionsCallbackInterface.OnUsingController;
                @UsingController.performed -= m_Wrapper.m_PlayerInputsActionsCallbackInterface.OnUsingController;
                @UsingController.canceled -= m_Wrapper.m_PlayerInputsActionsCallbackInterface.OnUsingController;
                @UsingKeyboard.started -= m_Wrapper.m_PlayerInputsActionsCallbackInterface.OnUsingKeyboard;
                @UsingKeyboard.performed -= m_Wrapper.m_PlayerInputsActionsCallbackInterface.OnUsingKeyboard;
                @UsingKeyboard.canceled -= m_Wrapper.m_PlayerInputsActionsCallbackInterface.OnUsingKeyboard;
                @RightStick.started -= m_Wrapper.m_PlayerInputsActionsCallbackInterface.OnRightStick;
                @RightStick.performed -= m_Wrapper.m_PlayerInputsActionsCallbackInterface.OnRightStick;
                @RightStick.canceled -= m_Wrapper.m_PlayerInputsActionsCallbackInterface.OnRightStick;
            }
            m_Wrapper.m_PlayerInputsActionsCallbackInterface = instance;
            if (instance != null)
            {
                @MovementDirection.started += instance.OnMovementDirection;
                @MovementDirection.performed += instance.OnMovementDirection;
                @MovementDirection.canceled += instance.OnMovementDirection;
                @Jump.started += instance.OnJump;
                @Jump.performed += instance.OnJump;
                @Jump.canceled += instance.OnJump;
                @Look.started += instance.OnLook;
                @Look.performed += instance.OnLook;
                @Look.canceled += instance.OnLook;
                @Shoot.started += instance.OnShoot;
                @Shoot.performed += instance.OnShoot;
                @Shoot.canceled += instance.OnShoot;
                @Pause.started += instance.OnPause;
                @Pause.performed += instance.OnPause;
                @Pause.canceled += instance.OnPause;
                @UsingController.started += instance.OnUsingController;
                @UsingController.performed += instance.OnUsingController;
                @UsingController.canceled += instance.OnUsingController;
                @UsingKeyboard.started += instance.OnUsingKeyboard;
                @UsingKeyboard.performed += instance.OnUsingKeyboard;
                @UsingKeyboard.canceled += instance.OnUsingKeyboard;
                @RightStick.started += instance.OnRightStick;
                @RightStick.performed += instance.OnRightStick;
                @RightStick.canceled += instance.OnRightStick;
            }
        }
    }
    public PlayerInputsActions @PlayerInputs => new PlayerInputsActions(this);
    public interface IPlayerInputsActions
    {
        void OnMovementDirection(InputAction.CallbackContext context);
        void OnJump(InputAction.CallbackContext context);
        void OnLook(InputAction.CallbackContext context);
        void OnShoot(InputAction.CallbackContext context);
        void OnPause(InputAction.CallbackContext context);
        void OnUsingController(InputAction.CallbackContext context);
        void OnUsingKeyboard(InputAction.CallbackContext context);
        void OnRightStick(InputAction.CallbackContext context);
    }
}
