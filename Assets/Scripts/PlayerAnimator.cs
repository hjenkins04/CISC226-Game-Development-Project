using UnityEngine;

namespace FrostFalls
{
    public class PlayerAnimator : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private Animator _anim;

        [Header("Settings")]
        [SerializeField, Range(1f, 3f)]
        private float _maxIdleSpeed = 2;
        [SerializeField] private float _maxTilt = 5;
        [SerializeField] private float _tiltSpeed = 20;

        [Header("Particles")]
        [SerializeField] private ParticleSystem _jumpParticles;
        [SerializeField] private ParticleSystem _launchParticles;
        [SerializeField] private ParticleSystem _moveParticles;
        [SerializeField] private ParticleSystem _landParticles;

        [Header("Audio Clips")]
        [SerializeField] private AudioClip[] _footsteps;

        private AudioSource _source;
        private IPlayerController _player;
        private bool _grounded;

        private void Awake()
        {
            _source = GetComponent<AudioSource>();
            _player = GetComponentInParent<IPlayerController>();
        }

        private void OnEnable()
        {
            _player.Jumped += OnJumped;
            _player.GroundedChanged += OnGroundedChanged;
            _player.Grappled += OnGrappled;
            _player.FreeFall += OnFreeFall;
            _player.WallSlid += OnWallSlide;
            _player.WallClimbed += OnWallClimb;
            _player.WallJumped += OnWallJump;
            _moveParticles.Play();
        }

        private void OnDisable()
        {
            _player.Jumped -= OnJumped;
            _player.GroundedChanged -= OnGroundedChanged;
            _player.Grappled -= OnGrappled;
            _player.FreeFall -= OnFreeFall;
            _player.FreeFall -= OnWallSlide;
            _player.WallClimbed -= OnWallClimb;
            _player.WallJumped -= OnWallJump;
            _moveParticles.Stop();
        }

        private void Update()
        {
            if (_player == null) return;

            HandleSpriteFlip();
            HandleIdleSpeed();
            HandleCharacterTilt();
        }

        private void HandleSpriteFlip()
        {
            if (_player.FrameInput.x != 0)
            {
                // Determine the direction to face based on input, without directly checking the scale
                bool shouldFaceLeft = _player.FrameInput.x < 0;
                // Check current facing direction and input direction
                // Flip only if the character is facing the opposite direction of movement
                if ((shouldFaceLeft && transform.localScale.x > 0) || (!shouldFaceLeft && transform.localScale.x < 0))
                {
                    SpriteFlip();
                }
            }
        }

        private void SpriteFlip()
        {
            Vector3 theScale = transform.localScale;
            theScale.x *= -1;
            transform.localScale = theScale;
        }

        private void HandleIdleSpeed()
        {
            var inputStrength = Mathf.Abs(_player.FrameInput.x);
            _anim.SetFloat(IdleSpeedKey, Mathf.Lerp(1, _maxIdleSpeed, inputStrength));
        }

        private void HandleCharacterTilt()
        {
            var runningTilt = _grounded ? Quaternion.Euler(0, 0, _maxTilt * _player.FrameInput.x) : Quaternion.identity;
            transform.up = Vector3.RotateTowards(transform.up, runningTilt * Vector2.up, _tiltSpeed * Time.deltaTime, 0f);
        }

        private void OnJumped()
        {
            _anim.SetTrigger(JumpKey);
            _anim.ResetTrigger(FreeFallKey);
            _anim.ResetTrigger(GroundedKey);
            _jumpParticles.Play();
            _launchParticles.Play();
        }

        private void OnGrappled()
        {
            _anim.SetTrigger(GrappleKey);
            _anim.ResetTrigger(FreeFallKey);
            _anim.ResetTrigger(GroundedKey);
        }

        private void OnFreeFall()
        {
            _anim.SetTrigger(FreeFallKey);
            _anim.ResetTrigger(GrappleKey);
            _anim.ResetTrigger(JumpKey);
        }

        private void OnWallSlide()
        {
            _anim.SetTrigger(WallSlideKey);

        }

        private void OnWallClimb(){
            _anim.SetTrigger(WallClimbKey);
        }

        private void OnWallJump()
        {
            SpriteFlip();
        }

        private void OnGroundedChanged(bool grounded, float impact)
        {
            _grounded = grounded;

            if (grounded)
            {
                _anim.SetTrigger(GroundedKey);
                _source.PlayOneShot(_footsteps[Random.Range(0, _footsteps.Length)]);
                _moveParticles.Play();
                _landParticles.Play();
            }
            else
            {
                _moveParticles.Stop();
            }
        }

        private static readonly int GroundedKey = Animator.StringToHash("Grounded");
        private static readonly int IdleSpeedKey = Animator.StringToHash("IdleSpeed");
        private static readonly int JumpKey = Animator.StringToHash("Jump");
        private static readonly int GrappleKey = Animator.StringToHash("Grapple");
        private static readonly int FreeFallKey = Animator.StringToHash("FreeFall");
        private static readonly int WallSlideKey = Animator.StringToHash("WallSlide");
        private static readonly int WallClimbKey = Animator.StringToHash("WallClimb");
    }
}
