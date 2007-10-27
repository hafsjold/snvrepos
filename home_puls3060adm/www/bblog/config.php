<?php

/* file and paths */

// Full path of the directory where you've installed bBlog
// ( i.e. the bblog folder )
define('BBLOGROOT','/data/home/puls3060adm/www/bblog/');

/* URL config */

// URL to your blog ( one folder below the 'bBlog' folder )
// e.g, if your bBlog folder is at www.example.com/blog/bblog, your
// blog will be at www.example.com/blog/
define('BLOGURL','http://adm.puls3060.dk/');

// URL to the bblog folder via the web.
// Becasue if you're using clean urls and news.php as your BLOGURL,
// we can't automatically append bblog to it.
define('BBLOGURL',BLOGURL.'bblog/');

define('C_BLOGURL',BLOGURL);
define('C_BLOGNAME','Puls 3060 Test');



?>