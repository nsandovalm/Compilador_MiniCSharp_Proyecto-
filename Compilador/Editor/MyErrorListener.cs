using System;
using Generated;
using Antlr4.Runtime;
using Antlr4.Runtime.Misc;
using System.Collections.Generic;
using System.IO;

public class MyErrorListener : BaseErrorListener {
    public ArrayList<string> ErrorMsgs { get; } = new ArrayList<string>();

    public override void SyntaxError(TextWriter output, IRecognizer recognizer, IToken offendingSymbol,
        int charPositionInLine, int i, string msg, RecognitionException recognitionException) 
    {
        if (recognizer is MiniCSharpParser)
            ErrorMsgs.Add($"Error - MiniCSharpParser -- Linea {charPositionInLine}:{i+1} {msg}");
        else
            ErrorMsgs.Add("--> Otro Error !!");
    }

    public Boolean hasErrors()
    {
        return this.ErrorMsgs.Count > 0;
    } 
    

    public override string ToString()
    {
        if (!hasErrors())
            return "0 Errores!!";

        var builder = new System.Text.StringBuilder();
        foreach (string errorMsg in ErrorMsgs)
        {
            builder.AppendLine(errorMsg);
        }

        return builder.ToString();
    }

    public MyErrorListener()
    {
        this.ErrorMsgs = new ArrayList<string>();
    }
}