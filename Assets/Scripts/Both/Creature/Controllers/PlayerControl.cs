using Assets.Scripts.Both.Creature.Controllers;
using System.Collections;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Assets.Scripts.Both.Creature.Controllers
{

    /// <summary>
    /// Client owner. Control its character.
    /// </summary>
    public class PlayerControl : NetworkBehaviour
    {
        public Vector2 VectorAxis { get; private set; }
        public Vector2 VectorState { get; private set; }
        public bool IsSpecial { get; private set; }
        public bool IsAttack { get; private set; }

        [SerializeField] private PlayerInput script;

        // Start is called before the first frame update
        void Start()
        {
            if (!IsOwner) return;

            IsAttack = false;
            IsSpecial = false;
            MoveNonAffect();
        }

        private void OnEnable()
        {
            ListenMovement();
            ListenAttack();
            ListenProtect();
        }

        private void OnDisable()
        {
            UnlistenMovement();
            UnlistenAttack();
            UnlistenProtect();
        }

        #region Input listener setup
        private void ListenMovement()
        {
            script.actions["Movement"].started += Movement;
            script.actions["Movement"].performed += Movement;
            script.actions["Movement"].canceled += Movement;
        }

        private void UnlistenMovement()
        {
            script.actions["Movement"].started -= Movement;
        }

        private void ListenAttack()
        {
            script.actions["Attack"].started += Attack;
        }

        private void UnlistenAttack()
        {
            script.actions["Attack"].started -= Attack;
        }

        private void ListenProtect()
        {
            script.actions["SpecialAttack"].started += Shield;
        }

        private void UnlistenProtect()
        {
            script.actions["SpecialAttack"].started -= Shield;
        }
        #endregion

        #region Config movement direct
        private void MoveNonAffect()
        {
            VectorState = Vector2.one;
        }

        private void Root()
        {
            VectorState = Vector2.zero;
        }
        #endregion

        private void Movement(InputAction.CallbackContext ctx)
        {
            VectorAxis = ctx.ReadValue<Vector2>();
        }

        private void Attack(InputAction.CallbackContext ctx)
        {
            UnlistenAttack();
            if (!IsSpecial) UnlistenProtect();

            IsAttack = true;
        }

        private void Shield(InputAction.CallbackContext obj)
        {
            UnlistenProtect();
            Root();

            IsSpecial = true;
        }

        [ClientRpc]
        public void RefreshClientRpc(NetworkBehaviourReference controller, int number, ClientRpcParams clientRpcParams = default)
        {
            if (!IsHost && IsServer) return;

            if (controller.TryGet(out PlayerController controllerComponent))
            {
                switch (number)
                {
                    case 1:
                        {
                            //Attack refresh
                            IsAttack = false;

                            controllerComponent.ResponseRefreshServerRpc(number);

                            ListenAttack();
                            if (!IsSpecial) ListenProtect();
                            break;
                        }
                    case 2:
                        {
                            //Special refresh, IsAttack = false
                            IsSpecial = false;

                            controllerComponent.ResponseRefreshServerRpc(number);

                            MoveNonAffect();
                            ListenProtect();
                            break;
                        }
                    case 3:
                        {
                            //Attack refresh in Special refresh
                            IsSpecial = false;
                            IsAttack = false;

                            controllerComponent.ResponseRefreshServerRpc(1);

                            ListenAttack();

                            controllerComponent.ResponseRefreshServerRpc(2);

                            MoveNonAffect();
                            ListenProtect();
                            break;
                        }
                }
            }
        }
    }
}