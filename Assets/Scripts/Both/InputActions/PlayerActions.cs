//------------------------------------------------------------------------------
// <auto-generated>
//     This code was auto-generated by com.unity.inputsystem:InputActionCodeGenerator
//     version 1.4.4
//     from Assets/Scripts/Both/InputActions/PlayerActions.inputactions
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

namespace Assets.Scripts.Both.InputActions
{
    public partial class @PlayerActions : IInputActionCollection2, IDisposable
    {
        public InputActionAsset asset { get; }
        public @PlayerActions()
        {
            asset = InputActionAsset.FromJson(@"{
    ""name"": ""PlayerActions"",
    ""maps"": [
        {
            ""name"": ""Ground"",
            ""id"": ""0c877833-c138-44cc-a6aa-0e2528e25114"",
            ""actions"": [
                {
                    ""name"": ""Movement"",
                    ""type"": ""Value"",
                    ""id"": ""b2be0e69-3433-4c5d-bd6c-9256e8e7ce52"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""Attack"",
                    ""type"": ""Button"",
                    ""id"": ""c259a436-557b-42c0-9700-dba70a8e3a87"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""SpecialAttack"",
                    ""type"": ""Button"",
                    ""id"": ""9541bada-4953-43ad-8d65-bb7affb2b753"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                }
            ],
            ""bindings"": [
                {
                    ""name"": ""WASD"",
                    ""id"": ""592b7793-8059-4034-a234-d0ad181fa315"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""d203ccef-1122-4cb4-9367-0c43823c4c29"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""47ea1c91-dc79-4d51-921a-15702d5fbb82"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""c03eb18a-0889-4f06-a48a-c55206c43ddd"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""9d057b83-edd0-4f54-b179-631e7ae1792a"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""88bd5045-5f87-416f-b49d-02cff7718bc0"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Attack"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""06bcce97-1e60-4bce-b16f-4e128f8d2a69"",
                    ""path"": ""<Mouse>/rightButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""SpecialAttack"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": [
        {
            ""name"": ""Keyboard"",
            ""bindingGroup"": ""Keyboard"",
            ""devices"": [
                {
                    ""devicePath"": ""<Keyboard>"",
                    ""isOptional"": false,
                    ""isOR"": false
                },
                {
                    ""devicePath"": ""<Mouse>"",
                    ""isOptional"": true,
                    ""isOR"": false
                }
            ]
        }
    ]
}");
            // Ground
            m_Ground = asset.FindActionMap("Ground", throwIfNotFound: true);
            m_Ground_Movement = m_Ground.FindAction("Movement", throwIfNotFound: true);
            m_Ground_Attack = m_Ground.FindAction("Attack", throwIfNotFound: true);
            m_Ground_SpecialAttack = m_Ground.FindAction("SpecialAttack", throwIfNotFound: true);
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

        // Ground
        private readonly InputActionMap m_Ground;
        private IGroundActions m_GroundActionsCallbackInterface;
        private readonly InputAction m_Ground_Movement;
        private readonly InputAction m_Ground_Attack;
        private readonly InputAction m_Ground_SpecialAttack;
        public struct GroundActions
        {
            private @PlayerActions m_Wrapper;
            public GroundActions(@PlayerActions wrapper) { m_Wrapper = wrapper; }
            public InputAction @Movement => m_Wrapper.m_Ground_Movement;
            public InputAction @Attack => m_Wrapper.m_Ground_Attack;
            public InputAction @SpecialAttack => m_Wrapper.m_Ground_SpecialAttack;
            public InputActionMap Get() { return m_Wrapper.m_Ground; }
            public void Enable() { Get().Enable(); }
            public void Disable() { Get().Disable(); }
            public bool enabled => Get().enabled;
            public static implicit operator InputActionMap(GroundActions set) { return set.Get(); }
            public void SetCallbacks(IGroundActions instance)
            {
                if (m_Wrapper.m_GroundActionsCallbackInterface != null)
                {
                    @Movement.started -= m_Wrapper.m_GroundActionsCallbackInterface.OnMovement;
                    @Movement.performed -= m_Wrapper.m_GroundActionsCallbackInterface.OnMovement;
                    @Movement.canceled -= m_Wrapper.m_GroundActionsCallbackInterface.OnMovement;
                    @Attack.started -= m_Wrapper.m_GroundActionsCallbackInterface.OnAttack;
                    @Attack.performed -= m_Wrapper.m_GroundActionsCallbackInterface.OnAttack;
                    @Attack.canceled -= m_Wrapper.m_GroundActionsCallbackInterface.OnAttack;
                    @SpecialAttack.started -= m_Wrapper.m_GroundActionsCallbackInterface.OnSpecialAttack;
                    @SpecialAttack.performed -= m_Wrapper.m_GroundActionsCallbackInterface.OnSpecialAttack;
                    @SpecialAttack.canceled -= m_Wrapper.m_GroundActionsCallbackInterface.OnSpecialAttack;
                }
                m_Wrapper.m_GroundActionsCallbackInterface = instance;
                if (instance != null)
                {
                    @Movement.started += instance.OnMovement;
                    @Movement.performed += instance.OnMovement;
                    @Movement.canceled += instance.OnMovement;
                    @Attack.started += instance.OnAttack;
                    @Attack.performed += instance.OnAttack;
                    @Attack.canceled += instance.OnAttack;
                    @SpecialAttack.started += instance.OnSpecialAttack;
                    @SpecialAttack.performed += instance.OnSpecialAttack;
                    @SpecialAttack.canceled += instance.OnSpecialAttack;
                }
            }
        }
        public GroundActions @Ground => new GroundActions(this);
        private int m_KeyboardSchemeIndex = -1;
        public InputControlScheme KeyboardScheme
        {
            get
            {
                if (m_KeyboardSchemeIndex == -1) m_KeyboardSchemeIndex = asset.FindControlSchemeIndex("Keyboard");
                return asset.controlSchemes[m_KeyboardSchemeIndex];
            }
        }
        public interface IGroundActions
        {
            void OnMovement(InputAction.CallbackContext context);
            void OnAttack(InputAction.CallbackContext context);
            void OnSpecialAttack(InputAction.CallbackContext context);
        }
    }
}
