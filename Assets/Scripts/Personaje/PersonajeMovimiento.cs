using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersonajeMovimiento : MonoBehaviour
{
    //SerializeField serializa la variable para que lo muestre en el inspector
    [SerializeField] private float velocidad;

    public bool EnMovimiento => _direccionMovimiento.magnitude > 0f;
    public Vector2 DireccionMoviento => _direccionMovimiento;
    private Rigidbody2D _rigidbody2d;
    private Vector2 _direccionMovimiento;
    private Vector2 _input;

    private void Awake()
    {
        _rigidbody2d = GetComponent<Rigidbody2D>();
    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        _input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));



        //x

        if (_input.x > 0.1f)
        {
            _direccionMovimiento.x = 1f;
        }
        else if (_input.x < 0f)
        {
            _direccionMovimiento.x = -1f;

        }
        else {
            _direccionMovimiento.x = 0f;

        }


        //y
        if (_input.y > 0.1f)
        {
            _direccionMovimiento.y = 1f;
        }
        else if (_input.y < 0f)
        {
            _direccionMovimiento.y = -1f;

        }
        else
        {
            _direccionMovimiento.y = 0f;

        }


    }


    private void FixedUpdate()
    {
        _rigidbody2d.MovePosition(_rigidbody2d.position + _direccionMovimiento * velocidad * Time.fixedDeltaTime);
    }



}
