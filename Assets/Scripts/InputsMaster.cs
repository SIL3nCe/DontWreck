// GENERATED AUTOMATICALLY FROM 'Assets/Scripts/InputsMaster.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class @InputsMaster : IInputActionCollection, IDisposable
{
    public InputActionAsset asset { get; }
    public @InputsMaster()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""InputsMaster"",
    ""maps"": [
        {
            ""name"": ""World"",
            ""id"": ""35920aa0-9e45-4891-b70d-bc8390806c29"",
            ""actions"": [
                {
                    ""name"": ""MouseMove"",
                    ""type"": ""Button"",
                    ""id"": ""ecb576d8-9914-4ffb-82cf-4973b4073811"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""939675e6-0b43-4ea4-9d81-97cd41191044"",
                    ""path"": """",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MouseMove"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""2D Vector"",
                    ""id"": ""29aa4bed-942f-4706-b276-5a9be1db9eb7"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MouseMove"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""a2176528-728a-47cd-89b5-486099e8f65a"",
                    ""path"": ""<Mouse>/delta/y"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MouseMove"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""c1ecc918-d148-4dbd-a445-e27f2d590e43"",
                    ""path"": ""<Mouse>/delta/y"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MouseMove"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""4a89ddab-370b-4b6c-ac36-ec267ddaad80"",
                    ""path"": ""<Mouse>/delta/x"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MouseMove"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""94a064da-bfd3-48db-a002-008709b3f03e"",
                    ""path"": ""<Mouse>/delta/x"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MouseMove"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // World
        m_World = asset.FindActionMap("World", throwIfNotFound: true);
        m_World_MouseMove = m_World.FindAction("MouseMove", throwIfNotFound: true);
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

    // World
    private readonly InputActionMap m_World;
    private IWorldActions m_WorldActionsCallbackInterface;
    private readonly InputAction m_World_MouseMove;
    public struct WorldActions
    {
        private @InputsMaster m_Wrapper;
        public WorldActions(@InputsMaster wrapper) { m_Wrapper = wrapper; }
        public InputAction @MouseMove => m_Wrapper.m_World_MouseMove;
        public InputActionMap Get() { return m_Wrapper.m_World; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(WorldActions set) { return set.Get(); }
        public void SetCallbacks(IWorldActions instance)
        {
            if (m_Wrapper.m_WorldActionsCallbackInterface != null)
            {
                @MouseMove.started -= m_Wrapper.m_WorldActionsCallbackInterface.OnMouseMove;
                @MouseMove.performed -= m_Wrapper.m_WorldActionsCallbackInterface.OnMouseMove;
                @MouseMove.canceled -= m_Wrapper.m_WorldActionsCallbackInterface.OnMouseMove;
            }
            m_Wrapper.m_WorldActionsCallbackInterface = instance;
            if (instance != null)
            {
                @MouseMove.started += instance.OnMouseMove;
                @MouseMove.performed += instance.OnMouseMove;
                @MouseMove.canceled += instance.OnMouseMove;
            }
        }
    }
    public WorldActions @World => new WorldActions(this);
    public interface IWorldActions
    {
        void OnMouseMove(InputAction.CallbackContext context);
    }
}
