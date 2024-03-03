using UnityEngine;
using UnityEngine.Windows;

[RequireComponent(typeof(CircleCollider2D))]
public class GroundCheck : MonoBehaviour
{
    [SerializeField]
    private LayerMask _groundLayer;
    [SerializeField]
    private float _groundDistance = 0.05f;
    [SerializeField]
    private float _slopeCheckDistance = 0.5f;
    private bool _isGrounded;
    private bool _isOnSlope;

    private Vector2 _slopeNormalPerp;
    private float _slopeDownAngle;
    private CircleCollider2D _circleCollider;


    public bool IsGrounded { get { return _isGrounded; } }
    public bool IsOnSlope { get { return _isOnSlope; } }

    public Vector2 SlopeNormalPerp { get { return _slopeNormalPerp; } }

    private void Start()
    {
        _circleCollider = GetComponent<CircleCollider2D>();
    }

    void FixedUpdate()
    {
        // Perform the raycast to check for ground
        _isGrounded = Physics2D.OverlapCircle(transform.position - new Vector3(0.0f, _circleCollider.radius), _groundDistance, _groundLayer);
        // Debug visualization of the raycast
        Debug.DrawRay(transform.position - new Vector3(0.0f, _circleCollider.radius), Vector3.down * _groundDistance, _isGrounded ? Color.green : Color.red);

        SlopeCheck();
    }
    private void SlopeCheck()
    {
        var checkPos = transform.position - new Vector3(0.0f, _circleCollider.radius);

        SlopeCheckHorizontal(checkPos);
        SlopeCheckVertical(checkPos);
    }

    private void SlopeCheckVertical(Vector2 checkPos)
    {


        RaycastHit2D hit = Physics2D.Raycast(checkPos, Vector2.down, 0.5f, _groundLayer);

        if (hit)
        {

            _slopeNormalPerp = Vector2.Perpendicular(hit.normal).normalized;

            _slopeDownAngle = Vector2.Angle(hit.normal, Vector2.up);

            if (_slopeDownAngle != 0)
            {
                _isOnSlope = true;
            }

            Debug.DrawRay(hit.point, _slopeNormalPerp, Color.blue);
            Debug.DrawRay(hit.point, hit.normal, Color.green);

        } else
        {
            _isOnSlope = false;
        }
    }
    
    private void SlopeCheckHorizontal(Vector2 checkPos)
    {
        RaycastHit2D slopeHitFront = Physics2D.Raycast(checkPos, transform.right, _slopeCheckDistance, _groundLayer);
        RaycastHit2D slopeHitBack = Physics2D.Raycast(checkPos, -transform.right, _slopeCheckDistance, _groundLayer);

        Debug.DrawRay(checkPos, transform.right * _slopeCheckDistance, Color.yellow);
        Debug.DrawRay(checkPos, -transform.right * _slopeCheckDistance, Color.black);

        if (slopeHitFront) _isOnSlope = true;
        else _isOnSlope = false;

    }
}