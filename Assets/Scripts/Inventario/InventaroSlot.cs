using Assets.Scripts.Inventario;
using System.Collections;
using System.Collections.Generic;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public enum TiposDeInteraccion
{
    Click,
    Usar,
    Equipar,
    Remover
}


public class InventaroSlot : MonoBehaviour
{

    public static Action<TiposDeInteraccion, int> EventoSlotInteraccion;


    [SerializeField] private Image itemIcono;
    [SerializeField] private GameObject fondoCantidad;
    [SerializeField] private TextMeshProUGUI CantidadTMP;

    public int Index { set; get; }

    private Button _buton;

    private void Awake()
    {
        _buton = GetComponent<Button>();
    }

    public void ActualizarSlot(InventarioItem item, int cantidad)
    {

        itemIcono.sprite = item.Icono;
        CantidadTMP.text = cantidad.ToString();


    }

    public void ActivarSloutUI(bool estado)
    {
        itemIcono.gameObject.SetActive(estado);
        fondoCantidad.SetActive(estado);
    }

    public void SeleccionarSlot() {

        _buton.Select();

    
    
    }

    public void ClickSlot()
    {
        EventoSlotInteraccion?.Invoke(TiposDeInteraccion.Click,Index);
    
    //mover items
    if(InventarioUI.Instance.IndexSlotInicialPorMover != -1)
        {
            if(InventarioUI.Instance.IndexSlotInicialPorMover != Index)
            {
                //Mover
                Inventario.Instance.MoverItem(InventarioUI.Instance.IndexSlotInicialPorMover, Index);
            }
        }
    
    }



    public void SlotUsarItem()
    {
        if (Inventario.Instance.ItemsInventario[Index] != null)
        {
            EventoSlotInteraccion?.Invoke(TiposDeInteraccion.Usar, Index);
        }
    }


}
