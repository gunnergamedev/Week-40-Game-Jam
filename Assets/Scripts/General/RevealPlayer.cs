using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RevealPlayer : MonoBehaviour
{
    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            other.GetComponent<PlayerSortingOrder>().RevealPlayer();
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            other.GetComponent<PlayerSortingOrder>().ResetPlayerSortingOrder();
        }
    }
}
