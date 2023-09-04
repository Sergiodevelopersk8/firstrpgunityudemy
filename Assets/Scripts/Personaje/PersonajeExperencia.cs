using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersonajeExperencia : MonoBehaviour
{

    [Header("Stats")] [SerializeField] private PersonajeStats stats;


    [Header("Config")] 
    [SerializeField] private int nivelMax;
    [SerializeField] private int expBase;
    [SerializeField] private int valorIncremental;



    private float expActual;
    private float expActualTemp;
    private float expRequeridaSiguienteNivel;
    // Start is called before the first frame update
    void Start()
    {
        stats.Nivel = 1;
        expRequeridaSiguienteNivel = expBase;
        stats.ExpRequeridaSiguienteNivel = expRequeridaSiguienteNivel;
        ActualizarBarraExp();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.X))
        {
            AñadirExperencia(2f);
        }
    }

    public void AñadirExperencia(float expObtenida)
    {
        if(expObtenida > 0f)
        {
            float expRestanteNuevoVivel = expRequeridaSiguienteNivel - expActualTemp; // 8 - 4 = 4

            if(expObtenida >= expRestanteNuevoVivel)
            {
                expObtenida -= expRestanteNuevoVivel;
                expActual += expObtenida;
                ActualizarNivel();
                AñadirExperencia(expObtenida);
            }
            else
            {
                expActual += expObtenida;
                expActualTemp += expObtenida;
                if(expActualTemp == expRequeridaSiguienteNivel)
                {
                    ActualizarNivel();
                }
            }
        }
        stats.ExpActual = expActual;
        ActualizarBarraExp();

    }

    private void ActualizarNivel()
    {
        if(stats.Nivel < nivelMax)
        {
            stats.Nivel++;
            expActualTemp = 0f;
            expRequeridaSiguienteNivel *= valorIncremental;
            stats.ExpRequeridaSiguienteNivel = expRequeridaSiguienteNivel;
            stats.PuntosDisponibles += 3;
        }
    }

    private void ActualizarBarraExp()
    {
        UIManager.Instance.ActualizarExpPersonaje(expActualTemp, expRequeridaSiguienteNivel);
    }


}
