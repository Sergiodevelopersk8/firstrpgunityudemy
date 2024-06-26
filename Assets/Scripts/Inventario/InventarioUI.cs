using Assets.Scripts.Inventario;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class InventarioUI : Singleton<InventarioUI>
{

    [Header("Panel Inventario Descripcion")]
    [SerializeField] private GameObject panelInventarioDescripcion;
    [SerializeField] private Image itemIcono;
    [SerializeField] private TextMeshProUGUI itemNombre;
    [SerializeField] private TextMeshProUGUI itemDescripcion;
    
    
    [SerializeField] private InventaroSlot slotPrefab;
    [SerializeField] private Transform contenedor;

    
    public int IndexSlotInicialPorMover { get; private set; } 
    public InventaroSlot SlotSeleccionado { get; private set;}



    List<InventaroSlot> slotsDisponibles  = new List<InventaroSlot> (); 
    void Start()
    {
        InicializarInventario();
        IndexSlotInicialPorMover = -1;
    }


    private void Update()
    {
        ActualizarSlotSeleccionado();
        if (Input.GetKeyDown(KeyCode.M))
        {
            if (SlotSeleccionado != null) {

                IndexSlotInicialPorMover = SlotSeleccionado.Index;
            
            }

        }
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

    private void ActualizarSlotSeleccionado()
    {
        GameObject goSeleccionado = EventSystem.current.currentSelectedGameObject;
        if(goSeleccionado== null)
        {
            return;
        }

        InventaroSlot slot = goSeleccionado.GetComponent<InventaroSlot>();
        if(slot != null)
        {
            SlotSeleccionado = slot;
        }



    }


    public void DibujarItemEnInventario(InventarioItem itemPorAnadir, int cantidad, int itemIndex)
    {
        InventaroSlot slot = slotsDisponibles[itemIndex];

        if (itemPorAnadir != null)
        {
            slot.ActivarSloutUI(true);
            slot.ActualizarSlot(itemPorAnadir, cantidad);
        }
        else
        {
            slot.ActivarSloutUI(false);
        }
    }



    private void ActualizarInventarioDescripcion(int index)
    {
        if(Inventario.Instance.ItemsInventario[index] != null)
        {
            itemIcono.sprite = Inventario.Instance.ItemsInventario[index].Icono;
            itemNombre.text = Inventario.Instance.ItemsInventario[index].Nombre;
            itemDescripcion.text = Inventario.Instance.ItemsInventario[index].Descripcion;
            panelInventarioDescripcion.SetActive(true);
        }
        else
        {
            panelInventarioDescripcion.SetActive(false);
        }
    }


    public void UsarItem()
    {
        if(SlotSeleccionado != null)
        {
            SlotSeleccionado.SlotUsarItem();
            SlotSeleccionado.SeleccionarSlot();
        }
    }





    #region Evento

    private void SlotInteraccionRespuesta(TiposDeInteraccion tipo, int index)
    {
        if (tipo == TiposDeInteraccion.Click)
        {
            ActualizarInventarioDescripcion(index);
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
