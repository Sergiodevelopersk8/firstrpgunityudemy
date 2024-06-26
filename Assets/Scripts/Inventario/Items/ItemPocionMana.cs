﻿using Assets.Scripts.Inventario;
using UnityEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

[CreateAssetMenu(menuName = "Items/Pocion Mana")]
    public class ItemPocionMana  : InventarioItem
    {

    [Header("Pocion Info")] 
    public float MPRestauracion;


    public override bool UsarItem()
    {
        if (Inventario.Instance.Personaje.PersonajeMana.SePuedeRestaurar)
        {
            Inventario.Instance.Personaje.PersonajeMana.RestaurarMana(MPRestauracion);
            return true;
        }

        return false;

    }


}

