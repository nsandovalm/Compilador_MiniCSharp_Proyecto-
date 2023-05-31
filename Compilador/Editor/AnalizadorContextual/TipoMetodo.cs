using System;
using System.Collections.Generic;
using System.Diagnostics;
using Antlr.Runtime;
using IToken = Antlr4.Runtime.IToken;

namespace Editor.AnalizadorContextual
{
    public class TipoMetodo : Tipo
    {
        public string Tipo => "Metodo";
        public int cantParametros { get; }
        public string tipoRetornar { get; }
        public LinkedList<Tipo> parametros { get; }

        public TipoMetodo(IToken token, int nivel, int cantParametros, string tipoRetornar, LinkedList<Tipo> parametros) : base(token, nivel)
        {
            this.tipoRetornar = tipoRetornar;
            this.cantParametros = cantParametros;
            this.parametros = parametros;
        }

        public void Imprimir()
        {
            Debug.WriteLine("************************* Metodos *************************");
            Debug.WriteLine($"Metodo de tipo: {tok.Text} / El nivel es: {nivel}");
            Debug.WriteLine($"Cantidad de parametros: {cantParametros}");
            Debug.WriteLine($"Tipo a retornar: {tipoRetornar}");
            Debug.WriteLine("Los parametros son: ");

            //Imprimir los parametros que se encuentran en el metodo
            foreach (var parametross in parametros)
            {
                switch (parametross)
                {
                    //TODO case TipoCustom
                    case TipoArray tipoArray:
                        System.Diagnostics.Debug.WriteLine($"El tamaño es: {tipoArray.tamanno} / Los tipos que tiene son: {tipoArray.Tipo} / El token es: {tipoArray.tok.Text}");
                        System.Diagnostics.Debug.WriteLine($"El nivel es: {tipoArray.nivel}");
                        break;
                    case TiposBasicos tiposBasico:
                        System.Diagnostics.Debug.WriteLine($"El tipo es: {tiposBasico.tipo} / El nivel es: {tiposBasico.nivel} / El token es: {tiposBasico.tok.Text}");
                        break; 
                }
                
            }
            System.Diagnostics.Debug.WriteLine("************************* FIN *************************");
        }

        public override string GetTipo()
        {
            return tipoRetornar;
        }
    }
}