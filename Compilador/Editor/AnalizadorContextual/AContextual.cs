using System;
using Generated;
using Antlr4.Runtime;
using Antlr4.Runtime.Misc;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using Editor.AnalizadorContextual;


namespace Editor
{
    public class AContextual : MiniCSharpParserBaseVisitor<Object>
    {
        private TablaSimbolos laTabla;

        public AContextual()
        {
            this.laTabla = new TablaSimbolos();
        }
        
        // Métodos auxiliares para mostrar errores
        private string ShowErrorPosition(IToken t)
        {
            return " Fila: " + t.Line + " , Columna: " + (t.Column + 1);
        }
        
        private void ReportError(string message, IToken location)
        {
            Console.Error.WriteLine("Error en " + ShowErrorPosition(location) + " : " + message);
        }

        // Visit para la regla programAST
        public override object VisitProgramAST(MiniCSharpParser.ProgramASTContext context)
        {
            
            foreach (var usingDirective in context.@using())
            {
                Visit(usingDirective);
            }

            foreach (var classDecl in context.classDecl())
            {
                Visit(classDecl);
            }
            foreach (var varDecl in context.varDecl())
            {
                Visit(varDecl);
            }

            foreach (var varDecl in context.varDecl())
            {
                Visit(varDecl);
            }

            return null;
            
            
        }

        public override object VisitUsingAST(MiniCSharpParser.UsingASTContext context)
        {
            return null;
        }

        public override object VisitVarDeclAST(MiniCSharpParser.VarDeclASTContext context)
        {
            /*Tipo tip;
            IToken t = context.ID().GetEnumerator();
            
            string type = context.type().GetText();
            string id = context.ID().GetText();
            if (type.Equals("string")){
                // tip = new TiposBasicos(t,(Enum)(String) , 0); 
             }
            else if(type.Equals("int"))
            {
                
            }else if(type.Equals("char"))
            {
                
            }
            else if(type.Equals("double"))
            {
                
            }
            else if(type.Equals("bool"))
            {
                
            }
            else if(type.Equals("float"))
            {
                
            }
            // Verificar si la variable ya fue declarada
            if (laTabla.estaAlmacenado(id).Equals(1))
            {
                Console.WriteLine($"Error: la variable '{id}' ya ha sido declarada anteriormente");
                return null;
            }

            // Agregar la variable a la tabla de símbolos
            laTabla.Insertar(tip);*/
            Visit(context.type());

            return null;
        }

        public override object VisitClassDeclAST(MiniCSharpParser.ClassDeclASTContext context)
        {
            laTabla.OpenScope();
            foreach (var child in context.varDecl())
            {
                Visit(child);
            }
            laTabla.CloseScope();
            return null;
        }

        //********************
        public override object VisitMethodDeclAST(MiniCSharpParser.MethodDeclASTContext context)
        {
            
            string id = context.ID().GetText();
            string returnType = context.type()?.GetText() ?? "void";

            // Verificar si el método ya fue declarado
            if (laTabla.estaAlmacenado(id).Equals(1))
            {
                Console.WriteLine($"Error: el método '{id}' ya ha sido declarado anteriormente en {ShowErrorPosition(context.ID().Symbol)}");
                return null;
            }

            Tipo t;
            if (id == "int" || id == "string" || id == "float" || id == "bool" || id == "char" || id == "double")
            {
                t = new TiposBasicos();
                laTabla.Insertar(t);
            }
            if (id == "class")
            {
                t = new TipoClase(null, 0);
                laTabla.Insertar(t);
            }
            if (id == "array")
            {
                t = new TipoArray();
                laTabla.Insertar(t);
            }
            if (id == "void")
            {
                t = new TipoMetodo(null,0,0,"",null);
                laTabla.Insertar(t);
            }

            // Visitar el cuerpo del método
            Visit(context.block());

            return null;
        }


        public override object VisitFormParsAST(MiniCSharpParser.FormParsASTContext context)
        {
            Visit(context.type(0));
            foreach (var parameter in context.type())
            {
                var type = Visit(parameter);
                if (type == null)
                {
                    Console.WriteLine("Error de tipo en la declaración del parámetro " + parameter.GetText());
                    return null;
                }
                if (laTabla.estaAlmacenado(parameter.GetText()) != 0)
                {
                    Console.WriteLine("El parámetro " + parameter.GetText() + " ya ha sido declarado" );
                }
            }
            return null; 
        }

        public override object VisitTypeAST(MiniCSharpParser.TypeASTContext context)
        {
            Tipo tipo;

            if (context.INT() != null)
            {
                tipo = new TiposBasicos();
            }
            else if (context.DOUBLE() != null)
            {
                tipo = new TiposBasicos();
            }
            else if (context.CHAR() != null)
            {
                tipo = new TiposBasicos();
            }
            else if (context.STRING() != null)
            {
                tipo = new TiposBasicos();
            }
            else if (context.FLOAT() != null)
            {
                tipo = new TiposBasicos();
            }
            else if (context.BOOLEAN() != null)
            {
                tipo = new TiposBasicos();
            }
            else
            {
                // Este caso no debería ocurrir nunca.
                return  null;
            }
            return tipo; 
        }

