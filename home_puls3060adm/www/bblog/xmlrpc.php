<?php
define('XMLRPC_DEBUG',TRUE);

include 'config.php';
include 'clsBlog.php';
include 'rpc.php';

$db = new clsPuls3060SQL();
$clsBlog = new clsBlog();
$clsBlog->setDB($db);

if(!is_array($xmlrpc_methods)){
	$xmlrpc_methods = array();

	$xmlrpc_methods['blogger.getUsersBlogs']  	= 'blogger_getUsersBlogs';
	$xmlrpc_methods['blogger.deletePost']		= 'blogger_deletePost';
	$xmlrpc_methods['metaWeblog.newPost']		= 'metaWeblog_newPost';
	$xmlrpc_methods['metaWeblog.getPost']		= 'metaWeblog_getPost';
	$xmlrpc_methods['metaWeblog.getCategories']	= 'metaWeblog_getCategories';
	$xmlrpc_methods['metaWeblog.getRecentPosts'] = 'metaWeblog_getRecentPosts';
	$xmlrpc_methods['metaWeblog.editPost']		= 'metaWeblog_editPost';
	$xmlrpc_methods['metaWeblog.newMediaObject']  = 'metaWeblog_newMediaObject';
}
$xmlrpc_methods['method_not_found'] = 'XMLRPC_method_not_found';

$xmlrpc_request = XMLRPC_parse($HTTP_RAW_POST_DATA);

$methodName = XMLRPC_getMethodName($xmlrpc_request);
XMLRPC_debug_print_to_file('/data/home/puls3060adm/www/bblog/debug/debug.htm');
$params = XMLRPC_getParams($xmlrpc_request);
define('WEBLOG_XMLPRPC_USERAGENT','Puls3060Agent');
if(!isset($xmlrpc_methods[$methodName])){
	$xmlrpc_methods['method_not_found']($methodName);
}else{
	//call the method
	$xmlrpc_methods[$methodName]($params);
}

XMLRPC_debug_print_to_file('/data/home/puls3060adm/www/bblog/debug/debug.htm');

// !blogger.getUsersBlogs  	= blogger_getUsersBlogs
// gets a list of the users blogs.
function blogger_getUsersBlogs ($params) {
	Global $clsBlog;
	$blogr=array();
	if($clsBlog->userauth($params[1],$params[2])) {
		$blog['url'] 		= 'http://adm.puls3060.dk/bblog/';
		$blog['blogName'] 	= 'Puls 3060 Test';
		$blog['blogid'] 	= 1;
		$blog['blogid type'] = 'string';
		$blogr[]=$blog;
		XMLRPC_response(XMLRPC_prepare($blogr),WEBLOG_XMLPRPC_USERAGENT);
	}	else {
		XMLRPC_error("301", "The username and password you entered was not accepted. Please try again.", WEBLOG_XMLRPC_USERAGENT);
	}
}

// !blogger.deletePost - delete a post
// Parameters: String appkey, String postid, String username, String password, boolean publish

function blogger_deletePost ($params) {
	global $clsBlog;
	if($clsBlog->userauth($params[2],$params[3])) {
		// password accepted
		$objPost = new clsPost();
		if ($objPost->deletePost($params[1])) {
			$XMLRPC_ReturnCode = "1";
			XMLRPC_response(XMLRPC_prepare($XMLRPC_ReturnCode),WEBLOG_XMLPRPC_USERAGENT);
		} else {
			XMLRPC_error("302", "There was an error deleting the post. Sorry...", WEBLOG_XMLRPC_USERAGENT);
		}
	} else {
		XMLRPC_error("301", "The username and password you entered was not accepted. Please try again.", WEBLOG_XMLRPC_USERAGENT);
	}
}


