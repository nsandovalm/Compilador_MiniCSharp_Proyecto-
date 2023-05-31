using System;
using Generated;
using Antlr4.Runtime;
using Antlr4.Runtime.Misc;
using System.Collections.Generic;
using System.IO;

public class MyErrorListenerScanner : IAntlrErrorListener<int> {
    public ArrayList<string> ErrorMsgs { get; } = new ArrayList<string>();

    public void SyntaxError(TextWriter output, IRecognizer recognizer, int offendingSymbol, int line, int charPositionInLine, string msg, RecognitionException e) 
    {
        if (recognizer is Scanner)
            ErrorMsgs.Add($"Error - Scanner -- Linea {line}:{charPositionInLine+1} {msg}");
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

    public MyErrorListenerScanner()
    {
        this.ErrorMsgs = new ArrayList<string>();
    }
}