        public override object VisitDesignatorStatementAST(MiniCSharpParser.DesignatorStatementASTContext context)
        {
            string id = context.GetText();
            
            Visit(context.designator());
            // Verificar si la variable o método existe en la tabla de símbolos
            if (laTabla.estaAlmacenado(id).Equals(0))
            {
                Console.WriteLine($"Error: el identificador '{id}' no ha sido declarado anteriormente");
                return null;
            }

            return null; 
        }

        public override object VisitIfStatementAST(MiniCSharpParser.IfStatementASTContext context)
        {
            Visit(context.condition());
            Visit(context.statement(0));

            if (context.statement().Length > 1)
            {
                Visit(context.statement(1));
            }
            return null; 
        }

        public override object VisitForStatementAST(MiniCSharpParser.ForStatementASTContext context)
        {
            Visit(context.expr());
            Visit(context.condition());
            Visit(context.statement(0));
            Visit(context.statement(0));
            return null; 
        }

        public override object VisitWhereStatementAST(MiniCSharpParser.WhereStatementASTContext context)
        {
            Visit(context.condition());
            Visit(context.statement());
            return null; 
        }

        public override object VisitBreakStatementAST(MiniCSharpParser.BreakStatementASTContext context)
        {
            return null; 
        }

        public override object VisitReturnStatementAST(MiniCSharpParser.ReturnStatementASTContext context)
        {
            Visit(context.expr());

            return null;
        }

        public override object VisitReadStatementAST(MiniCSharpParser.ReadStatementASTContext context)
        {
            Visit(context.designator());
            return null; 
        }

        public override object VisitWriteStatementAST(MiniCSharpParser.WriteStatementASTContext context)
        {
            Visit(context.expr());
            return null; 
        }

        public override object VisitBlockStatementAST(MiniCSharpParser.BlockStatementASTContext context)
        {
            Visit(context.block());
            return null; 
        }

        public override object VisitPuntoComaStatementAST(MiniCSharpParser.PuntoComaStatementASTContext context)
        {
            return null; 
        }

        public override object VisitBlockAST(MiniCSharpParser.BlockASTContext context)
        {

            // Visitamos las declaraciones y sentencias del bloque.
            foreach (var sentencia in context.children)
            {
                Visit(sentencia);
            }
            
            return null; 
        }

        public override object VisitActParsAST(MiniCSharpParser.ActParsASTContext context)
        {
            foreach (var sentencia in context.expr())
            {
                Visit(sentencia);
            }
            return null; 
        }

        public override object VisitConditionAST(MiniCSharpParser.ConditionASTContext context)
        {
            foreach (var sentencia in context.condTerm())
            {
                Visit(sentencia);
            }
            return null; 
        }

        public override object VisitCondTermAST(MiniCSharpParser.CondTermASTContext context)
        {
            foreach (var sentencia in context.condFact())
            {
                Visit(sentencia);
            }
            return null; 
        }

        public override object VisitCondFactAST(MiniCSharpParser.CondFactASTContext context)
        {
            Visit(context.expr(0));
            Visit(context.relop());
            Visit(context.expr(0));
            return null; 
        }

        public override object VisitCastAST(MiniCSharpParser.CastASTContext context)
        {
            Visit(context.type());
            return null; 
        }

        public override object VisitExprAST(MiniCSharpParser.ExprASTContext context)
        {
            Visit(context.cast());
            Visit(context.term(0));
            foreach (var sentencia in context.addop())
            {
                Visit(sentencia);
            }
            foreach (var sentencia in context.term())
            {
                Visit(sentencia);
            }
            return null; 
        }

        public override object VisitTermAST(MiniCSharpParser.TermASTContext context)
        {
            Visit(context.factor(0));
            foreach (var sentencia in context.mulop())
            {
                Visit(sentencia);
            }
            foreach (var sentencia in context.factor())
            {
                Visit(sentencia);
            }
            return null; 
        }

        public override object VisitDesignatorFactorAST(MiniCSharpParser.DesignatorFactorASTContext context)
        {
            Visit(context.designator());
            Visit(context.actPars());
            return null; 
        }

        public override object VisitNumFactorAST(MiniCSharpParser.NumFactorASTContext context)
        {
            VisitNumFactorAST(context);
            return null; 
        }

        public override object VisitTrueFalseFactorAST(MiniCSharpParser.TrueFalseFactorASTContext context)
        {
            VisitTrueFalseFactorAST(context);
            return null; 
        }

        public override object VisitNewFactorAST(MiniCSharpParser.NewFactorASTContext context)
        {
            return null; 
        }

        public override object VisitExprFactorAST(MiniCSharpParser.ExprFactorASTContext context)
        {
            Visit(context.expr());
            return null; 
        }

        public override object VisitDesignatorAST(MiniCSharpParser.DesignatorASTContext context)
        {
            var nombreVariable = context.GetText();

            if (!laTabla.estaAlmacenado(nombreVariable).Equals(1))
            {
                Console.WriteLine($"La variable {nombreVariable} no ha sido declarada en la línea {context.Start.Line}:{context.Start.Column}.");
            }
            return null;
        }
    }
}