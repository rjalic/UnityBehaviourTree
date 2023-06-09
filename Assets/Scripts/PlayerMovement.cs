using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private GameObject bulletPrefab;
    private float fireCooldown = 0.5f;
    private float fireCooldownLeft = 0;

    private Plane groundPlane;

    void Start()
    {
        groundPlane = new Plane(Vector3.up, Vector3.zero);
    }

    void Update()
    {
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");

        Vector3 direction = new Vector3(h, 0f, v);
        transform.Translate(Time.deltaTime * speed * direction);

        if (Input.GetMouseButtonDown(0) && fireCooldownLeft <= 0)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            float rayDistance;
            if (groundPlane.Raycast(ray, out rayDistance))
            {
                Vector3 targetPosition = ray.GetPoint(rayDistance);
                targetPosition.y = 1;

                // Calculate the shoot direction from the player's position with offset to the target position

                // Spawn the bullet and set its target
                GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
                BulletMovement bulletController = bullet.GetComponent<BulletMovement>();
                Vector3 shootDirection = (targetPosition - transform.position).normalized;
                bulletController.SetPlayer(this);
                bulletController.SetTarget(shootDirection);
            }
            fireCooldownLeft = fireCooldown;
        }
        
        fireCooldownLeft -= Time.deltaTime;
    }
}
