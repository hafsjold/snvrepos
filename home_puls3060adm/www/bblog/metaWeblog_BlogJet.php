<?php

$xmlrpc_methods = array();

$xmlrpc_methods['blogger.getUsersBlogs']  	= 'blogger_getUsersBlogs';
$xmlrpc_methods['blogger.deletePost']		= 'blogger_deletePost';
$xmlrpc_methods['metaWeblog.newPost']		= 'metaWeblog_newPost';
$xmlrpc_methods['metaWeblog.getPost']		= 'metaWeblog_getPost';
$xmlrpc_methods['metaWeblog.getCategories']	= 'metaWeblog_getCategories';
$xmlrpc_methods['metaWeblog.getRecentPosts'] = 'metaWeblog_getRecentPosts';
$xmlrpc_methods['metaWeblog.editPost']		= 'metaWeblog_editPost';
$xmlrpc_methods['metaWeblog.newMediaObject']  = 'metaWeblog_newMediaObject';

include 'xmlrpc.php';
?>