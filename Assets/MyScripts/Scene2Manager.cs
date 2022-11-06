using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scene2Manager : MonoBehaviour
{
    GameObject player;
    public Transform playerPosition;
    // Start is called before the first frame update
    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        player.transform.position = playerPosition.position;
        player.transform.SetParent(playerPosition);
        player.GetComponent<CharacterController>().enabled=true;
        player.GetComponent<SoundManager>().enabled = true;
        player.GetComponent<PlayerController>().enabled = true;
        player.GetComponent<Attack>().enabled = true;
        player.GetComponentInChildren<Animator>().enabled = true;
        player.GetComponent<PlayerController>().weapon.SetActive(true);
        player.GetComponent<PlayerController>().cam = FindObjectOfType<Camera>();
    }

}
