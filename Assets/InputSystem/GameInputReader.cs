using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using System;

namespace SJ22
{
    public class GameplayInputReader : GameActions.IGameplayActions
    {
        public Action<bool> Shoot;
        public Action Action;
        public Action<Vector2> Move;

        public void OnAction(InputAction.CallbackContext context)
        {
            if(context.performed) Action?.Invoke();
        }

        public void OnMove(InputAction.CallbackContext context)
        {
            //if (context.performed || context.canceled)
                Move?.Invoke(context.ReadValue<Vector2>());
        }

        public void OnShoot(InputAction.CallbackContext context)
        {
            if (context.performed || context.canceled)
                Shoot?.Invoke(context.ReadValueAsButton());
        }
    }

    [CreateAssetMenu(fileName = "InputReader", menuName = "Input Reader", order = 0)]
    public class GameInputReader : ScriptableObject
    {
        [NonSerialized] public GameplayInputReader Gameplay;
        [NonSerialized] public GameActions Actions;

        [SerializeField] InputActionAsset UI;

        void OnEnable()
        {
            Actions = new GameActions();
            Gameplay = new GameplayInputReader();
            Actions.Gameplay.SetCallbacks(Gameplay);

            EnableGamplay();
        }

        public void EnableGamplay()
        {
            Actions.Gameplay.Enable();
        }

        public void DisableGameplay()
        {
            Actions.Gameplay.Enable();
        }

        public void EnableUI()
        {
            UI.Enable();
        }

        public void DisableUI()
        {
            UI.Disable();
        }
    }

}