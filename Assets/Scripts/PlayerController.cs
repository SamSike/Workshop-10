// COMP30019 - Graphics and Interaction
// (c) University of Melbourne, 2022

using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float speed = 1.0f; // Default speed sensitivity
    [SerializeField] private ProjectileController projectilePrefab;
    [SerializeField] private float projectileOffset = 0.5f;

    Vector3 _aimDirection = Vector3.forward;

    // Update is called once per frame
    private void Update()
    {
        if (Input.GetKey(KeyCode.LeftArrow))
            transform.Translate(Vector3.left * (this.speed * Time.deltaTime));
        if (Input.GetKey(KeyCode.RightArrow))
            transform.Translate(Vector3.right * (this.speed * Time.deltaTime));
        
        // Use the "down" variant to avoid spamming projectiles. Will only get
        // triggered on the frame where the key is initially pressed.
        if (Input.GetMouseButtonDown(0))
        {
            Aim();
            var projectile = Instantiate(this.projectilePrefab);
            projectile.transform.position = transform.position + _aimDirection * projectileOffset;
            projectile.Initialize(_aimDirection);
        }
    }

    void Aim(){
        var mouseRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        var gamePlane = new Plane(Vector3.up, Vector3.zero);

        if(gamePlane.Raycast(mouseRay, out var distance)){
            Vector3 hit = mouseRay.GetPoint(distance);

            _aimDirection = (hit - transform.position).normalized; // normalized because direction
        }

    }
}
