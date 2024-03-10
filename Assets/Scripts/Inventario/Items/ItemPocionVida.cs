using Assets.Scripts.Inventario;
using UnityEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


[CreateAssetMenu(menuName = "Items/Pocion Vida")]
public class ItemPocionVida : InventarioItem
    {

    [Header("Pocion Info")]
    public float HPRestauracion;

    }

