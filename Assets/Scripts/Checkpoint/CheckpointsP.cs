using UnityEngine.SceneManagement;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointsP : MonoBehaviour
{
    private static CheckpointsP checkpointsP;
    public static bool first;
    private List<Transform> checkpoints;
    public Transform lastCheckpoint;

    void Awake()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;

        if (!first)
        {
            checkpoints = new List<Transform>();

            Transform[] transforms = gameObject.GetComponentsInChildren<Transform>();
            foreach (Transform transform in transforms)
            {
                if (transform.transform.parent != null)
                {
                    checkpoints.Add(transform);
                }
            }
            first = true;
        }

        if (checkpointsP == null)
        {
            checkpointsP = this;
            DontDestroyOnLoad(checkpointsP);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Update()
    {
        FindLastCheckpoint();
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
       GameObject.FindWithTag("Player").GetComponent<Transform>().position = new Vector3(-48.2f, -9.4f, 0f);
    }

    private void FindLastCheckpoint()
    {
        if (lastCheckpoint != null)
        {
            lastCheckpoint = checkpoints[checkpoints.FindIndex(d => d == lastCheckpoint)];
        }
    }
    public void NascerCheckpoint()
    {
        if (lastCheckpoint != null)
        {
            Transform Player = GameObject.FindWithTag("Player").GetComponent<Transform>();
            Player.position = lastCheckpoint.position;
        }
    }

}
