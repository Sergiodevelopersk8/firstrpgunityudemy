using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventarioUI : MonoBehaviour
{
    [SerializeField] private InventaroSlot slotPrefab;
    [SerializeField] private Transform contenedor;
    // Start is called before the first frame update
    List<InventaroSlot> slotsDisponibles  = new List<InventaroSlot> (); 
    void Start()
    {
        InicializarInventario();
    }


private void InicializarInventario()
    {

        /*usamos un for para llamar de la clase  de inventario el nuemro de slots*/

        for(int i = 0; i < Inventario.Instance.NumerosdeSlot; i++)
        {
            InventaroSlot nuevoSlot =  Instantiate(slotPrefab, contenedor);
            nuevoSlot.Index = i; 
            slotsDisponibles.Add(nuevoSlot);
        }


    }


}
