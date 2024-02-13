using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] float maxSpeed, moveForce, jumpHeight, groundShotgunForce, airShotgunForce, reloadSpeed, secondReloadSpeed, xVelocityMaintained, yVelocityMaintained;
    float xDirection = 0f, currentShotgunForce;
    bool jumpable, shootable, airborne;
    Coroutine reloadCoroutine;
    [SerializeField] private float freezeFrameTime;
    float timer;
    [SerializeField] GameObject impactFrame;
    const int maxShots = 2;
    [SerializeField] GameObject shotgunParticle;

    Rigidbody2D rb;
    //reference rb
    //
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        timer = 0;
    }

    // Update is called once per frame
    void Update()
    {
        Update2();
        Timer();
        Movement();
        Jump();
        Shotgun();
    }

    void Update2()
    {

    }

    private void Timer()
    {
        timer -= Time.unscaledDeltaTime;
        if (timer <= 0)
        {
            Time.timeScale = 1f;
            impactFrame.SetActive(false);
        }
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

        if (GameManager.Instance.playerShots < 1)
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
            if (rb.velocity.y < 0f && direction.y < 0f)
            {
                rb.velocity = new Vector3(rb.velocity.x / xVelocityMaintained, 0, 0);
            }
            else
            { 
                rb.velocity = new Vector3(rb.velocity.x / xVelocityMaintained, rb.velocity.y/yVelocityMaintained, 0);
                Debug.Log("poo");
            }
            
            rb.AddForce((-direction)*currentShotgunForce, ForceMode2D.Impulse);
            GameManager.Instance.playerShots -= 1;
            Debug.Log(shootable.ToString());
            if (GameManager.Instance.playerShots < 1)
            {
                reloadCoroutine = StartCoroutine(Co_Reload());
            }

            Time.timeScale = 0.03f;
            impactFrame.SetActive(true);
            timer = freezeFrameTime;
            
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
        GameManager.Instance.playerShots += 1;
        yield return new WaitForSeconds(secondReloadSpeed);
        Debug.Log("reload2");
        GameManager.Instance.playerShots += 1;
        Debug.Log(GameManager.Instance.playerShots);
    }
}
