using UnityEngine;

public class LiquidWobble : MonoBehaviour
{
    private static int RotXHash = Shader.PropertyToID("_rotx");
    private static int RotZHash = Shader.PropertyToID("_rotz");

    [Header("Wobble Settings")]
    [SerializeField, Range(0f, 1f)] private float maxWobble = 0.3f;
    [SerializeField, Range(0f, 2f)] private float wobbleSpeed = 0.3f;
    [SerializeField, Range(0f, 5f)] private float recoverySpeed = 0.3f;

    private MeshRenderer meshRenderer;
    private Vector3 lastPos, lastRot;

    private float wobbleAmountX, wobbleAmountZ, pulse;
    private float time = .5f;

    private void Start()
    {
        meshRenderer = GetComponent<MeshRenderer>();
        lastPos = transform.position;
        lastRot = transform.rotation.eulerAngles;
    }

    void Update()
    {
        time += Time.deltaTime;

        wobbleAmountX = Mathf.Lerp(wobbleAmountX, 0, Time.deltaTime * recoverySpeed);
        wobbleAmountZ = Mathf.Lerp(wobbleAmountZ, 0, Time.deltaTime * recoverySpeed);

        pulse = Mathf.Sin(2 * Mathf.PI * wobbleSpeed * time);
        var wobbleX = wobbleAmountX * pulse;
        var wobbleZ = wobbleAmountZ * pulse;

        Vector3 velocity = (lastPos - transform.position) / Time.deltaTime;
        Vector3 angularVelocity = transform.eulerAngles - lastRot;

        wobbleAmountX += Mathf.Clamp((velocity.x + (angularVelocity.z * 0.2f)) * maxWobble, -maxWobble, maxWobble);
        wobbleAmountZ += Mathf.Clamp((velocity.z + (angularVelocity.x * 0.2f)) * maxWobble, -maxWobble, maxWobble);

        Shader.SetGlobalFloat("_wobbleX", wobbleX);
        Shader.SetGlobalFloat("_wobbleZ", wobbleZ);

        meshRenderer.material.SetFloat(RotXHash, wobbleAmountX);
        meshRenderer.material.SetFloat(RotZHash, wobbleAmountZ);

        lastPos = transform.position;
        lastRot = transform.eulerAngles;
    }
}
