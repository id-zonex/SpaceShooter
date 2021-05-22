using UnityEngine;

[RequireComponent(typeof(Animator))]
public class PlayerMover : MonoBehaviour
{
    [SerializeField] private float speed;

    [SerializeField] private AnimationClip moveAnimation;
    [SerializeField] private AnimationClip idleAnimation;

    private Camera _mainCamera;
    private Animator _animator;

    private void Start()
    {
        _animator = GetComponent<Animator>();
        _mainCamera = Camera.main;
    }

    private void Update()
    {
        Move();
    }

    private void Move()
    {
        if (Input.GetMouseButton(0))
        {
            Vector2 position = _mainCamera.ScreenToWorldPoint(Input.mousePosition);
            MoveTo(position);

            PlayMoveAnimation();
        }
        else
        {
            PlayIdleAnimation();
        }
    }

    private void MoveTo(Vector2 position)
    {
        transform.position = Vector3.Lerp(transform.position, position, speed * Time.deltaTime);
    }

    private void PlayMoveAnimation()
    {
        _animator.Play(moveAnimation.name);
    }

    private void PlayIdleAnimation()
    {
        _animator.Play(idleAnimation.name);
    }
}
