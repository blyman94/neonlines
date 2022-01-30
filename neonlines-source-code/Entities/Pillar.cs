using UnityEngine;

/// <summary>
/// Represents the pillar obstacles the player must avoid during gameplay.
/// </summary>
public class Pillar : MonoBehaviour
{
    [Tooltip("The black center portion of the pillar to be scaled.")]
    [SerializeField] private Transform pillarTransform;

    [Tooltip("The outline portion of the pillar to be scaled.")]
    [SerializeField] private Transform outlineTransform;

    /// <summary>
    /// Box collider of the pillar.
    /// </summary>
    private BoxCollider2D bc2d;

    private void Awake()
    {
        bc2d = GetComponent<BoxCollider2D>();
    }

    /// <summary>
    /// Changes the height of the pillar and box collider.
    /// </summary>
    /// <param name="height">New height of the pillar in standard Unity 
    /// units.</param>
    public void SetHeight(float height)
    {
        pillarTransform.localScale = new Vector3(pillarTransform.localScale.x,
            height, pillarTransform.localScale.z);
        outlineTransform.localScale = new Vector3(outlineTransform.localScale.x,
            height + 0.1f, outlineTransform.localScale.z);
        bc2d.size = new Vector2(bc2d.size.x, height);
    }

    /// <summary>
    /// Changes the width of the pillar and box collider.
    /// </summary>
    /// <param name="width">New width of the pillar in standard Unity 
    /// units.</param>
    public void SetWidth(float width)
    {
        pillarTransform.localScale = new Vector3(width,
            pillarTransform.localScale.y, pillarTransform.localScale.z);
        outlineTransform.localScale = new Vector3(width + 0.1f,
            outlineTransform.localScale.y, outlineTransform.localScale.z);
        bc2d.size = new Vector2(width, bc2d.size.y);
    }
}
