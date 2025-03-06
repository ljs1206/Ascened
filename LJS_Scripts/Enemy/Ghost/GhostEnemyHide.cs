using UnityEngine;

public class GhostEnemyHide : MonoBehaviour
{
    [Header("Setting")]
    [SerializeField] private Material _hideMat;
    [SerializeField] private SkinnedMeshRenderer _skinnedMeshRenderer;
    [SerializeField] private GhostEnemy _ghostEnemy;

    private Material _originMat;

    private void Start() {
        _originMat = _skinnedMeshRenderer.material;
    }

    public void StartHide(){
        _ghostEnemy.Hide = true;
        _skinnedMeshRenderer.material = _hideMat;
        gameObject.layer = 29;
    }

    public void StopHide(){
        _ghostEnemy.Hide = false;
        _skinnedMeshRenderer.material = _originMat;
        gameObject.layer = 29;
    }
}
