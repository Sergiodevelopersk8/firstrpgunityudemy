using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

    public class Personaje : MonoBehaviour
    {

    [SerializeField] private PersonajeStats stats;

    public PersonajeVida PersonajeVida { get; private set; }
        
    public PersonajeAnimaciones PersonajeAnimaciones { get; private set; }
    public PersonajeMana PersonajeMana { get; private set; }

        private void Awake()
        {
        PersonajeVida = GetComponent<PersonajeVida>();
        PersonajeAnimaciones = GetComponent<PersonajeAnimaciones>();
        PersonajeMana = GetComponent<PersonajeMana>();
        }

        public void RestaurarPersonaje()
        {
        PersonajeVida.RestaurarPersonaje();
        PersonajeAnimaciones.RevivirPersonaje();
        PersonajeMana.RestablecerMana();
        }


    private void AtributoRespuesta(TipoAtributo tipo)
    {

        if(stats.PuntosDisponibles <= 0)
        {
            return;
        }
        switch (tipo)
        {
            case TipoAtributo.Fuerza:
                stats.Fuerza++;
                stats.AñadirBonudPorAtributoFuerza();
                break;
            case TipoAtributo.Ineligencia:
                stats.Inteligencia++;

                stats.AñadirBonudPorAtributoInteligencia();
                break;
            case TipoAtributo.Destreza:
                stats.Destreza++;

                stats.AñadirBonudPorAtributoDestreza();
                break;
        }
        stats.PuntosDisponibles -= 1;
    }

    private void OnEnable()
    {
        AtributoButton.EventoAgregarAtributo += AtributoRespuesta;
    }

    private void OnDisable()
    {
        AtributoButton.EventoAgregarAtributo -= AtributoRespuesta;
        
    }


}

