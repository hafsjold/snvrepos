<?php
include 'clsBlog.php';

$objPost = new clsPost();
$objPost->initPostList();
if ($objPost->nextPostList()){
	$title = $objPost->title;
	$body = $objPost->body;
}
else {
	$body = 'Not found';
	$title = 'Not found';
}
?>
<head>
<meta http-equiv=Content-Type content="text/html; charset=utf-8">
<meta name=Generator content="Microsoft Word 11 (filtered)">
<title>Elektronisk nyhedsbrev</title>

<style>
<!--
 /* Style Definitions */
 p.MsoNormal, li.MsoNormal, div.MsoNormal
	{margin:0cm;
	margin-bottom:.0001pt;
	font-size:10.0pt;
	font-family:"Times New Roman";}
h1
	{margin:0cm;
	margin-bottom:.0001pt;
	page-break-after:avoid;
	font-size:10.0pt;
	font-family:"Times New Roman";
	font-style:italic;}

a:link, span.MsoHyperlink
	{color:blue;
	text-decoration:underline;}
a:visited, span.MsoHyperlinkFollowed
	{color:purple;
	text-decoration:underline;}
@page Section1
	{size:595.3pt 841.9pt;
	margin:3.0cm 2.0cm 3.0cm 2.0cm;}
div.Section1
	{page:Section1;}
div.artik
	{width:400;  background-color: #CFE3CC;    }
-->
</style>
</head>
<body>
<div class=Section1>

<div class=artik>
<h1><?=$title ?></h1>

<p class=MsoNormal>
  <?=$body ?>
</p>
</div>

</div>
</body>