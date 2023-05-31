using System.Collections.Generic;
using System;
using Antlr4.Runtime;
using Antlr4.Runtime.Misc;
using Editor.AnalizadorContextual;

//namespace AnalizadorContextual;

public class TablaSimbolos
{
    private LinkedList<object> tabla;
    private static int nivelActual;

    public TablaSimbolos()
    {
        tabla = new LinkedList<object>();
        nivelActual = -1;
    }

    public void Insertar(Tipo i)
    {
        tabla.AddFirst(i);
    }

    public Tipo Buscar(string nombre)
    {
        foreach (Tipo tip in tabla)
        {
            if (tip.Tok.Text == nombre)
            {
                return (tip);
            }
        }
        return null;
    }

    public void OpenScope()
    {
        nivelActual++;
    }

    public void CloseScope()
    {
        // Se sacan todos los ident del nivel que se está cerrando
        tabla.Remove(new Func<Tipo, bool>(n => n.nivel == nivelActual));
        nivelActual--;
    }

    public int estaAlmacenado(String iden)
    {
        foreach (Tipo en in tabla)
        {
            if (en.tok.Text == iden && en.nivel == nivelActual)
            {
                return 1;
            }
        }

        return 0;
    }
    

    public void Imprimir()
    {
        System.Diagnostics.Debug.WriteLine("----- INICIO TABLA ------");
        foreach (var item in tabla)
        {
            var s = ((Tipo)item).tok;
            var printMessage = $"Nombre {s.Text} - Nivel principal: {((Tipo)item).nivel}";
            switch (item)
            {
                case TipoMetodo m:
                    printMessage += $"Tipo: {m.tipoRetornar} - Cantidada de parametros: {m.cantParametros}";
                    break;
                case TiposBasicos b:
                    printMessage += $" - Tipo: {b.tipo}";
                    break;
                case TipoClase c:
                    printMessage += $" - Tipo: {c.nombre}";
                    break;
                case TipoArray a:
                    printMessage += $" - Tipo: {a.Tipo} - Tamaño: {a.tamanno}";
                    break;
            }
            System.Diagnostics.Debug.WriteLine(printMessage);
        }
        System.Diagnostics.Debug.WriteLine("----- FIN TABLA ------");
    }

}