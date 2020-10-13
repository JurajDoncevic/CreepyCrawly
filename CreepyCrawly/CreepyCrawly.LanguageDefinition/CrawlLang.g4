grammar CrawlLang;

/* LEXICAL RULES */
CLICK_KEYWORD           :   'CLICK';
WAIT_MS_KEYWORD         :   'WAIT_MS';
EXTRACT_TEXT_KEYWORD	:   'EXTRACT_TEXT';
EXTRACT_TITLE_KEYWORD	:	'EXTRACT_TITLE';
INPUT_KEYWORD           :   'INPUT';
WAIT_FOR_KEYWORD		:   'WAIT_FOR';
SELECT_KEYWORD          :   'SELECT';
EXTRACT_SCRIPT_KEYWORD  :   'EXTRACT_SCRIPT';
ON_KEYWORD              :   'ON';
ROOT_KEYWORD            :   'ROOT';
DO_KEYWORD              :   'DO';
FOREACH_SELECT_KEYWORD  :   'FOREACH_SELECT';
FOREACH_CLICK_KEYWORD	:	'FOREACH_CLICK';
FOREACH_HREF_KEYWORD	:	'FOREACH_HREF';
EXTRACT_ALL_HREFS_KEYWORD   :   'EXTRACT_ALL_HREFS';
CLICK_EACH_KEYWORD      :   'CLICK_EACH';
SUBMIT_KEYWORD			:	'SUBMIT';
PUT_INNER_HTML_KEYWORD  :   'PUT_INNER_HTML';
EXTRACT_IMAGE_KEYWORD	:	'EXTRACT_IMAGE';
EXTRACT_ALL_IMAGES_KEYWORD	:	'EXTRACT_ALL_IMAGES';
EXTRACT_TO_CSV_KEYWORD	:	'EXTRACT_TO_CSV';
EXTRACT_HREF_KEYWORD    :   'EXTRACT_HREF';
GOTO_SRC_KEYWORD		:	'GOTO_SRC';
GOTO_CLICK_KEYWORD		:	'GOTO_CLICK';
WHILE_CLICK_KEYWORD		:	'WHILE_CLICK';
WITH_KEYWORD            :   'WITH';
REGEX_KEYWORD           :   'REGEX';
/*composite extract command*/
EXTRACT_KEYWORD         :   'EXTRACT';
TEXT_KEYWORD            :   'TEXT';
TITLE_KEYWORD           :   'TITLE';
HREF_KEYWORD            :   'HREF';
SCRIPT_KEYWORD          :   'SCRIPT';
IMAGE_KEYWORD           :   'IMAGE';

TEXT             :  '\''(.)+?'\'';
POSITIVE_INTEGER :  ([1-9]+[0-9]*|[0]);
REGEX            :  'r\''(.)+?'\'';

COMMA		:	',';
SEMICOL     :   ';';
LPAREN      :   '(';
RPAREN      :   ')';
LANGLE      :   '[';
RANGLE      :   ']';
LCURLY      :   '{';
RCURLY      :   '}';

WS  :   [ \r\t\u000C\n]+ -> skip;
COMMENT	:	'/*'(.)*'*/' -> skip;

/* SYNTAX RULES */
text_value      : LPAREN TEXT RPAREN
                ;

selector        : TEXT
                ;

www_url			: TEXT
				;

regex           : REGEX
                ;

/* simple commands */
select_index    : POSITIVE_INTEGER
                ;

click_command   :   CLICK_KEYWORD selector SEMICOL
                ;

/* wait commands */
wait_amount     : POSITIVE_INTEGER
                ;

wait_ms_command	:   WAIT_MS_KEYWORD wait_amount SEMICOL
                ;

wait_for_command   :   WAIT_FOR_KEYWORD selector wait_amount SEMICOL
                    ;

/* extract commands */ 
extract_text_command	:   EXTRACT_TEXT_KEYWORD selector (WITH_KEYWORD REGEX_KEYWORD regex)? SEMICOL
						;

extract_image_command	:	EXTRACT_IMAGE_KEYWORD selector SEMICOL
						;

extract_title_command	:	EXTRACT_TITLE_KEYWORD SEMICOL
				        ;

extract_script_command  :   EXTRACT_SCRIPT_KEYWORD selector SEMICOL
                        ;

extract_all_images_command	:	EXTRACT_ALL_IMAGES_KEYWORD selector SEMICOL
							;

extract_to_csv_command	:	EXTRACT_TO_CSV_KEYWORD selector(COMMA selector)* SEMICOL
						;

extract_href_command    :   EXTRACT_HREF_KEYWORD selector SEMICOL
                        ;

extract_all_hrefs_command   :   EXTRACT_ALL_HREFS_KEYWORD selector SEMICOL
                            ;

input_command   :   INPUT_KEYWORD selector text_value SEMICOL
                ;

put_inner_html_command  : PUT_INNER_HTML_KEYWORD selector text_value SEMICOL
                ;

select_command  :   SELECT_KEYWORD selector LANGLE select_index RANGLE SEMICOL
                ;

submit_command	: SUBMIT_KEYWORD selector SEMICOL
				;

click_each_command   : CLICK_EACH_KEYWORD selector command_block
                    ;

goto_src_command	:	GOTO_SRC_KEYWORD selector command_block
					;

goto_click_command	:	GOTO_CLICK_KEYWORD selector command_block
					;

foreach_click_command	:	FOREACH_CLICK_KEYWORD selector command_block
						;

foreach_href_command	:	FOREACH_HREF_KEYWORD selector command_block
						;

foreach_select_command  :   FOREACH_SELECT_KEYWORD selector command_block
                        ;

while_click_command		:	WHILE_CLICK_KEYWORD	selector command_block
						;

do_while_click_command	:	command_block WHILE_CLICK_KEYWORD selector SEMICOL
						;
/*composite extract command*/
partial_extract_text_command    :   TEXT_KEYWORD selector (WITH_KEYWORD REGEX_KEYWORD regex)?
                                ;

partial_extract_title_command   :   TITLE_KEYWORD
                                ;

partial_extract_image_command   :   IMAGE_KEYWORD selector
                                ;

partial_extract_script_command  :   SCRIPT_KEYWORD selector
                                ;

partial_extract_href_command    :   HREF_KEYWORD selector
                                ;

partial_command                 : (partial_extract_text_command|partial_extract_title_command|partial_extract_image_command|partial_extract_script_command|partial_extract_href_command)
                                ;

composite_extract_command   :   EXTRACT_KEYWORD partial_command (COMMA partial_command)* SEMICOL
                            ;

simple_command  :   (put_inner_html_command|click_command|wait_ms_command|extract_text_command|extract_href_command|extract_all_hrefs_command|extract_all_images_command|extract_title_command|extract_image_command|extract_to_csv_command|input_command|wait_for_command|select_command|submit_command|composite_extract_command)
                ;

complex_command :   (click_each_command|foreach_select_command|foreach_click_command|foreach_href_command|goto_src_command|goto_click_command|while_click_command|do_while_click_command)
                ;

command_block   :   DO_KEYWORD LCURLY (simple_command|complex_command)* RCURLY
                ;

on_root_command     :    ON_KEYWORD ROOT_KEYWORD www_url command_block
                    ;

prog    :   on_root_command
        ;