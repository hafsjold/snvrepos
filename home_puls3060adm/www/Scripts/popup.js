/* $Id: popup.js,v 1.1 2005/09/02 16:15:33 ta Exp $ */
  
function popWin(url, w, h) {
	window.open(url,'_blank','toolbar=no, location=no, directories=no, status=no, menubar=no, scrollbars=yes, resizable=no, copyhistory=yes, width=' + w + ', height=' + h + '');
	return false;
}