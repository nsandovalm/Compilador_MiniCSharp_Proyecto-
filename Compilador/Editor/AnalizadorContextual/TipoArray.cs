using System;
using Antlr.Runtime;
using IToken = Antlr4.Runtime.IToken;

namespace Editor.AnalizadorContextual
{
    public class TipoArray : Tipo
    {
        public enum TiposDisponibles { Int, Char, NoHay}
        public readonly TiposDisponibles tipo;
        public readonly string Tipo = "Array";
        public readonly int tamanno;


        //Se envia a la clase padre los datos y se asignan a tipo el tipo que se pasa por parametro
        public TipoArray(IToken token, TiposDisponibles tipos, int nivel, int tamanno) : base(token, nivel)
        {
            this.tipo = tipos;
            this.tamanno = tamanno;
        }

        public TipoArray() : base(null,-1) { } //Si no se encuentran

        //Comprobar si el tipo que viene por parametro se encuentra en los tipos disponibles si no se indica que no hay
        public static TiposDisponibles ParseTipo(string tipoComprobar) =>
            Enum.TryParse(tipoComprobar, true, out TiposDisponibles tipo) ? tipo : TiposDisponibles.NoHay;
        
        //Se retorna el tipo que se encuentra 
        public override string GetTipo() => tipo.ToString();
    }
}