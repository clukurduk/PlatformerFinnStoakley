using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGuard : MonoBehaviour
{

    [SerializeField] float speed;
    private bool playerFound;
    private GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        playerFound = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (playerFound)
        {
            var step = speed * Time.deltaTime;
            Vector3 playerPosition = player.transform.position;
            transform.position = Vector3.MoveTowards(transform.position, playerPosition, step);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            playerFound = true;
            player = collision.gameObject;
        }
    }
}
