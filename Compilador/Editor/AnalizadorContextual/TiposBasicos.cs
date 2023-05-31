using System;
using Antlr.Runtime;
using IToken = Antlr4.Runtime.IToken;

namespace Editor.AnalizadorContextual
{
    public class TiposBasicos : Tipo
    {
        
        public enum TiposDisponibles { Int, Char, String, Double, Bool, Float, NoHay}
        public readonly TiposDisponibles tipo;
        
        //Se envia a la clase padre los datos y se asignan a tipo el tipo que se pasa por parametro
        public TiposBasicos(IToken token, TiposDisponibles tipos, int nivel) : base(token, nivel) =>
            this.tipo = tipos;
        
        public TiposBasicos() : base(null,-1) { } //Si no se encuentran

        //Comprobar si el tipo que viene por parametro se encuentra en los tipos disponibles si no se indica que no hay
        public static TiposDisponibles ParseTipo(string tipoComprobar) =>
            Enum.TryParse(tipoComprobar, true, out TiposDisponibles tipo) ? tipo : TiposDisponibles.NoHay;
        
        //Se retorna el tipo que se encuentra 
        public override string GetTipo() => tipo.ToString();
    }
    /*private string Int = "int";
        private string String = "string";
        private string Char = "char";
        private string Bool = "bool";
        private string Double = "double";
        //private string String = "string";*/
}