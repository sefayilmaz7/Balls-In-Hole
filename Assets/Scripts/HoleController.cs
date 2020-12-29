using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoleController : MonoBehaviour
{
    Rigidbody rb;

    Vector3 target;
    Vector3 direction;

    [SerializeField]
    float speed = 10;
    [HideInInspector]
    public float spacing = 0;

    [SerializeField]
    bool canMove = true;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }
    private void FixedUpdate()
    {
        if (!canMove)
        {
            direction = (target - transform.position).normalized;
            rb.MovePosition(transform.position + direction * speed * Time.deltaTime);
            if (Vector3.Distance(transform.position, target) < 0.3f)
            {
                canMove = true;
            }
        }
    }

    public void Swipe(Vector3 dir)
    {
        if (canMove)
        {
            RaycastHit hit;
            RaycastHit hit2;

            if (Physics.Raycast(transform.position, dir, out hit2, Mathf.Infinity))
            {
                if (hit2.transform.CompareTag("Player"))
                {
                    spacing = 4;
                }
                else
                {
                    spacing = 4;
                }
            }
            
            if (Physics.Raycast(transform.position, dir, out hit, Mathf.Infinity, 1 << LayerMask.NameToLayer("Cube")))
            {
                target = hit.point - dir / spacing;
            }
            canMove = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == ("Coin"))
        {
            Destroy(other.gameObject);
        }
        else if (other.gameObject.tag == ("Obstacle"))
        {
            canMove = true;
            Destroy(other.gameObject);
        }
        else if (other.gameObject.tag == ("Trap"))
        {
            canMove = true;
            GameController.instance.GameOver();
        }
        else if (other.gameObject.tag == ("Player"))
        {
            // setting game object's activity as false when enters hole
            other.gameObject.SetActive(false);
        }
    }


}
