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

    public void AddItem(InventarioItem itemporAdd, int cantidad)
    {
        if(itemporAdd == null)
        {
            return;
        }

        /*verificacipn en caso de tener ya un item similar en el invetario*/
        List<int> indexs = ValidarExistencias(itemporAdd.Id);
        if (itemporAdd.EsAcumulable)
        {
            if(indexs.Count > 0)
            {
                for(int i = 0; i < indexs.Count; i++)
                {
                    if(itemsInventario[indexs[i]].Cantidad < itemporAdd.AcumulacionMax)
                    {
                        itemsInventario[indexs[i]].Cantidad += cantidad;
                        if (itemsInventario[indexs[i]].Cantidad > itemporAdd.AcumulacionMax)
                        {
                            int diferencia = itemsInventario[i].Cantidad - itemporAdd.AcumulacionMax;
                            itemsInventario[indexs[i]].Cantidad = itemporAdd.AcumulacionMax;
                            AddItem(itemporAdd, diferencia);
                        }

                        InventarioUI.Instance.DibujarItemEnInventario(itemporAdd, itemsInventario[indexs[i]].Cantidad, indexs[i]);
                        return;
                    }
                }
            }
        }

        if(cantidad <= 0)
        {
            return;
        }

        if(cantidad > itemporAdd.AcumulacionMax)
        {
            AddItemEnSlotDisponible(itemporAdd, itemporAdd.AcumulacionMax);
            cantidad -= itemporAdd.AcumulacionMax;
            AddItem(itemporAdd, cantidad);
        }

        else
        {
            AddItemEnSlotDisponible(itemporAdd, cantidad);
        }

    }


    private List<int> ValidarExistencias(string itemID)
    {
        List<int> indexDelItem = new List<int>();
        for(int i = 0; i < itemsInventario.Length; i++)
        {

            if(itemsInventario[i]!= null)
            {
                if (itemsInventario[i].Id == itemID)
                {
                    indexDelItem.Add(i);
                }
            }

            
        }
        return indexDelItem;
    }

    private void AddItemEnSlotDisponible(InventarioItem item, int cantidad)
    {
        for (int i = 0; i < itemsInventario.Length; i++) 
        {
        
        if(itemsInventario[i] == null)
            {
                itemsInventario[i] = item;
                itemsInventario[i].Cantidad = cantidad;
                InventarioUI.Instance.DibujarItemEnInventario(item, cantidad,i);
                return;
            }
        
        }
    }

}
