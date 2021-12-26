using UnityEngine;

public class InvisibleEnemy : MonoBehaviour
{
    [SerializeField] private SkinnedMeshRenderer _meshRenderer = null;
    [SerializeField] private Material _invisibleMaterial = null;
    [SerializeField] private Material _visibleMaterial = null;

    public void Awake()
    {
        TurnInvisible();
    }

    public void NoLongerStealth()
    {
        _meshRenderer.sharedMaterial = _visibleMaterial;
    }

    public void TurnInvisible()
    {
        _meshRenderer.sharedMaterial = _invisibleMaterial;
    }
}
