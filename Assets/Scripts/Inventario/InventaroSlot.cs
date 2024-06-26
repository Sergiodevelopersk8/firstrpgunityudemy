using Assets.Scripts.Inventario;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class InventaroSlot : MonoBehaviour
{
  
    [SerializeField] private Image itemIcono;
    [SerializeField] private GameObject fondoCantidad;
    [SerializeField] private TextMeshProUGUI CantidadTMP;

    public int Index { set; get; }

    
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


}
