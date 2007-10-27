<html>a html document</html>
<?php


$htmFileIn = dirname(__FILE__) . '/Nyhedsbrev-2_orig.htm';
$htmFileOut = dirname(__FILE__) . '/Nyhedsbrev-2_konv.htm';
@unlink($htmFileOut);
$htmlIn = implode('', file($htmFileIn));


$tidy_tmpFile = tempnam(dirname(__FILE__), 'tidy-');
$tidy_config = dirname(__FILE__) . '/Convert_to_XHTML.tdy';
$tidybinpath = '/usr/local/bin/tidy';
$tmp_result = array();
$cmd = $tidybinpath . ' -output ' . $tidy_tmpFile . ' -config ' . $tidy_config . ' ' . $htmFileIn . ' 2>&1';
exec($cmd, $tmp_result, $retCode);
$htmltidy = implode('', file($tidy_tmpFile));
@unlink($tidy_tmpFile);

//mixed preg_replace ( pattern, replacement, subject [, limit [, &count]] )
$pattern = array(
	'@<body[^>]*>@',
	'@<span[^>]*>(.*)</span[^>]*>@i',
	'@<style[^>]*>.*</style[^>]*>@si',
	'@<p[^>]*>[\s]*</p>[\s]*@',
	'@<p[^>]*>@',
	'@<div[\s]+class="Section\d">@i',
	'@(</p>)([\s]*)(<h\d>)@'
);
$replacement = array(
	'<body>',
	'$1',
	'',
	'',
	'<p>',
	'<div class="artikel">',	
	"$1$2</div>$2<div class=\"artikel\">$2$3"
);
$count = 0;
$htmlOut = preg_replace($pattern, $replacement, $htmltidy, -1, $count );

$f=fopen($htmFileOut,"w");
fputs($f,$htmlOut);
fclose($f);

?>
</body>
</html> 
