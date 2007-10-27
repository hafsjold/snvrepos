<?php

// load Smarty library
require('/usr/local/share/smarty/Smarty.class.php');

class Smarty_Puls3060adm extends Smarty {

   function Smarty_Puls3060adm()
   {
        // Class Constructor.
        // These automatically get set with each new instance.

        $this->Smarty();

		$this->plugins_dir[] = '/data/home/includes/my_smarty_plugins';
		$this->template_dir = '/data/home/puls3060adm/www';
		$this->compile_dir = '/data/home/puls3060adm/www/smarty/templates_c';
		$this->cache_dir = '/data/home/puls3060adm/www/smarty/cache';
		$this->config_dir = '/data/home/puls3060adm/www/smarty/configs';

		$this->compile_check = true;
		$this->debugging = false;
		$this->caching = true;
		$smarty->cache_lifetime = 3600;
        
        $this->assign('app_name', 'Puls 3060 Adm');
   }

}
?> 