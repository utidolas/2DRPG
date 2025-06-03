using UnityEngine;

public class EnemyLoot : MonoBehaviour
{
    [Header("Config")]
    [SerializeField] private float expDrop;

    // prop return expdrop value
    public float ExpDrop => expDrop;
}
