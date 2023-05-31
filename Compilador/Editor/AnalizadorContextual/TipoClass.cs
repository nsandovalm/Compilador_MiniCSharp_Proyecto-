using System;
using System.Collections.Generic;
using Antlr.Runtime;
using IToken = Antlr4.Runtime.IToken;

namespace Editor.AnalizadorContextual
{
    public class TipoClase : Tipo
    {
        public string nombre => tok?.Text ?? string.Empty;

        public LinkedList<Tipo> ListaTipos { get; } = new LinkedList<Tipo>();

        public TipoClase(IToken token, int nivell) : base(token, nivell) { }

        public void Imprimir()
        {
            System.Diagnostics.Debug.WriteLine($"********************* CLASE {nombre} ***********************");
            foreach (var parametro in ListaTipos)
            {
                switch (parametro)
                {
                    /*case CustomType customType:
                        System.Diagnostics.Debug.WriteLine($"Type: {customType.Tipo} - TypeOf {customType.TypeOf} - Token {customType.Token.Text}");
                        System.Diagnostics.Debug.WriteLine($"Level: {customType.Level}");
                        break;*/
                    case TipoArray tipoArray:
                        System.Diagnostics.Debug.WriteLine($"El tamaño es: {tipoArray.tamanno} / Los tipos que tiene son: {tipoArray.Tipo} / El token es: {tipoArray.tok.Text}");
                        System.Diagnostics.Debug.WriteLine($"El nivel es: {tipoArray.nivel}");
                        break;
                    case TiposBasicos tiposBasico:
                        System.Diagnostics.Debug.WriteLine($"El tipo es: {tiposBasico.tipo} / El nivel es: {tiposBasico.nivel} / El token es: {tiposBasico.tok.Text}");
                        break;
                }
            }
            System.Diagnostics.Debug.WriteLine("************************ FIN ***************************");
        }
        public override string GetTipo() => "Class";
    }

}