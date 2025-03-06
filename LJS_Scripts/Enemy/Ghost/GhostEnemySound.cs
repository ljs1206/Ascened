using FMODUnity;
using Sirenix.OdinInspector;
using UnityEngine;

public class GhostEnemySound : EnemySound
{
    private GhostEnemy _ghostEnemy;

    private int _currentAttackNumber;

    [field: BoxGroup("Sound")] 
    [Header("Sound Ref")]
    [SerializeField] private EventReference _castSoundType1;
    [field: BoxGroup("Sound")] 
    [SerializeField] private EventReference _castSoundType2;
    
    public void Start(){
        _ghostEnemy = _enemy as GhostEnemy;
    }

    public void CastSoundType1Play() => RuntimeManager.PlayOneShot(_castSoundType1, transform.position);
    public void CastSoundType2Play() => RuntimeManager.PlayOneShot(_castSoundType2, transform.position);
}
