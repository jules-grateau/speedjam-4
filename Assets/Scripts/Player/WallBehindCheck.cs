using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class WallBehindCheck : MonoBehaviour
{
    [SerializeField]
    private LayerMask _groundLayer;
    private BoxCollider2D _collider;
    [SerializeField]
    private float _wallDistance = 0.05f;
    private bool _isAgainstWall;

    public bool IsAgainstWall { get { return _isAgainstWall; } }

    private void Start()
    {
        _collider = GetComponent<BoxCollider2D>();
    }

    void FixedUpdate()
    {
        // Perform the raycast to check for ground
        _isAgainstWall = Physics2D.BoxCast(transform.position, _collider.size, 0, Vector2.left, _wallDistance, _groundLayer);

        // Debug visualization of the raycast
        Debug.DrawRay(transform.position, Vector3.right * _wallDistance, _isAgainstWall ? Color.green : Color.red);
    }
}