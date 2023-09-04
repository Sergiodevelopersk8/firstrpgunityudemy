using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VidaBase : MonoBehaviour
{

    [SerializeField] protected float saludInicia;
    [SerializeField] protected float saludMax;
    
   // creamos una propiedad
   public float Salud { get; protected set; } 
    
    
   protected  virtual void Start()
    {
       Salud = saludInicia; 
    }

    public void RecibirDano(float cantidad)
    {
        if (cantidad <= 0)
        {
            return;
        }

        if(Salud > 0f)
        {
            Salud -= cantidad;
            ActualizarBarraVida(Salud,saludMax);
            if(Salud <= 0f)
            {
                ActualizarBarraVida(Salud, saludMax);
                PersonajeDerrotado();
            }
        }

    }


    protected virtual void ActualizarBarraVida(float vidaActual, float vidaMax)
    {

    }

    protected virtual void PersonajeDerrotado()
    {

    }

}
