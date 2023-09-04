using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

//namespace Assets.Scripts
//{

public class PersonajeVida : VidaBase
    {
    
    public static Action EventoPersonajeDerrotado;
    public bool Derrotado { get; private set; }

    public bool PuedeSerCurado => Salud < saludMax;
    
    private BoxCollider2D _boxCollider2D;

    private void Awake()
    {
        _boxCollider2D = GetComponent<BoxCollider2D>();
    }


    protected override void Start()
    {
        base.Start();
        ActualizarBarraVida(Salud, saludMax);
    }
    private void Update()
    {
       
        
        if (Input.GetKeyDown(KeyCode.T))
        {
            RecibirDano(10);
        }
        if (Input.GetKeyDown(KeyCode.Y))
        {
            RestaurarSalud(10);
        }
    }

    public void RestaurarSalud(float cantidad)
    {
        if (Derrotado)
        {
            return;
        }
        if (PuedeSerCurado)
        {
            Salud += cantidad;
            if(Salud > saludMax)
            {
                Salud = saludMax;
            }
            ActualizarBarraVida(Salud, saludMax);
        }
    }
    
    /*Sobre escritura*/
    protected override void PersonajeDerrotado()
    {
        _boxCollider2D.enabled = false;
        Derrotado = true;
       /*el codigo de abajo significa un if que si no es nulo lo invoca el != se remplaza con el signo de ? */
        EventoPersonajeDerrotado?.Invoke();
       
    }
    public void RestaurarPersonaje()
    {
        _boxCollider2D.enabled = true;
        Derrotado = false;
        Salud = saludInicia;
        ActualizarBarraVida(Salud, saludInicia);
    }
    protected override void ActualizarBarraVida(float vidaActual, float vidaMax)
    {
        //base.ActualizarBarraVida(vidaActual, vidaMax);
        UIManager.Instance.ActualizarVidaPersonaje(vidaActual, vidaMax);
    }
}




//}
