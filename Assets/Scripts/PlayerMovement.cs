using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private GameObject bulletPrefab;
    private readonly float _fireCooldown = 0.5f;
    private float _fireCooldownLeft;
    private Plane _groundPlane;

    private void Start()
    {
        _groundPlane = new Plane(Vector3.up, Vector3.zero);
    }

    private void Update()
    {
        var h = Input.GetAxisRaw("Horizontal");
        var v = Input.GetAxisRaw("Vertical");

        var direction = new Vector3(h, 0f, v);
        transform.Translate(Time.deltaTime * speed * direction);

        if (Input.GetMouseButtonDown(0) && _fireCooldownLeft <= 0)
        {
            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            float rayDistance;
            if (_groundPlane.Raycast(ray, out rayDistance))
            {
                var targetPosition = ray.GetPoint(rayDistance);
                targetPosition.y = 1;

                // Calculate the shoot direction from the player's position with offset to the target position

                // Spawn the bullet and set its target
                var bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
                var bulletController = bullet.GetComponent<BulletMovement>();
                var shootDirection = (targetPosition - transform.position).normalized;
                bulletController.SetPlayer(gameObject);
                bulletController.SetTarget(shootDirection);
            }

            _fireCooldownLeft = _fireCooldown;
        }

        _fireCooldownLeft -= Time.deltaTime;
    }
}