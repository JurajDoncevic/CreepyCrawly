grammar CrawlLang;

/* Lexical rules */
CLICK_KEYWORD           :   'CLICK';
WAIT_MS_KEYWORD         :   'WAIT_MS';
EXTRACT_TEXT_KEYWORD	:   'EXTRACT_TEXT';
INPUT_KEYWORD           :   'INPUT';
WAIT_LOAD_KEYWORD       :   'WAIT_LOAD';
SELECT_KEYWORD          :   'SELECT';
EXTRACT_SCRIPT_KEYWORD  :   'EXTRACT_SCRIPT';
ON_KEYWORD              :   'ON';
ROOT_KEYWORD            :   'ROOT';
DO_KEYWORD              :   'DO';
FOREACH_KEYWORD         :   'FOREACH';
SUBMIT_KEYWORD			:	'SUBMIT';
EXTRACT_IMAGE_KEYWORD	:	'EXTRACT_IMAGE';
GOTO_SRC_KEYWORD		:	'GOTO_SRC';

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

wait_ms_command    :   WAIT_MS_KEYWORD wait_amount SEMICOL
                ;

extract_text_command :   EXTRACT_TEXT_KEYWORD selector SEMICOL
                ;

extract_image_command	:	EXTRACT_IMAGE_KEYWORD selector SEMICOL
						;

goto_src_command	:	GOTO_SRC_KEYWORD selector SEMICOL
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

simple_command  :   (click_command|wait_ms_command|extract_text_command|goto_src_command|extract_image_command|input_command|wait_load_command|select_command|submit_command)
                ;

complex_command :   (foreach_command)
                ;

command_block   :   DO_KEYWORD LCURLY (simple_command|complex_command)* RCURLY
                ;

on_root_command     :    ON_KEYWORD ROOT_KEYWORD www_url command_block
                    ;

prog    :   on_root_command
        ;