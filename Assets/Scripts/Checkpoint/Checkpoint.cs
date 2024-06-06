using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    private CheckpointsP checkpointsP;

    void Awake()
    {
        checkpointsP = GetComponentInParent<CheckpointsP>();
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            checkpointsP.lastCheckpoint = this.transform;
        }
    }
}
