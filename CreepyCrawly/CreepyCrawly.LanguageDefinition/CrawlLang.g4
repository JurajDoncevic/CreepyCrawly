grammar CrawlLang;

/* Lexical rules */
CLICK_KEYWORD           :   'CLICK';
WAIT_KEYWORD            :   'WAIT';
EXTRACT_KEYWORD         :   'EXTRACT';
INPUT_KEYWORD           :   'INPUT';
WAIT_LOAD_KEYWORD       :   'WAIT_LOAD';
SELECT_KEYWORD          :   'SELECT';
EXTRACT_SCRIPT_KEYWORD  :   'EXTRACT SCRIPT';
ON_KEYWORD              :   'ON';
ROOT_KEYWORD            :   'ROOT';
DO_KEYWORD              :   'DO';
FOREACH_KEYWORD         :   'FOREACH';
SUBMIT_KEYWORD			:	'SUBMIT';

TEXT             :   '\''(.)+?'\'';
POSITIVE_INTEGER :   ([1-9]+[0-9]*|[0]);

SEMICOL     :   ';';
LPAREN      :   '(';
RPAREN      :   ')';
LANGLE      :   '[';
RANGLE      :   ']';
LCURLY      :   '{';
RCURLY      :   '}';

WS  :   [ \r\t\u000C\n]+ -> skip;

/* Syntax rules */
text_value      : LPAREN TEXT RPAREN
                ;

selector        : TEXT
                ;

www_url			: TEXT
				;

wait_amount     : POSITIVE_INTEGER
                ;

select_index    : POSITIVE_INTEGER
                ;

click_command   :   CLICK_KEYWORD selector SEMICOL
                ;

wait_command    :   WAIT_KEYWORD wait_amount SEMICOL
                ;

extract_command :   EXTRACT_KEYWORD selector SEMICOL
                ;

input_command   :   INPUT_KEYWORD selector text_value SEMICOL
                ;

wait_load_command   :   WAIT_LOAD_KEYWORD selector wait_amount SEMICOL
                    ;

select_command  :   SELECT_KEYWORD selector LANGLE select_index RANGLE SEMICOL
                ;

extract_script_command  :   EXTRACT_SCRIPT_KEYWORD selector SEMICOL
                        ;

submit_command	: SUBMIT_KEYWORD selector SEMICOL
				;

foreach_command :   FOREACH_KEYWORD selector command_block
                ;

simple_command  :   (click_command|wait_command|extract_command|input_command|wait_load_command|select_command|submit_command)
                ;

complex_command :   (foreach_command)
                ;

command_block   :   DO_KEYWORD LCURLY (simple_command|complex_command)* RCURLY
                ;

on_root_command     :    ON_KEYWORD ROOT_KEYWORD www_url command_block
                    ;

prog    :   on_root_command
        ;