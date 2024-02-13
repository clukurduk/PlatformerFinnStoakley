using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shotgun : MonoBehaviour
{
    [SerializeField] float shotgunKickback, basePos, resetRate;
    [SerializeField] GameObject shotgunParticle;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {



        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            Debug.Log(GameManager.Instance.playerShots);
            transform.localPosition += new Vector3(transform.localPosition.x, shotgunKickback, transform.localPosition.z);
            
            Quaternion particleRotation = Quaternion.Euler(0, 0, 0);
            Instantiate(shotgunParticle, transform.position, particleRotation);
        }
        if (transform.localPosition.y < basePos) {
            transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y+resetRate, transform.localPosition.z);
                }
    }
}
