using Assets.Scripts.Inventario;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/*lo hacemos Singleton*/

public class Inventario : Singleton<Inventario>
{



    [SerializeField] private int numeroDeSlots;
    public int NumerosdeSlot => numeroDeSlots;

    [Header ("Items")]
    [SerializeField] private InventarioItem[] itemsInventario;


    private void Start()
    {
        itemsInventario = new InventarioItem[numeroDeSlots];
    }



}
