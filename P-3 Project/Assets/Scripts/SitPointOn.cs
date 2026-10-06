using System;
using UnityEngine;

public class SitPointOn : MonoBehaviour
{
    private float detectionRadius = 1.2f; 
    private bool isPlayerClose = false; 
    private bool isInFinalArea = false; 
    private GameObject player;
   [SerializeField] private GameObject visualClue;
    private GameObject finalArea;

    private void Start()
    {
        player = GameObject.FindWithTag("Player");
        finalArea = GameObject.FindWithTag("FinalArea");
    }

    void Update()
    {
        

        if (player != null)
        {
            //distance between the object and the player
            float distance = Vector3.Distance(player.transform.position, transform.position);
            float finalDistance = Vector3.Distance(finalArea.transform.position, transform.position);

            if (finalDistance <= detectionRadius * 10) // some offset can be adjusted
            {
                isInFinalArea = true;
                visualClue.SetActive(false);
            }
            else if (distance <= detectionRadius && !isPlayerClose && !isInFinalArea)
            {
                isPlayerClose = true;
                visualClue.SetActive(false);
            }
            else if (distance > detectionRadius && isPlayerClose)
            {
                isPlayerClose = false;
                visualClue.SetActive(true);
            }

            
        }
    }

   /*private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("FinalArea"))
        {
            visualClue.SetActive(false);
        }
        Debug.Log("FinalArea viausal clue collde");
    }*/
}