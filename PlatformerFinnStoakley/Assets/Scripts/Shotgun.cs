using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class Shotgun : MonoBehaviour
{
    [SerializeField] float shotgunKickback, basePos, resetRate;
    [SerializeField] GameObject shotgunParticle, bullet;
    [SerializeField] float force;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {




        
        if (transform.localPosition.y < basePos) {
            transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y+resetRate, transform.localPosition.z);
                }
    }


    public void ShotgunAnimation()
    {
            Debug.Log(GameManager.Instance.playerShots);
            transform.localPosition += new Vector3(transform.localPosition.x, shotgunKickback, transform.localPosition.z);

        Quaternion particleRotation = Quaternion.Euler(0, 0, 0);
        GameObject p = Instantiate(shotgunParticle, transform.position, Quaternion.LookRotation(transform.up));
        Destroy(p, 2);
        GameObject v =Instantiate(bullet, transform.position, Quaternion.identity);
        Destroy(v, 2);
        Rigidbody2D rb = v.GetComponent<Rigidbody2D>();
        rb.AddForce(transform.up * force, ForceMode2D.Impulse);
    }
}
