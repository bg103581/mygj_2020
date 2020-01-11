using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Camera cam;

    private CharacterController _controller;

    [SerializeField]
    private float _speed = 12f;
    [SerializeField]
    private float _jumpHeight = 3f;
    [SerializeField]
    private float _gravity = -9.81f;

    private Vector3 _velocity;

    [SerializeField]
    private Transform _groundCheck;
    [SerializeField]
    private float _groundDistance = 0.4f;
    [SerializeField]
    private LayerMask _groundMask;

    private bool _isGrounded;

    // Start is called before the first frame update
    void Start() {
        _controller = GetComponent<CharacterController>();
        cam = Camera.main;
    }

    // Update is called once per frame
    void Update() {
        _isGrounded = Physics.CheckSphere(_groundCheck.position, _groundDistance, _groundMask);

        if (_isGrounded && _velocity.y < 0) {
            _velocity.y = -2f;
        }

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;

        _controller.Move(move * _speed * Time.deltaTime);

        if (Input.GetButtonDown("Jump") && _isGrounded) {
            _velocity.y = Mathf.Sqrt(_jumpHeight * -2f * _gravity);
        }

        _velocity.y += _gravity * Time.deltaTime;

        _controller.Move(_velocity * Time.deltaTime);
        
    }

    #region ROCHE
    
   /*public void RaycastClickedObject() {
        if (Input.GetMouseButtonDown(1)) {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit)) {

                // Check if we hit an interactable
                Interactable interactable = hit.collider.GetComponent<Interactable>();
                
                Debug.Log("We hit " + hit.collider.name);
            }
        }
    }*/

    #endregion
}
