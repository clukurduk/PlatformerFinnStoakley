using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] float maxSpeed, moveForce, jumpHeight,groundShotgunForce, airShotgunForce, reloadSpeed, secondReloadSpeed, xVelocityMaintained, yVelocityMaintained;
    float xDirection=0f, shots, maxShots=2, currentShotgunForce;
    bool jumpable, shootable, airborne;
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
        if (!airborne)
        {
            currentShotgunForce = groundShotgunForce;
        }
        else
        {
            currentShotgunForce = airShotgunForce;
        }

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
            if (rb.velocity.y < 0f)
            {
                rb.velocity = new Vector3(rb.velocity.x / xVelocityMaintained, 0, 0);
            }
            else
            {
                rb.velocity = new Vector3(rb.velocity.x / xVelocityMaintained, rb.velocity.y/yVelocityMaintained, 0);
            }
            rb.AddForce((-direction)*currentShotgunForce, ForceMode2D.Impulse);
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
            airborne = true;
        }
    }
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            jumpable = true;
            airborne = false;
            
        }

        if (other.gameObject.CompareTag("Killbox"))
        {
            Debug.Log("Death");
        }
    }
    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            jumpable = false;
            airborne=true;
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
