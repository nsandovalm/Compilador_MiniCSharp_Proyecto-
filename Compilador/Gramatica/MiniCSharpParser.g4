parser grammar MiniCSharpParser;


options {
  tokenVocab = Scanner;
}


program     : using* CLASS ID LLAVE_IZQUIERDA (varDecl | classDecl | methodDecl)* LLAVE_DERECHA EOF                                            #programAST;

using       : USING ID PUNTO_Y_COMA                                                                                                            #usingAST;                

varDecl     : type ID (COMA ID)* PUNTO_Y_COMA                                                                                                  #varDeclAST;            

classDecl   : CLASS ID LLAVE_IZQUIERDA (varDecl)* LLAVE_DERECHA                                                                                #classDeclAST;                   

methodDecl  : (type | VOID) ID PARENTESIS_IZQUIERDO (formPars)? PARENTESIS_DERECHO block                                                       #methodDeclAST;             

formPars    : type ID (COMA type ID)*                                                                                                          #formParsAST;           

//type        : ID (CORCHETE_IZQUIERDO CORCHETE_DERECHO)?                                                                                        #typeAST;
type : (BOOLEAN | INT | CHAR | STRING | DOUBLE | FLOAT | USING | ID) (CORCHETE_IZQUIERDO CORCHETE_DERECHO)?                                    #typeAST;

statement   : designator (IGUAL expr | PARENTESIS_IZQUIERDO (actPars)? PARENTESIS_DERECHO | MAS_Y_MAS | MENOS_Y_MENOS) PUNTO_Y_COMA           #designatorStatementAST  
            | IF PARENTESIS_IZQUIERDO condition PARENTESIS_DERECHO statement (ELSE statement)?                                                #ifStatementAST
            | FOR PARENTESIS_IZQUIERDO expr PUNTO_Y_COMA (condition)? PUNTO_Y_COMA (statement)? PARENTESIS_DERECHO statement                  #forStatementAST
            | WHILE PARENTESIS_IZQUIERDO condition PARENTESIS_DERECHO statement                                                               #whereStatementAST
            | BREAK PUNTO_Y_COMA                                                                                                              #breakStatementAST     
            | RETURN (expr)? PUNTO_Y_COMA                                                                                                     #returnStatementAST
            | READ PARENTESIS_IZQUIERDO designator PARENTESIS_DERECHO PUNTO_Y_COMA                                                            #readStatementAST
            | WRITE PARENTESIS_IZQUIERDO expr (COMA NUM)? PARENTESIS_DERECHO PUNTO_Y_COMA                                                     #writeStatementAST 
            | block                                                                                                                           #blockStatementAST
            | PUNTO_Y_COMA                                                                                                                    #puntoComaStatementAST //
            ;
            
block       : LLAVE_IZQUIERDA (varDecl | statement)* LLAVE_DERECHA                                                                          #blockAST;
            
actPars     : expr (COMA expr)*                                                                                                             #actParsAST;            

condition   : condTerm (OR condTerm)*                                                                                                       #conditionAST;

condTerm    : condFact (AND condFact)*                                                                                                      #condTermAST;

condFact    : expr relop expr                                                                                                               #condFactAST;                      

cast        : PARENTESIS_IZQUIERDO type PARENTESIS_DERECHO                                                                                  #castAST;

expr        : (MENOS)? (cast)? term (addop term)*                                                                                           #exprAST;

term        : factor (mulop factor)*                                                                                                        #termAST;

factor      : designator (PARENTESIS_IZQUIERDO (actPars)? PARENTESIS_DERECHO)?                                                              #designatorFactorAST  //
            | NUM                                                                                                                           #numFactorAST         //
            | (TRUE | FALSE)                                                                                                                #trueFalseFactorAST   //
            | NEW ID                                                                                                                        #newFactorAST         // 
            | PARENTESIS_IZQUIERDO expr PARENTESIS_DERECHO                                                                                  #exprFactorAST       
            ;

designator  : ID (PUNTO ID | CORCHETE_IZQUIERDO expr CORCHETE_DERECHO)*                                                                    #designatorAST;


relop       : IGUAL_QUE                                                                                                                    //#igualRelopAST  
            |DISTINTO_QUE                                                                                                                  //#distintoRelopAST
            |MAS                                                                                                                           //#masRelopAST 
            |MAYOR_IGUAL                                                                                                                   //#mayorRelopAST
            |MENOS                                                                                                                         //#menosRelopAST
            |MENOR_IGUAL                                                                                                                   //#menorRelopAST    
            |MENOR_QUE
            |MAYOR_QUE
            |SIGNO_EXCLAMACION
            |IGUAL
            ;

addop       : MAS
            |MENOS
             ;
       
mulop       : MULTIPLICACION
            |DIVISION
            |PORCENTAJE
            ;       
       
       
     