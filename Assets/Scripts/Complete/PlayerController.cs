using Unity.Mathematics;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Complete
{
    public class PlayerController : MonoBehaviour
    {
        [Header("Movement")] [Space(10)]
        public float moveSpeed = 5f;
        private Vector2 _moveVector;
        private Vector2 _lookVector;
     
        [Header("Projectile Spawn")] [Space(10)]
        public GameObject projectile;

        public float projectileSpeed;
        public float spawnTime;
        private float _spawnTimeCounter;
        private Vector2 _spawnPosition;
        
        
        [Header("Components")]
        private Rigidbody2D _rigidbody2D;


        private void Start()
        {
            _rigidbody2D = GetComponent<Rigidbody2D>();
            
            if (_lookVector == Vector2.zero)
            {
                _lookVector.x = 1;
            }
        }

        private void Update()
        {
            UpdateInput();

            UpdateProjectileSpawn();
            
        }
        
        private void FixedUpdate()
        {
            UpdateMovement();
        }

        private void UpdateProjectileSpawn()
        {
            if (!(Time.time > _spawnTimeCounter)) return;
            
            var projectileClone = Instantiate(projectile, UpdateSpawnPosition(), quaternion.identity);
            projectileClone.TryGetComponent(out Rigidbody2D rb2D);
            rb2D.linearVelocity = _lookVector * projectileSpeed + _rigidbody2D.linearVelocity;
 
            _spawnTimeCounter = Time.time + spawnTime;
            Destroy(projectileClone, 4f);
        }

        private Vector2 UpdateSpawnPosition()
        {
            _spawnPosition.x = transform.localPosition.x + (_moveVector.x/2);
            _spawnPosition.y = transform.localPosition.y + (_moveVector.y/2);

            if (_spawnPosition == Vector2.zero)
            {
                _spawnPosition.x = transform.localPosition.x + 0.5f;
            }
            return _spawnPosition;
        }

        private void UpdateMovement()
        {
            _rigidbody2D.linearVelocity = _moveVector * moveSpeed;
        }

        private void UpdateInput()
        {
            _moveVector.x = (Keyboard.current.dKey.isPressed ? 1f : 0f) + (Keyboard.current.aKey.isPressed ? -1f : 0f);
            _moveVector.y = (Keyboard.current.wKey.isPressed ? 1f : 0f) + (Keyboard.current.sKey.isPressed ? -1f : 0f);

            if (_moveVector.magnitude > 1)
            {
                _moveVector = _moveVector.normalized;
            }
            
            if (_moveVector != Vector2.zero)
            {
                _lookVector = _moveVector;
            }
        }
    }
}