// !metaweblog.getRecentPosts, get recent posts - not working at the moment
// Params : blogid, username, password, numposts
function metaweblog_getRecentPosts ($params) {
	global $clsBlog;
	$numposts = $params[3];
	if($numposts < 1 || $numposts > 20) { $numposts = 20 ; }

	if($clsBlog->userauth($params[1],$params[2])) {
		// password accepted
		$objPost = new clsPost();
		$objPost->initPostList();
		while ($objPost->nextPostList()){
			$entryrt['userid'] = 1;
			$entryrt['userid type'] = 'string';
			$entryrt['postid'] = $objPost->postid;
			$entryrt['dateCreated'] = XMLRPC_convert_timestamp_to_iso8601($objPost->posttime);
			$entryrt['description']= $objPost->body;
			$entryrt['title'] 	= $objPost->title;
			$entryrt['link'] 	= 'http://adm.puls3060.dk/bblog/blog.php?postid='.$objPost->postid;
			$entriesar[] = $entryrt;
		}

		XMLRPC_response(XMLRPC_prepare($entriesar),WEBLOG_XMLPRPC_USERAGENT);

	} else {
		XMLRPC_error("301", "The username and password you entered was not accepted. Please try again.", WEBLOG_XMLRPC_USERAGENT);
	}
}


// metaWeblog.newPost		= metaWeblog_newPost
// Params :

function metaWeblog_newPost ($params) {
	Global $clsBlog;
	if($clsBlog->userauth($params[1],$params[2])) {
		// password accepted
		//$sectionid = 0; //$clsBlog->_get_section_id($params[3]['categories'][0]);

		$objPost = new clsPost();
		$objPost->initNewPost();
		$objPost->title = stripslashes($params[3]['title']);
		$objPost->body = stripslashes($params[3]['description']);
		//if($sectionid > 0){
		//	$objPost->sections = array(0=>$sectionid);
		//}
		if($objPost->addPost()) {
			$XMLRPC_ReturnCode = "$objPost->postid";
			XMLRPC_response(XMLRPC_prepare($XMLRPC_ReturnCode),WEBLOG_XMLPRPC_USERAGENT);
		} else {
			XMLRPC_error("500", "something went wrong adding entry.", WEBLOG_XMLRPC_USERAGENT);
		}
	} else {
		XMLRPC_error("301", "The username and password you entered was not accepted. Please try again.", WEBLOG_XMLRPC_USERAGENT);
	}
}

// metaWeblog.editPost		= metaWeblog_editPost
// metaWeblog.editPost (postid, username, password, struct, publish) returns true

function metaWeblog_editPost ($params) {
	global $clsBlog;

	if($clsBlog->userauth($params[1],$params[2])) { // password accepted
		$objPost = new clsPost();
		if ($objPost->readPost($params[0])) {
			$objPost->body = stripslashes($params[3]['description']);
			$objPost->title = stripslashes($params[3]['title']);
			$objPost->updatePost();
			$XMLRPC_ReturnCode = "1";
			XMLRPC_response(XMLRPC_prepare($XMLRPC_ReturnCode),WEBLOG_XMLPRPC_USERAGENT);
		}
		else {
			XMLRPC_error("500", "the entry was not found.", WEBLOG_XMLRPC_USERAGENT);
		}
	} else { // password not accepted
		XMLRPC_error("301", "The username and password you entered was not accepted. Please try again.", WEBLOG_XMLRPC_USERAGENT);
	}
}

// !metaWeblog.newMediaObject 
function metaWeblog_newMediaObject ($params) {
	Global $clsBlog;
	if($clsBlog->userauth($params[1],$params[2])) {
		// password accepted
		$newMediaObject->name = $params[3]['name'];
		$newMediaObject->type = $params[3]['type'];
		$newMediaObject->binary = base64_decode($params[3]['bits']);
		$path_parts = pathinfo($newMediaObject->name);
		$basename = $path_parts['basename'];

		$newMediaObject->filename = "/data/home/puls3060adm/www/bblog/MediaObjects/$basename";

		if (!$handle = fopen($newMediaObject->filename, 'w')) {
			// Cannot open file
			exit;
		}
		if (fwrite($handle, $newMediaObject->binary) === FALSE) {
			// Cannot write to file
			exit;
		}
		fclose($handle);

		$entry['url']  = "http://adm.puls3060.dk/bblog/MediaObjects/$basename";
		XMLRPC_response(XMLRPC_prepare($entry),WEBLOG_XMLPRPC_USERAGENT);
	} else {
		XMLRPC_error("301", "The username and password you entered was not accepted. Please try again.", WEBLOG_XMLRPC_USERAGENT);
	}
}


