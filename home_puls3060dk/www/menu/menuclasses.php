<?php

define('ROOT_DIR', '/data/home/puls3060dk/www/');
define('INCLUDE_DIR', '/data/home/includes/');
define('IMAGES_DIR', 'menu/image/');

require('clsLogin.php');
session_start();
/* = = = = = = = = = = = = = = = = = = = = */
class clsMenu {

	protected $_UserSeclevel;
	protected $_menuitems = array();

	function __construct(){
		$this->_UserSeclevel = 1;
		if (isset($_SESSION['login'])) {
			$login = $_SESSION['login'];
			if ($login->login) $this->_UserSeclevel = 2;
		}		
		return ;
	} // ctor

	function addMenuItem($inItemName , $inItemText ,$inItemLink, $inNewWindow = false, $inItemSeclevel =1){
        if ($this->_UserSeclevel >= $inItemSeclevel ) {
			$this->_menuitems[$inItemName] = new clsMenuItem($inItemName, $inItemText, $inItemLink, $inNewWindow, $inItemSeclevel);
		}
	}
	
	function makeImages(){
		foreach ($this->_menuitems as $menuitem) {
			$menuitem->makeImageFiles();
		}
	}

	function makeHtml() {
		$html = '';
		foreach ($this->_menuitems as $menuitem) {
			$html .= $menuitem->makeHtml();
		}
		return $html;
	}
	
	function onLoad(){
		$html = '';
		$first = true;
		foreach ($this->_menuitems as $menuitem) {
			if ($first) {
			  $html .= '\'/' . $menuitem->getPathImage2() . '\'';
			}
			else { 
			  $html .= ',\'/' . $menuitem->getPathImage2() . '\'';
			}
			$first = false;
		}
		return $html;		
	}

} // class clsMenu

/* = = = = = = = = = = = = = = = = = = = = */

class clsMenuItem {

	protected	$_ItemName;
	protected	$_ItemText;
	protected	$_ItemLink;
	protected   $_NewWindow;
	protected   $_ItemSeclevel;

	private	$_PathSaveImage1;
	private	$_PathSaveImage2;
	private	$_PathImage1;
	private	$_PathImage2;

	function __construct( $inItemName , $inItemText ,$inItemLink, $inNewWindow = false, $inItemSeclevel =1 ){

		$this->_ItemName = $inItemName ;
		$this->_ItemText = $inItemText ;
		$this->_ItemLink= $inItemLink ;
		$this->_NewWindow= $inNewWindow ;
		$this->_ItemSeclevel= $inItemSeclevel ;
		
		$this->_PathSaveImage1 = ROOT_DIR . IMAGES_DIR . $this->_ItemName . '_1.png';
		$this->_PathSaveImage2 = ROOT_DIR . IMAGES_DIR . $this->_ItemName . '_2.png';
		$this->_PathImage1 = IMAGES_DIR . $this->_ItemName . '_1.png';
		$this->_PathImage2 = IMAGES_DIR . $this->_ItemName . '_2.png';
		return ;

	} // ctor

	function getPathImage2(){return $this->_PathImage2;}
	
	function makeImageFiles( ){
		$img1 = imagecreatefrompng (INCLUDE_DIR . 'menutemplate1.png');
		$img2 = imagecreatefrompng (INCLUDE_DIR . 'menutemplate2.png');

		$font = INCLUDE_DIR . 'arial.ttf';
		//$font = INCLUDE_DIR . 'arialbd.ttf';
		$point = 8;
		$blue_51_153 = imagecolorallocate($img1, 0, 51, 153);
		$blue_102_204 = imagecolorallocate($img2, 0, 102, 204);

		$size  = imagettfbbox ( $point, 0, $font, $this->_ItemText );
		$dx = 103-abs($size[2]-$size[0]);

		imagettftext($img1, $point, 0, $dx, 16, $blue_51_153, $font, $this->_ItemText);
		imagettftext($img2, $point, 0, $dx, 16, $blue_102_204, $font, $this->_ItemText);

		imagepng($img1, $this->_PathSaveImage1);
		imagepng($img2, $this->_PathSaveImage2);

		imagedestroy($img1);
		imagedestroy($img2);
	} // makeImageFiles()

	function getItemTarget() {
	    if ($this->_NewWindow)
	       return ' target="_blank" ';
		else
           return ' ';
	}
	
	function makeHtml(){
		$html = '
		  <tr><td><a href="' . $this->_ItemLink . '"' .  $this->getItemTarget() . '
		    onClick="return top.rwlink(href);"
		    onMouseOut="MM_swapImgRestore()"
		    onMouseOver="MM_displayStatusMsg(\'' . $this->_ItemText . '\');
                         MM_swapImage(\'' . $this->_ItemName . '\',\'\',\'/' . $this->_PathImage2 . '\',1);
                         return document.MM_returnValue" >
		    <img name="' . $this->_ItemName . '" src="/' . $this->_PathImage1 . '"
		    width="117" height="26" border="0">
		  </a></td></tr>';
		return $html;
	} // makeHtml()

} // class clsMenuItem

/* = = = = = = = = = = = = = = = = = = = = */

?>
