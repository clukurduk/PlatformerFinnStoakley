using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Build.Reporting;
using UnityEngine;

public class EnemyGuard : MonoBehaviour
{

    [SerializeField] float speed;
    private bool playerFound;
    private GameObject player;
    [SerializeField] GameObject goal1, goal2;
    Coroutine patrol;
    bool towards1;
    [SerializeField] Vector3 patrolSpeed;


    // Start is called before the first frame update
    void Start()
    {

        playerFound = false;
        patrol = StartCoroutine(Co_Patrol());
    }

    // Update is called once per frame
    void Update()
    {

        //if (playerFound)
        //{

        //    var step = speed * Time.deltaTime;
        //    Vector3 playerPosition = player.transform.position;
        //    transform.position = Vector3.MoveTowards(transform.position, playerPosition, step);
        //}
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            playerFound = true;
            player = collision.gameObject;
        }
    }

    IEnumerator Co_Patrol()
    {

        while (!playerFound)
        {
            if (towards1)
            {
                var step = speed * Time.deltaTime;
                transform.position = Vector3.MoveTowards(transform.position, goal1.transform.position, step);
                if (transform.position == goal1.transform.position)
                {
                    towards1 = false;
                }
            }
            else if (!towards1)
            {
                var step = speed * Time.deltaTime;
                transform.position = Vector3.MoveTowards(transform.position, goal2.transform.position, step);
                if (transform.position == goal2.transform.position)
                {
                    towards1 = true;
                }
            }
           
            yield return new WaitForEndOfFrame();

        }
    }
}
