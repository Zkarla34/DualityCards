using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Configs/Level Particle Config")]
public class LevelParticleConfig : ScriptableObject
{
    public LevelParticleEntry[] levels;
}
[System.Serializable]
public class LevelParticleEntry
{
    public int level;
    public GameObject particlePrefab;
}
