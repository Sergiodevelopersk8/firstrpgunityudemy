using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Inventario
{

    public enum TiposDeItems
    {
        Armas,
        pociones,
        Pergaminos,
        Ingredientes,
        Tesoros
    }

    public class InventarioItem : ScriptableObject
    {
        [Header("Parametros")] 
        public string Id;
        public string Nombre;
        public Sprite Icono;
        [TextArea] public string Descripcion;

        [Header("Informacion")]
        public TiposDeItems Tipo;
        public bool EsConsumible;
        public bool EsAcumulable;
        public int AcumulacionMax;

        [HideInInspector]public int Cantidad;


    }
}
