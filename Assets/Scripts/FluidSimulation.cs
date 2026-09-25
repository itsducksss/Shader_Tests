using System;
using UnityEngine;

public class FluidSimulation : MonoBehaviour
{
    [SerializeField] private float slosh_add_x;
    [SerializeField] private float slosh_add_z;
    [Space]
    [SerializeField] private float MaxSlosh;
    [SerializeField] private float sloshX, sloshZ;
    [Space]
    [SerializeField] private MaterialPropertyBlock block =  new MaterialPropertyBlock();
    [SerializeField] private MeshRenderer rend;
    
    private Vector3 vel;
    private Vector3 angular_vel;

    private void Awake()
    {
        block = new MaterialPropertyBlock();
    }

    // Update is called once per frame
    void Update()
    {
        float angular_influence = 0.01f;
        slosh_add_x += Mathf.Clamp(
            vel.x + vel.y * 0.2f + (angular_vel.z + angular_vel.y) * angular_influence,
            -MaxSlosh, MaxSlosh);
        slosh_add_z += Mathf.Clamp(
            vel.z + vel.y * 0.2f + (angular_vel.x + angular_vel.y) * angular_influence,
            -MaxSlosh, MaxSlosh);
        
        block.SetFloat("_RotationX", Mathf.Clamp(-sloshX, -1f, 1f));
        block.SetFloat("_RotationZ", Mathf.Clamp(sloshZ, -1f, 1f));
        rend.SetPropertyBlock(block);
    }
}
