using Assets.Scripts.Inventario;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/*lo hacemos Singleton*/

public class Inventario : Singleton<Inventario>
{
    [Header("Items")]
    [SerializeField] private InventarioItem[] itemsInventario;
    [SerializeField] private Personaje personaje;
    [SerializeField] private int numeroDeSlots;

    public Personaje Personaje => personaje;
    public int NumerosdeSlot => numeroDeSlots;


     public InventarioItem[] ItemsInventario => itemsInventario;
    


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
                            int diferencia = itemsInventario[indexs[i]].Cantidad - itemporAdd.AcumulacionMax;
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
                itemsInventario[i] = item.copiarItem();
                itemsInventario[i].Cantidad = cantidad;
                InventarioUI.Instance.DibujarItemEnInventario(item, cantidad,i);
                return;
            }
        
        }
    }


    private void EliminarItem(int index)
    {

        itemsInventario[index].Cantidad--;
        if(itemsInventario[index].Cantidad < 0)
        {
            itemsInventario[index].Cantidad = 0;
            itemsInventario[index] = null;
            InventarioUI.Instance.DibujarItemEnInventario(null,0,index);
        }
        else
        {
            InventarioUI.Instance.DibujarItemEnInventario(itemsInventario[index], itemsInventario[index].Cantidad, index);
        }

    }


    public void MoverItem(int indexInicial, int indexFinal)
    {
        if(itemsInventario[indexInicial] == null || itemsInventario[indexFinal] != null)
        {
            return;
        }

        //copiar el item en el slot final
        InventarioItem itemPorMover = itemsInventario[indexInicial].copiarItem();
        itemsInventario[indexFinal] = itemPorMover;
        InventarioUI.Instance.DibujarItemEnInventario(itemPorMover, itemPorMover.Cantidad, indexFinal);
        
        //borarr el slot inicial
        itemsInventario[indexInicial] = null;
        InventarioUI.Instance.DibujarItemEnInventario(null, 0, indexInicial);

    }



    private void UsarItem(int index)
    {
        if(itemsInventario[index] == null)
        {
            return;
        }

        if (itemsInventario[index].UsarItem())
        {
            EliminarItem(index);
        }

    }



    #region Eventos

    private void SlotInteraccionRespuesta(TiposDeInteraccion tipo, int index)
    {
        switch (tipo)
        {
            case TiposDeInteraccion.Usar:
                UsarItem(index);
                break;

            case TiposDeInteraccion.Equipar:
                break;


            case TiposDeInteraccion.Remover:
                break;
        }
    }

    private void OnEnable()
    {
        InventaroSlot.EventoSlotInteraccion += SlotInteraccionRespuesta;
    }

    
    private void OnDisable()
    {
        InventaroSlot.EventoSlotInteraccion -= SlotInteraccionRespuesta;
    }
    #endregion



}
