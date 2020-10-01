using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MalusTrigger : MonoBehaviour
{
    public enum Malus { MoveLeft, MoveRight }

    public Malus selectedMalus;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerController playerScript = other.GetComponent<PlayerController>();
            switch (selectedMalus)
            {
                case Malus.MoveLeft:
                    if (playerScript.moveLeft)
                        playerScript.moveLeft = false;
                    break;
                case Malus.MoveRight:
                    if (playerScript.moveRight)
                        playerScript.moveRight = false;
                    break;
            }
            GetComponent<BoxCollider>().enabled = false;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerController playerScript = other.GetComponent<PlayerController>();

        }
    }
}