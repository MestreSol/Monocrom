using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Player player;

    public float _horizontal;

    [SerializeField] public float _speed;
    [SerializeField] private float _jumpForce;

    private float _wallSlidingSpeed = 0.75f;
    private float _wallJumpingTime = 0.5f;
    public float _wallJumpingCounter;

    public float dashCooldown = 1f;
    public float dashSpeed = 1f;

    [SerializeField] private int _jumpCountMax;
    [SerializeField] private Vector2 _wallJumpingPower = new Vector2(8f, 16f);

    [SerializeField] public Rigidbody2D _rigidbody;
    [SerializeField] public GameObject _wallCheck;
    [SerializeField] public LayerMask _wallLayer;
    [SerializeField] public GameObject _attackCheck;

    public LayerMask _enemyLayer;
    public void Move()
    {
        _horizontal = Input.GetAxisRaw("Horizontal");
        player.animation.Flip();
        _rigidbody.velocity = new Vector2(_horizontal * _speed, _rigidbody.velocity.y);
        player.animator.SetBool("isRun", _horizontal != 0f);
    }
    public void Jump()
    {
        if (_wallJumpingCounter < _jumpCountMax)
        {
            _rigidbody.velocity = new Vector2(_rigidbody.velocity.x, _jumpForce);
            _wallJumpingCounter++;
            player.animation.Jump();
        }
    }
    public void WallSliding()
    {
        if (player.state.isWalled() && _horizontal != 0f && _rigidbody.velocity.y < 0f)
        {
            player.state.isWallSliding = true;
            _rigidbody.velocity = new Vector2(_rigidbody.velocity.x, _wallSlidingSpeed);
        }
        else
        {
            player.state.isWallSliding = false;
        }
    }
    public void WallJumping()
    {
        if (player.state.isWallSliding && Input.GetKeyDown(KeyCode.Space))
        {
            player.state.isWallSliding = false;
            player.state.isFacingRight = !player.state.isFacingRight;
            Vector3 localScale = player.gameObject.transform.localScale;
            localScale.x *= -1;
            player.gameObject.transform.localScale = localScale;
            _rigidbody.velocity = new Vector2(_wallJumpingPower.x * (_horizontal > 0f ? -1f : 1f), _wallJumpingPower.y);
            _wallJumpingCounter = _wallJumpingTime;
        }
    }

    public void Dash()
    {
        player.state.isDashing = true;
        player.collider.enabled = false;
        Invoke(nameof(StopDash), dashCooldown);
        _rigidbody.AddForce(new Vector2(_rigidbody.velocity.x * dashSpeed, _rigidbody.velocity.y));
    }
    private void StopDash()
    {
        player.state.isDashing = false;
        player.collider.enabled = true;
    }
}