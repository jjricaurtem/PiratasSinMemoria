//------------------------------------------------------------------------------
// <auto-generated>
//     This code was auto-generated by com.unity.inputsystem:InputActionCodeGenerator
//     version 1.4.4
//     from Assets/Settings/Controls.inputactions
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public partial class @Controls : IInputActionCollection2, IDisposable
{
    public InputActionAsset asset { get; }
    public @Controls()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""Controls"",
    ""maps"": [
        {
            ""name"": ""Default"",
            ""id"": ""f6fe6e4c-e891-44c8-9a94-cce1db9eafdf"",
            ""actions"": [
                {
                    ""name"": ""Move"",
                    ""type"": ""Value"",
                    ""id"": ""06808a02-eb88-43a7-aa9f-aaf7b5006e21"",
                    ""expectedControlType"": ""Stick"",
                    ""processors"": ""StickDeadzone(min=0.7)"",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""DefaultAction"",
                    ""type"": ""Button"",
                    ""id"": ""b0ed70e0-7f96-4df6-807d-5ed680ced4a0"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""ToggleMenu"",
                    ""type"": ""Button"",
                    ""id"": ""a873bb80-f076-431b-a3aa-a75b879c3302"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""CancelAction"",
                    ""type"": ""Button"",
                    ""id"": ""2f7cd397-5271-4d8b-8b32-8b60b6bfa426"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""ConfirmAction"",
                    ""type"": ""Button"",
                    ""id"": ""a46a15bd-d06f-4419-be25-481b4d8b6340"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""756e4fbd-758d-4cd2-af5a-7ae3b832bbf6"",
                    ""path"": ""<Gamepad>/leftStick"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""f4005c1f-801b-43b0-806a-cbf1ec45050a"",
                    ""path"": ""<Gamepad>/buttonSouth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""DefaultAction"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""a183a230-84ff-44fe-a45b-2b1320472d80"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""DefaultAction"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""6811859c-512c-443b-b155-12dd59f17cac"",
                    ""path"": ""<Touchscreen>/Press"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""DefaultAction"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""1b795c3a-10fd-4206-9939-7fbcba7adc5d"",
                    ""path"": ""<Gamepad>/start"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""ToggleMenu"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""b963ca8d-9ac4-40ab-ae17-e72e8f0fe658"",
                    ""path"": ""<Keyboard>/escape"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""ToggleMenu"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""42a5dd25-781b-4822-883e-dfca85d0e08d"",
                    ""path"": ""<Gamepad>/buttonEast"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""CancelAction"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""af1bc19c-11bc-49ef-936e-14011f8bab86"",
                    ""path"": ""<Gamepad>/buttonNorth"",
                    ""interactions"": ""Hold"",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""ConfirmAction"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": [
        {
            ""name"": ""Mouse"",
            ""bindingGroup"": ""Mouse"",
            ""devices"": [
                {
                    ""devicePath"": ""<Mouse>"",
                    ""isOptional"": true,
                    ""isOR"": false
                }
            ]
        },
        {
            ""name"": ""Gamepad"",
            ""bindingGroup"": ""Gamepad"",
            ""devices"": [
                {
                    ""devicePath"": ""<Gamepad>"",
                    ""isOptional"": true,
                    ""isOR"": false
                }
            ]
        },
        {
            ""name"": ""TouchScreen"",
            ""bindingGroup"": ""TouchScreen"",
            ""devices"": [
                {
                    ""devicePath"": ""<Touchscreen>"",
                    ""isOptional"": true,
                    ""isOR"": false
                }
            ]
        }
    ]
}");
        // Default
        m_Default = asset.FindActionMap("Default", throwIfNotFound: true);
        m_Default_Move = m_Default.FindAction("Move", throwIfNotFound: true);
        m_Default_DefaultAction = m_Default.FindAction("DefaultAction", throwIfNotFound: true);
        m_Default_ToggleMenu = m_Default.FindAction("ToggleMenu", throwIfNotFound: true);
        m_Default_CancelAction = m_Default.FindAction("CancelAction", throwIfNotFound: true);
        m_Default_ConfirmAction = m_Default.FindAction("ConfirmAction", throwIfNotFound: true);
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
    public IEnumerable<InputBinding> bindings => asset.bindings;

    public InputAction FindAction(string actionNameOrId, bool throwIfNotFound = false)
    {
        return asset.FindAction(actionNameOrId, throwIfNotFound);
    }
    public int FindBinding(InputBinding bindingMask, out InputAction action)
    {
        return asset.FindBinding(bindingMask, out action);
    }

    // Default
    private readonly InputActionMap m_Default;
    private IDefaultActions m_DefaultActionsCallbackInterface;
    private readonly InputAction m_Default_Move;
    private readonly InputAction m_Default_DefaultAction;
    private readonly InputAction m_Default_ToggleMenu;
    private readonly InputAction m_Default_CancelAction;
    private readonly InputAction m_Default_ConfirmAction;
    public struct DefaultActions
    {
        private @Controls m_Wrapper;
        public DefaultActions(@Controls wrapper) { m_Wrapper = wrapper; }
        public InputAction @Move => m_Wrapper.m_Default_Move;
        public InputAction @DefaultAction => m_Wrapper.m_Default_DefaultAction;
        public InputAction @ToggleMenu => m_Wrapper.m_Default_ToggleMenu;
        public InputAction @CancelAction => m_Wrapper.m_Default_CancelAction;
        public InputAction @ConfirmAction => m_Wrapper.m_Default_ConfirmAction;
        public InputActionMap Get() { return m_Wrapper.m_Default; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(DefaultActions set) { return set.Get(); }
        public void SetCallbacks(IDefaultActions instance)
        {
            if (m_Wrapper.m_DefaultActionsCallbackInterface != null)
            {
                @Move.started -= m_Wrapper.m_DefaultActionsCallbackInterface.OnMove;
                @Move.performed -= m_Wrapper.m_DefaultActionsCallbackInterface.OnMove;
                @Move.canceled -= m_Wrapper.m_DefaultActionsCallbackInterface.OnMove;
                @DefaultAction.started -= m_Wrapper.m_DefaultActionsCallbackInterface.OnDefaultAction;
                @DefaultAction.performed -= m_Wrapper.m_DefaultActionsCallbackInterface.OnDefaultAction;
                @DefaultAction.canceled -= m_Wrapper.m_DefaultActionsCallbackInterface.OnDefaultAction;
                @ToggleMenu.started -= m_Wrapper.m_DefaultActionsCallbackInterface.OnToggleMenu;
                @ToggleMenu.performed -= m_Wrapper.m_DefaultActionsCallbackInterface.OnToggleMenu;
                @ToggleMenu.canceled -= m_Wrapper.m_DefaultActionsCallbackInterface.OnToggleMenu;
                @CancelAction.started -= m_Wrapper.m_DefaultActionsCallbackInterface.OnCancelAction;
                @CancelAction.performed -= m_Wrapper.m_DefaultActionsCallbackInterface.OnCancelAction;
                @CancelAction.canceled -= m_Wrapper.m_DefaultActionsCallbackInterface.OnCancelAction;
                @ConfirmAction.started -= m_Wrapper.m_DefaultActionsCallbackInterface.OnConfirmAction;
                @ConfirmAction.performed -= m_Wrapper.m_DefaultActionsCallbackInterface.OnConfirmAction;
                @ConfirmAction.canceled -= m_Wrapper.m_DefaultActionsCallbackInterface.OnConfirmAction;
            }
            m_Wrapper.m_DefaultActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Move.started += instance.OnMove;
                @Move.performed += instance.OnMove;
                @Move.canceled += instance.OnMove;
                @DefaultAction.started += instance.OnDefaultAction;
                @DefaultAction.performed += instance.OnDefaultAction;
                @DefaultAction.canceled += instance.OnDefaultAction;
                @ToggleMenu.started += instance.OnToggleMenu;
                @ToggleMenu.performed += instance.OnToggleMenu;
                @ToggleMenu.canceled += instance.OnToggleMenu;
                @CancelAction.started += instance.OnCancelAction;
                @CancelAction.performed += instance.OnCancelAction;
                @CancelAction.canceled += instance.OnCancelAction;
                @ConfirmAction.started += instance.OnConfirmAction;
                @ConfirmAction.performed += instance.OnConfirmAction;
                @ConfirmAction.canceled += instance.OnConfirmAction;
            }
        }
    }
    public DefaultActions @Default => new DefaultActions(this);
    private int m_MouseSchemeIndex = -1;
    public InputControlScheme MouseScheme
    {
        get
        {
            if (m_MouseSchemeIndex == -1) m_MouseSchemeIndex = asset.FindControlSchemeIndex("Mouse");
            return asset.controlSchemes[m_MouseSchemeIndex];
        }
    }
    private int m_GamepadSchemeIndex = -1;
    public InputControlScheme GamepadScheme
    {
        get
        {
            if (m_GamepadSchemeIndex == -1) m_GamepadSchemeIndex = asset.FindControlSchemeIndex("Gamepad");
            return asset.controlSchemes[m_GamepadSchemeIndex];
        }
    }
    private int m_TouchScreenSchemeIndex = -1;
    public InputControlScheme TouchScreenScheme
    {
        get
        {
            if (m_TouchScreenSchemeIndex == -1) m_TouchScreenSchemeIndex = asset.FindControlSchemeIndex("TouchScreen");
            return asset.controlSchemes[m_TouchScreenSchemeIndex];
        }
    }
    public interface IDefaultActions
    {
        void OnMove(InputAction.CallbackContext context);
        void OnDefaultAction(InputAction.CallbackContext context);
        void OnToggleMenu(InputAction.CallbackContext context);
        void OnCancelAction(InputAction.CallbackContext context);
        void OnConfirmAction(InputAction.CallbackContext context);
    }
}
