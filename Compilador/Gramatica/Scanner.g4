lexer grammar Scanner;

@lexer::members {

public override void NotifyListeners(LexerNoViableAltException e){
			    this.ErrorListenerDispatch.SyntaxError(this.ErrorOutput, (IRecognizer) this, 0, TokenStartLine, this.TokenStartColumn, "Token No Identificado : '" + this.GetErrorDisplay(this.EmitEOF().InputStream.GetText(Interval.Of(this.TokenStartCharIndex, this.InputStream.Index)))  + "'", (RecognitionException) e);
			   }
} 

COMMENT : '//' ~[\r\n]* -> skip;
WS : [ \t\n\r]+ -> skip;

fragment LETTER : [a-z] | [A-Z];
fragment DIGIT : [0-9];
fragment FLOAT_LITERAL : [0-9]+ '.' [0-9]*;
//fragment CHAR_CONST : '\'' [a-zA-Z0-9] '\'';
fragment CHARR : [a-zA-Z0-9]; //AGREGADO 
fragment ESCAPE : '\\' ( 'n' | 'r' | 't' | '\\' | '\''); 

STRING_CONST : '"' ( ESCAPE | ~["\\] )* '"';
CHAR_CONST : '\'' ( ESCAPE | CHARR ) '\''; //AGREGADO



// Palabras clave
CLASS : 'class';
VOID : 'void';
IF : 'if';
ELSE : 'else';
FOR : 'for';
WHILE : 'while';
BREAK : 'break';
RETURN : 'return';
READ : 'read';
WRITE : 'write';
NEW : 'new';
TRUE : 'true';
FALSE : 'false';

// Tipos de datos
BOOLEAN : 'bool';
INT : 'int';
CHAR  : 'char';
STRING :'string';
DOUBLE : 'double';

FLOAT : 'float';
USING : 'using';

// Símbolos
LLAVE_IZQUIERDA : '{';
LLAVE_DERECHA : '}';
PARENTESIS_IZQUIERDO : '(';
PARENTESIS_DERECHO : ')';
CORCHETE_IZQUIERDO: '[';
CORCHETE_DERECHO : ']';
PUNTO_Y_COMA : ';';
COMA : ',';
PUNTO : '.';
MAS : '+';
MAS_Y_MAS : '++';
MENOS_Y_MENOS : '--';
MENOS : '-';
MULTIPLICACION : '*';
DIVISION : '/';
PORCENTAJE : '%';
IGUAL : '=';
MENOR_QUE : '<';
MAYOR_QUE : '>';
SIGNO_EXCLAMACION : '!';
SIGNO_PREGUNTA : '?';
DOS_PUNTOS : ':';

// Operadores
IGUAL_QUE : '==';
DISTINTO_QUE : '!=';
MENOR_IGUAL : '<=';
MAYOR_IGUAL : '>=';
AND : '&&';
OR : '||';

NUM : DIGIT+;
ID : LETTER (LETTER | DIGIT)*;




