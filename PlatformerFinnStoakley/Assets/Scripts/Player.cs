using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] float maxSpeed, moveForce, jumpHeight, shotgunForce, reloadSpeed, secondReloadSpeed;
    float xDirection=0f, shots, maxShots=2;
    bool jumpable, shootable;
    Coroutine reloadCoroutine;


    Rigidbody2D rb;
    //reference rb
    //
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        shots = maxShots;
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
        Jump();
        Shotgun();
    }

    void Movement()
    {
        xDirection = Input.GetAxis("Horizontal");
        if (xDirection < 0f && rb.velocity.magnitude < maxSpeed)
        {

            rb.AddForce(new Vector3(xDirection * (moveForce/100),0,0), ForceMode2D.Impulse);
        }
        if (xDirection > 0f && rb.velocity.magnitude < maxSpeed)
        {

            rb.AddForce(new Vector3(xDirection * (moveForce / 100), 0, 0), ForceMode2D.Impulse);
        }
    }

    void Shotgun()
    {
        if (shots < 1)
        {
            shootable = false;
            
        }
        else
        {
            shootable= true;
        }

        if (Input.GetKeyDown(KeyCode.Mouse0)&&shootable)
        {

            Vector3 direction = (Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x,Input.mousePosition.y,0)));
            direction = direction - transform.position;
            direction.z = 0f;
            direction.Normalize();
            Debug.Log(direction.ToString());
            rb.AddForce((-direction)*shotgunForce, ForceMode2D.Impulse);
            shots -= 1;
            Debug.Log(shootable.ToString());
            if (shots < 1)
            {
                reloadCoroutine = StartCoroutine(Co_Reload());
            }
        }
    }

    void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space)&&jumpable)
        {
            rb.AddForce(new Vector3(0, jumpHeight, 0), ForceMode2D.Impulse);
            jumpable = false;
        }
    }
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            jumpable = true;
            
        }
    }

    IEnumerator Co_Reload()
    {
        yield return new WaitForSeconds(reloadSpeed);
        Debug.Log("reload1");
        shots += 1;
        yield return new WaitForSeconds(secondReloadSpeed);
        Debug.Log("reload2");
        shots += 1;
        Debug.Log(shots);
    }
}
