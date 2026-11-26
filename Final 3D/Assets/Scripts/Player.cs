using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float _torqueForce;
    [SerializeField] private Rigidbody _ballRigidbody;
    [SerializeField] private Rigidbody _playerRigidbody;
    [SerializeField] private Animator _animator;
    [SerializeField] private float _moveSpeed;
    [SerializeField] private float _jump;
    [SerializeField] private float _mouseSensitivity;
    [SerializeField] private float _rotationSpeed;

    private Transform _cameraTrans;
    private float _rotationX;
    private float _rotationY;
    private bool _isGrounded;
    //private Collision 
    //private Vector3 righta = new Vector3(1,0,0);
    //private Vector3 lefta = new Vector3(-1,0,0);

    // ------------------------------------------------------------------------
    // You don't need to code anything in this method :)
    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        _cameraTrans = Camera.main.transform;
    }

    // ------------------------------------------------------------------------
    private void Update()
    {
        
        // Rotates the player based on where the mouse is pointed.
        // You don't need to change this code.
        float mouseY = Input.GetAxis("Mouse Y");
        _rotationY += mouseY * _mouseSensitivity;
        _rotationY = Mathf.Clamp(_rotationY, -60.0f, 60.0f);

        float mouseX = Input.GetAxis("Mouse X");
        _rotationX += mouseX * _mouseSensitivity;

        _cameraTrans.localEulerAngles = new Vector3(-_rotationY, 0, 0);
        transform.localEulerAngles = new Vector3(0, _rotationX, 0);

        // Code the player's left/right movement and jump using the veritcal
        //      and horizontal input.
        float vertical = Input.GetAxis("Vertical");
        float horizontal = Input.GetAxis("Horizontal");

        transform.Rotate(_playerRigidbody.transform.up, horizontal * _rotationSpeed * Time.deltaTime);
        transform.position+=vertical*_playerRigidbody.transform.forward*_moveSpeed*Time.deltaTime;

        if(Input.GetKey(KeyCode.D)){
            transform.position+=_playerRigidbody.transform.right*_moveSpeed*Time.deltaTime;
            //Debug.Log("test");
        }
        if(Input.GetKey(KeyCode.A)){
            transform.position-=_playerRigidbody.transform.right*_moveSpeed*Time.deltaTime;
            //Debug.Log("test");
        }
        

        // This rotates the ball as you move.
        // You don't need to change this code.
        _ballRigidbody.AddTorque((
            ((horizontal + mouseX * _mouseSensitivity) * transform.forward) +
            (vertical * transform.right))
            * _torqueForce
        );

        //jump

        if(Input.GetKeyDown(KeyCode.Space)&&_isGrounded){
            //Debug.Log(_playerRigidbody.transform.position);
            _playerRigidbody.isKinematic = false;
            //_playerRigidbody.transform.position = new Vector3(50,10,25);
            

            _playerRigidbody.AddForce(Vector3.up * _jump, ForceMode.Impulse);
            _isGrounded=false;
            //Debug.Log(_playerRigidbody.transform.position);
        }
    }

    // ------------------------------------------------------------------------
    // Utilize a COLLISION method to prevent the player from double jumping.
    // The player should only be able to jump after making contact with the ground.

    private void OnCollisionEnter(Collision col){
        if(col.gameObject.CompareTag("Ground")){
            _isGrounded=true;
            //Debug.Log("ground");
        }
    }
}
