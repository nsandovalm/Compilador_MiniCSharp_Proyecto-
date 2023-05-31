using Antlr4.Runtime;

namespace Editor.AnalizadorContextual
{
    public abstract class Tipo
    {
        public readonly IToken tok;
        public readonly int nivel;

        public Tipo(IToken tok, int nivel)
        {
            this.tok = tok;
            this.nivel = nivel;
        }

        public abstract string GetTipo();

        public IToken Tok
        {
            get { return tok; }
        }

        public int Nivel
        {
            get { return nivel; }
        }
    }

}