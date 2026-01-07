using UnityEngine;

public class Skorlling : MonoBehaviour
{
    [SerializeField] private Material mat;
    [SerializeField] private float smooth = 0.5f;

    [SerializeField] float offset;

    private void Update()
    {
        if (mat == null) return;
        offset = Mathf.Repeat(offset + Time.deltaTime * smooth, 1f);
        mat.SetTextureOffset("_MainTex", Vector2.up * offset);
    }
}
