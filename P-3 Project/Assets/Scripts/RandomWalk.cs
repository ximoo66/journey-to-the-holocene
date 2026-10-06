using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class RandomWalk : MonoBehaviour
{
 private int terrainWidth;
    private int terrainLength;
    private int terrainPosX;
    private int terrainPosZ;
    private Terrain terrain;
    private NavMeshAgent agent;
    //private Animator animator;  // Reference to the Animator

    public float waitTime = 2f;  // Time to wait before changing path
    private float timer = 0f;    // Timer to track the waiting period

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        //animator = GetComponent<Animator>();  // Get the Animator component

        // Search the Terrain and store the reference to read the dimensions
        terrain = GameObject.Find("Terrain").GetComponent<Terrain>();
        if (terrain != null)
        {
            terrainWidth = (int)terrain.terrainData.size.x;
            terrainLength = (int)terrain.terrainData.size.z;
            terrainPosX = (int)terrain.transform.position.x;
            terrainPosZ = (int)terrain.transform.position.z;
        }
        else
        {
            Debug.LogError("No Terrain found!");
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Check if the deer has a path
        bool hasPath = agent.hasPath;

        // Set the animator parameter to true when there is a path, false otherwise
        //animator.SetBool("isWalking", hasPath);

        // If the deer is not walking, start the waiting timer
        if (!hasPath)
        {
            timer += Time.deltaTime;  // Increment the timer by the time elapsed since last frame

            // If the wait time has passed, generate a new path
            if (timer >= waitTime)
            {
                // Reset the timer
                timer = 0f;

                // Generate a random position within the terrain bounds
                int posx = Random.Range(terrainPosX, terrainPosX + terrainWidth);
                int posz = Random.Range(terrainPosZ, terrainPosZ + terrainLength);

                Vector3 pos = new Vector3(posx, 0, posz);
                // Get the terrain height at the random position
                pos.y = Terrain.activeTerrain.SampleHeight(pos);

                // Set the new destination for the NavMeshAgent
                agent.destination = new Vector3(posx, pos.y, posz);
            }
        }
    }
}
