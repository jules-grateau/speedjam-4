using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class GroundCheck : MonoBehaviour
{
    [SerializeField]
    private LayerMask _groundLayer;
    private BoxCollider2D _collider;
    [SerializeField]
    private float _groundDistance = 0.05f;
    private bool _isGrounded;

    public bool IsGrounded { get { return _isGrounded; } }

    private void Start()
    {
        _collider = GetComponent<BoxCollider2D>();
    }

    void FixedUpdate()
    {
        // Perform the raycast to check for ground
        _isGrounded = Physics2D.BoxCast(transform.position, _collider.size, 0, Vector2.down, _groundDistance, _groundLayer);

        // Debug visualization of the raycast
        Debug.DrawRay(transform.position, Vector3.down * _groundDistance, _isGrounded ? Color.green : Color.red);
    }
}