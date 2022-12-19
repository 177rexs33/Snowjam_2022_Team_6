using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.Windows;

namespace SJ22
{
    class EnemyPathWaypoint
    {
        public Vector2 Position;
        public float WaitTime = 0f;
    }


    public enum EnemyPathFlagEnum
    {
        End,
        Charge,
        FollowScreen,
    }

    public class EnemyPathFlag
    {
        public EnemyPathFlagEnum Flag;
        public float? Time = null;
    }

    class EnemySpawn
    {
        public float Time;
        public EnemyProperties Properties;
        public float Speed;
        // Object is either EnemyPosition or EnemyPositionFlag
        public List<object> Path;
    }

    public class EnemySpawner : MonoBehaviour
    {
        [SerializeField] GameTime time;
        [SerializeField] EventSO restartFromCheckpointEvent;
        [SerializeField] GameObject screen;
        [SerializeField] EnemyDatabase enemyDatabase;
        [SerializeField] GameObject shotContainer;
        [SerializeField] GameObject enemyPrefab;

        /// <summary>
        /// Format of the file: 
        /// [time:float]: [enemy name:string], [speed:float], ([point:vec2](:[waittime:float])? | end)...
        /// eg: 10: snowman, 5, (0, 12) (-6, 6) end
        /// </summary>
        [SerializeField] TextAsset script;

        List<EnemySpawn> spawns = new List<EnemySpawn>();
        int spawnIndex = 0;

        void Reset()
        {
            screen = GameObject.Find("Screen");
        }

        void Awake()
        {
            time.TimeChanged += RecalculateSpawnIndex;
            restartFromCheckpointEvent.Event += ClearShotContainer;
        }

        void OnDestroy()
        {
            time.TimeChanged -= RecalculateSpawnIndex;
            restartFromCheckpointEvent.Event -= ClearShotContainer;
        }

        void Start()
        {
            ParseScript();
        }

        void ClearShotContainer()
        {
            foreach(Transform shot in shotContainer.transform)
            {
                Destroy(shot.gameObject);
            }
        }

        void RecalculateSpawnIndex(float _)
        {
            int newSpawnIndex;
            for(newSpawnIndex = 0; newSpawnIndex < spawns.Count; newSpawnIndex++)
            {
                var spawn = spawns[newSpawnIndex];
                if(spawn.Time > time.Time)
                {
                    break;
                }
            }
            // this makes it so if none of the spawns are after, we set to
            // the index after the end of list
            spawnIndex = newSpawnIndex;
        }


        void Update()
        {
            while(spawnIndex < spawns.Count)
            {
                var nextSpawn = spawns[spawnIndex];
                if(time.Time > nextSpawn.Time)
                {
                    SpawnEnemy(nextSpawn);
                    ++spawnIndex;
                }
                else
                {
                    break;
                }
            }
        }

        void ParseScript()
        {
            string text = script.text;
            text = text.Replace("\r\n", "\n");
            var lines = text.Split('\n');
            Regex lineRx = new Regex(
                @"(?<time>[-\d.]+): *(?<enemy>\w+), (?<speed>[-\d.]+),(?<positions> (?:[\w_]+|\([-\d.]+, *[-\d.]+\))(?::[\d.]+)?)+",
                RegexOptions.Compiled
            );
            Regex positionRx = new Regex(
                @"\((?<x>[-\d.]+), *(?<y>[-\d.]+)\)(?::(?<wait>[-\d.]+))?",
                RegexOptions.Compiled
            );
            Regex flagRx = new Regex(
                @"^ *(?<flag>[\w_]+)(?::(?<time>[-\d.]+))?$"
            );
            int lineNum = 0;
            foreach(var line in lines)
            {
                if(line.Length == 0 || line[0] == '#')
                {
                    continue;
                }
                void LogWarning()
                {
                    Debug.LogWarningFormat(
                        "Level Script: Line {0} \"{1}\" does not match RegEx, ignoring.",
                        lineNum, line
                    );
                }

                ++lineNum;
                var lineMatch = lineRx.Match(line);
                if (!lineMatch.Success)
                {
                    LogWarning();
                    continue;
                }

                float time = float.Parse(lineMatch.Groups["time"].Value);
                var enemyProperties = enemyDatabase[lineMatch.Groups["enemy"].Value];
                if(enemyProperties == null)
                {
                    Debug.LogWarningFormat(
                        "Could not find enemy by name \"{0}\", ignoring",
                        lineMatch.Groups["enemy"].Value
                    );
                    break;
                }
                float speed = float.Parse(lineMatch.Groups["speed"].Value);


                EnemySpawn enemySpawn = new EnemySpawn {
                    Time = float.Parse(lineMatch.Groups["time"].Value),
                    Properties = enemyProperties,
                    Speed = speed
                };

                List<object> positions = new List<object>();
                foreach(Capture capture in lineMatch.Groups["positions"].Captures)
                {
                    var flagMatch = flagRx.Match(capture.Value);
                    if (flagMatch.Success)
                    {
                        EnemyPathFlag pathFlag = new EnemyPathFlag();
                        var flagStr = flagMatch.Groups["flag"].Value;
                        if (flagStr == "end")
                        {
                            pathFlag.Flag = EnemyPathFlagEnum.End;
                        }
                        else if (flagStr == "followscreen")
                        {
                            pathFlag.Flag = EnemyPathFlagEnum.FollowScreen;
                        }
                        else if (flagStr == "charge")
                        {
                            pathFlag.Flag = EnemyPathFlagEnum.Charge;
                        }
                        else
                        {
                            LogWarning();
                            continue;
                        }

                        if (flagMatch.Groups["time"].Success)
                        {
                            pathFlag.Time = float.Parse(
                                flagMatch.Groups["time"].Value
                            );
                        }
                        positions.Add(
                            pathFlag
                        );
                        continue;
                    }

                    var positionMatch = positionRx.Match(capture.Value);
                    if (!float.TryParse(positionMatch.Groups["wait"].Value, out var waitTime)){
                        waitTime = 0f;
                    }

                    positions.Add(
                        new EnemyPathWaypoint
                        {
                            Position = new Vector2(
                                float.Parse(positionMatch.Groups["x"].Value),
                                float.Parse(positionMatch.Groups["y"].Value)
                            ),
                            WaitTime = waitTime
                        }
                    );
                }

                enemySpawn.Path = positions;

                spawns.Add(enemySpawn);
            }

            spawns.Sort((a, b) => Comparer<float>.Default.Compare(a.Time,b.Time));
        }

        void SpawnEnemy(EnemySpawn enemySpawn)
        {
            var enemyGO = Instantiate(enemyPrefab);
            enemyGO.transform.SetParent(screen.transform);
            
            var enemy = enemyGO.GetComponent<Enemy>();
            enemy.Properties = enemySpawn.Properties;
            enemy.ShotContainer = shotContainer;

            var enemyMove = enemyGO.GetComponent<EnemyMove>();
            enemyMove.Speed = enemySpawn.Speed;
            enemyMove.Path = enemySpawn.Path;
        }
    }
}
