using Assets.Scripts.Both.Creature.Controllers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldDetect : MonoBehaviour
{
    [SerializeField] private Collider2D up;
    [SerializeField] private Collider2D down;
    [SerializeField] private Collider2D left;
    [SerializeField] private Collider2D right;
    [SerializeField] private PlayerController owner;
    [SerializeField] private int orient;

    // Start is called before the first frame update
    void Start()
    {
        owner = gameObject.transform.parent.GetComponent<PlayerController>();

        if (owner is null)
        {
            Debug.Log("Shield noname!");
            //Destroy
            gameObject.SetActive(false);
        }
    }

    private void FixedUpdate()
    {
        if (owner.IsSpecial)
        {
            orient = Mathf.Abs(owner.Animator.GetInteger("orientation"));

            switch (orient)
            {
                case 1:
                    up.enabled = true;
                    break;
                case 2:
                    down.enabled = true;
                    break;
                case 3:
                    left.enabled = true;
                    break;
                case 4:
                    right.enabled = true;
                    break;
            }
        }
        else
        {
            switch (orient)
            {
                case 1:
                    up.enabled = false;
                    break;
                case 2:
                    down.enabled = false;
                    break;
                case 3:
                    left.enabled = false;
                    break;
                case 4:
                    right.enabled = false;
                    break;
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log(collision.gameObject.name);
    }
}
