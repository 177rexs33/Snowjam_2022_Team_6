using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SJ22
{
    public class CheckpointManager : MonoBehaviour
    {
        [SerializeField] GameTime time;
        [SerializeField] List<GameTime> timesToUnpause;
        [SerializeField] ScreenProperties screenProperties;
        [SerializeField] Checkpoints checkpoints;
        [SerializeField] EventSO restartFromCheckpointEvent;
        [SerializeField] GameObject checkpointPrefab;

        int nextCheckpointIndex = 0;
        float lastCheckpoint = 0;

        float NextCheckpointTime => checkpoints.Timepoints[nextCheckpointIndex];

        void Awake()
        {
            restartFromCheckpointEvent.Event += OnRestart;
            int i = 0;
            foreach(var checkpointTime in checkpoints.Timepoints)
            {
                i++;
                var checkpointGO = Instantiate(checkpointPrefab);
                checkpointGO.name = string.Format("Checkpoint {0}", i);
                checkpointGO.transform.position = new Vector3(
                    0, screenProperties.Speed * checkpointTime, 0
                );
            }
        }

        void Update()
        {
            if (nextCheckpointIndex < checkpoints.Timepoints.Count)
            {
                if(time.Time > NextCheckpointTime)
                {
                    lastCheckpoint = NextCheckpointTime;
                    nextCheckpointIndex++;
                }
            }
        }

        void OnRestart()
        {
            timesToUnpause.ForEach(t => t.Resume());
            time.Time = lastCheckpoint;
            transform.position = new Vector3(
                0, lastCheckpoint * screenProperties.Speed, 0
            );
        }
    }
}
