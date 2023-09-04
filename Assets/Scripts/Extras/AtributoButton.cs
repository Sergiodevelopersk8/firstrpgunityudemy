using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public enum TipoAtributo
{
    Fuerza,
    Ineligencia,
    Destreza
}
public class AtributoButton : MonoBehaviour
{

    public static Action<TipoAtributo> EventoAgregarAtributo;

    [SerializeField] private TipoAtributo tipo;

    public void AgregaAtributo()
    {
        EventoAgregarAtributo?.Invoke(tipo);
    }


}
