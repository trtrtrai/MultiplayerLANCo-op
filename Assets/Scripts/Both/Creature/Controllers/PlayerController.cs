using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEditor.PackageManager;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Assets.Scripts.Both.Creature.Controllers
{

    /// <summary>
    /// Server owner
    /// </summary>
    public class PlayerController : NetworkBehaviour
    {
        private IEnumerator attackDelay;
        private IEnumerator specialDelay;
        [SerializeField] private Animator animator;
        [SerializeField] private Rigidbody2D player;
        [SerializeField] private PlayerControl control;
        [SerializeField] private Vector2 VectorSpeed;

        public Animator Animator => animator;
        public bool IsSpecial => control.IsSpecial;
        public bool IsAttack => control.IsAttack;
        public Vector2 VectorAxis => control.VectorAxis;
        public Vector2 VectorState => control.VectorState;

        private void Awake()
        {
            animator = GetComponent<Animator>();
            player = GetComponent<Rigidbody2D>();
        }

        // Start is called before the first frame update
        void Start()
        {
            //init animation
            animator.SetInteger("orientation", 2);
        }

        private void FixedUpdate()
        {
            if (control is null) return;

            //Movement
            VectorSpeed = new Vector2(VectorAxis.x * 250 * Time.fixedDeltaTime, VectorAxis.y * 250 * Time.fixedDeltaTime);
            player.velocity = VectorSpeed * VectorState;

            if (IsAttack)
            {
                Attack();
            }

            if (IsSpecial) Special();

            UpdateAnimation();
        }

        private void Attack()
        {
            attackDelay = AttackDelay();
            StartCoroutine(attackDelay);

            Animator.SetBool("isAttack", IsAttack);
        }

        private void Special()
        {
            specialDelay = SpecialDelay();
            StartCoroutine(specialDelay);

            Animator.SetBool("isShield", IsSpecial);
        }

        private IEnumerator AttackDelay()
        {
            yield return new WaitForSeconds(.5f);

            ClientRpcParams clientRpcParams = new ClientRpcParams
            {
                Send = new ClientRpcSendParams
                {
                    TargetClientIds = new ulong[] { OwnerClientId }
                }
            };

            control.RefreshClientRpc(this, 1, clientRpcParams);
        }

        private IEnumerator SpecialDelay()
        {
            yield return new WaitForSeconds(.5f);

            ClientRpcParams clientRpcParams = new ClientRpcParams
            {
                Send = new ClientRpcSendParams
                {
                    TargetClientIds = new ulong[] { OwnerClientId }
                }
            };

            if (IsAttack)
            {
                StopCoroutine(attackDelay);

                control.RefreshClientRpc(this, 3, clientRpcParams);
            }
            else control.RefreshClientRpc(this, 2, clientRpcParams);
        }

        [ServerRpc(RequireOwnership = false)]
        public void ResponseRefreshServerRpc(int number, ServerRpcParams serverRpcParams = default)
        {
            if (OwnerClientId != serverRpcParams.Receive.SenderClientId) return;

            switch (number)
            {
                case 1:
                    {
                        //Attack refresh response
                        Animator.SetBool("isAttack", IsAttack);
                        Animator.SetInteger("orientation", Mathf.Abs(Animator.GetInteger("orientation")));
                        break;
                    }
                case 2:
                    {
                        //Special refresh
                        Animator.SetBool("isShield", IsSpecial);
                        Animator.SetInteger("orientation", Mathf.Abs(Animator.GetInteger("orientation")));
                        break;
                    }
            }
        }

        /// <summary>
        /// Control animation state
        /// </summary>
        private void UpdateAnimation()
        {
            //handle movement animation (depend on animator architect)
            animator.SetFloat("speed", Mathf.Abs(VectorSpeed.x) + Mathf.Abs(VectorSpeed.y));

            if (IsAttack || IsSpecial)
            {
                var orien = animator.GetInteger("orientation");
                animator.SetInteger("orientation", orien < 0 ? orien : -orien);
            }
            else
            {
                //handle movement animation direct (depend on animator architect)
                if (VectorSpeed.x < 0) animator.SetInteger("orientation", 3);//left animate
                else if (VectorSpeed.x > 0) animator.SetInteger("orientation", 4); //right animate
                if (VectorSpeed.y > 0) animator.SetInteger("orientation", 1); //up animate
                else if (VectorSpeed.y < 0) animator.SetInteger("orientation", 2); //down animate
            }
        }

        /// <summary>
        /// Inject PLayerControl
        /// </summary>
        /// <param name="ctrl"></param>
        public void AddControl(PlayerControl ctrl) => control = ctrl;
    }
}