// -------------------------------------------------------------------
// metaWeblog.getPost'	= metaWeblog_getPost
// Params: String postid, String username, String password
/*
Return value: on success, struct containing
String userid,
ISO.8601 dateCreated,
String postid,
String description,
String title,
String link,
String permaLink,
on failure, fault
*/
function metaWeblog_getPost ($params) {
	global  $clsBlog;
	if ($clsBlog->userauth($params[1],$params[2])) {
		$objPost = new clsPost();
		if ($objPost->readPost($params[0])) {
			$entryrt['userid'] = 1;
			$entryrt['userid type'] = 'string';
			$entryrt['postid'] = $objPost->postid;
			$entryrt['dateCreated'] = XMLRPC_convert_timestamp_to_iso8601($objPost->posttime);
			$entryrt['description']= $objPost->body;
			$entryrt['title'] 	= $objPost->title;
			$entryrt['link'] 	= 'http://adm.puls3060.dk/bblog/blog.php?postid='.$objPost->postid;
			$entriesar[] = $entryrt;
			XMLRPC_response(XMLRPC_prepare($entry),WEBLOG_XMLPRPC_USERAGENT);
		}
		else {
			XMLRPC_error("500", "the entry was not found.", WEBLOG_XMLRPC_USERAGENT);
		}
	} else {
		XMLRPC_error("301", "The username and password you entered was not accepted. Please try again.", WEBLOG_XMLRPC_USERAGENT);
	}
}

// !metaWeblog.getCategories get SECTIONS, we call them SECTIONS!
function metaWeblog_getCategories ($params) {
	global  $clsBlog;
	if($clsBlog->userauth($params[1],$params[2])) {
		// password accepted
		$blogcats = array();
		$blogname = 'Puls 3060 Test';
		$defaultblog['description'] = "Default";
		$defaultblog['htmlURL']	= 'http://adm.puls3060.dk/bblog/';
		$defaultblog['rssURL']	= $clsBlog->_get_rss_url();
		$blogcats[] = $defaultblog;
		foreach($clsBlog->sections as $section) {
			$catr['description']= $section->name;
			$catr['htmlURL']	= $section->url;
			$catr['rssURL']		= $section->rss_url;
			$blogcats[]=$catr;
		}
		XMLRPC_response(XMLRPC_prepare($blogcats),WEBLOG_XMLPRPC_USERAGENT);

	} else {
		XMLRPC_error("301", "The username and password you entered was not accepted. Please try again.", WEBLOG_XMLRPC_USERAGENT);
	}
}

// !metaWeblog.getCategories_2_BlogJet get SECTIONS, we call them SECTIONS!
function metaWeblog_getCategories_2_BlogJet ($params) {
	global  $clsBlog;
	if($clsBlog->userauth($params[1],$params[2])) {
		// password accepted
		$blogcats = array();
		$blogcats['Default'] = true;

		$objSection = new clsSection();
		while ($objSection->nextSectionList()){
			$blogcats[$objSection->name]=true;
		}
		XMLRPC_response(XMLRPC_prepare($blogcats, 'struct'),WEBLOG_XMLPRPC_USERAGENT);

	} else {
		XMLRPC_error("301", "The username and password you entered was not accepted. Please try again.", WEBLOG_XMLRPC_USERAGENT);
	}
}

// !metaWeblog.getCategories_LiveWriter get SECTIONS, we call them SECTIONS!
function metaWeblog_getCategories_LiveWriter ($params) {
	global  $clsBlog;
	if($clsBlog->userauth($params[1],$params[2])) {
		// password accepted
		$blogcats = array();
		$catr['description'] = "Default";
		$blogcats["Default"] = $catr;

		$objSection = new clsSection();
		while ($objSection->nextSectionList()){
			$catr['description']= $objSection->name;
			$blogcats[$objSection->name]= $catr;
		}
		XMLRPC_response(XMLRPC_prepare($blogcats, 'struct'),WEBLOG_XMLPRPC_USERAGENT);

	} else {
		XMLRPC_error("301", "The username and password you entered was not accepted. Please try again.", WEBLOG_XMLRPC_USERAGENT);
	}
}

?>
