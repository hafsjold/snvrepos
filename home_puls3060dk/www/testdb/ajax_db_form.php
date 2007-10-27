<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html><head>
<title>NuSphere DB Wizard generated form</title>
<meta http-equiv="Content-Type" content="text/html; charset=<?=$config['encoding']?>" />
<style type="text/css">
    body                 { font-family: Tahoma,sans-serif,Verdana; font-size: 9pt;}
    table.datatable      { background: #fcfcfc; }
    table.datatable * td { padding: 0px 8px 0px 8px; margin: 0 8px 0 8px; }
    tr.sublight          { background: #ededed; }
/*     table.datatable * tr { white-space: nowrap; } */
    table.datatable * th { background: #ffffcc; text-align: center; }
</style>

<?php
require_once "db_utils.inc";
/* use php code to initialize form validation */
$fielddef = array(
    'f0' => array(FLD_ID => true, FLD_VISIBLE => true, FLD_DISPLAY => 'id', FLD_DISPLAY_SZ => 7,
        FLD_INPUT => true, FLD_INPUT_TYPE => 'text',
        FLD_INPUT_SZ => 7, FLD_INPUT_MAXLEN => 10, FLD_INPUT_DFLT => '',
        FLD_INPUT_NOTEMPTY => true, FLD_INPUT_VALIDATION => 'Numeric',
        FLD_DATABASE => 'id'),
    'f1' => array(FLD_ID => false, FLD_VISIBLE => true, FLD_DISPLAY => 'navn', FLD_DISPLAY_SZ => 100,
        FLD_INPUT => true, FLD_INPUT_TYPE => 'text',
        FLD_INPUT_SZ => 100, FLD_INPUT_MAXLEN => 51, FLD_INPUT_DFLT => '',
        FLD_INPUT_NOTEMPTY => true, FLD_INPUT_VALIDATION => '',
        FLD_DATABASE => 'navn'),
    'f2' => array(FLD_ID => false, FLD_VISIBLE => true, FLD_DISPLAY => 'dato', FLD_DISPLAY_SZ => 22,
        FLD_INPUT => true, FLD_INPUT_TYPE => 'text',
        FLD_INPUT_SZ => 22, FLD_INPUT_MAXLEN => 30, FLD_INPUT_DFLT => '',
        FLD_INPUT_NOTEMPTY => true, FLD_INPUT_VALIDATION => '',
        FLD_DATABASE => 'dato'),
    'f3' => array(FLD_ID => false, FLD_VISIBLE => true, FLD_DISPLAY => 'nrtype', FLD_DISPLAY_SZ => 100,
        FLD_INPUT => true, FLD_INPUT_TYPE => 'text',
        FLD_INPUT_SZ => 100, FLD_INPUT_MAXLEN => 11, FLD_INPUT_DFLT => '',
        FLD_INPUT_NOTEMPTY => true, FLD_INPUT_VALIDATION => '',
        FLD_DATABASE => 'nrtype'),
    'f4' => array(FLD_ID => false, FLD_VISIBLE => true, FLD_DISPLAY => 'afhente_dit_loebsnummer', FLD_DISPLAY_SZ => 100,
        FLD_INPUT => true, FLD_INPUT_TYPE => 'text',
        FLD_INPUT_SZ => 100, FLD_INPUT_MAXLEN => 513, FLD_INPUT_DFLT => '',
        FLD_INPUT_NOTEMPTY => true, FLD_INPUT_VALIDATION => '',
        FLD_DATABASE => 'afhente_dit_loebsnummer')
);
$idx = 0;
foreach($fielddef as $fkey=>$fld) {
    if ($fld[FLD_INPUT]) {
        if ($fld[FLD_INPUT_NOTEMPTY]) {
            if (!empty($fld_indices_notempty)) $fld_indices_notempty .= ', ';
            $fld_indices_notempty .= $idx;
        }
        if (!empty($fld[FLD_INPUT_VALIDATION])) {
            $name = "fld_indices_" . $fld[FLD_INPUT_VALIDATION];
            if (isset(${$name})) ${$name} .= ', ';
            ${$name} .= $idx;
        }
    }
    $idx++;
}
?>
 <script  type="text/javascript">
<!--
function doslice(arg, idx) {
    var ret = Array();
    for (var i = idx; i < arg.length; i++) {
        ret.push(arg[i]);
    }
    return ret;
}

function Check(theForm, what, regexp, indices) {
    for (var i = 0; i < indices.length; i++) {
        var el = theForm.elements[indices[i]];
        if (el.value == "") continue;
        var avalue = el.value;
        if (!regexp.test(avalue)) {
            alert("Field is not a valid " + what);
            el.focus();
            return false;
        }
    }
    return true;
}

function CheckEmail(theForm) {
    var regexp = /^[0-9a-z\.\-_]+@[0-9a-z\-\_]+\..+$/i;    
    return Check(theForm, "email", regexp, doslice(arguments, 1));
}

function CheckAlpha(theForm) {
    var regexp = /^[a-z]*$/i;
    return Check(theForm, "alpha value", regexp, doslice(arguments, 1));
}

function CheckAlphaNum(theForm) {
    var regexp = /^[a-z0-9]*$/i;
    return Check(theForm, "alphanumeric value", regexp, doslice(arguments, 1));
}

function CheckNumeric(theForm) {
    for (var i = 1; i < arguments.length; i++) {
        var el = theForm.elements[arguments[i]];
        if (el.value == "") continue;
        var avalue = parseInt(el.value);
        if (isNaN(avalue)) {
            alert("Field is not a valid integer number");
            el.focus();
            return false;
        }
    }
    return true;
}

function CheckFloat(theForm) {
    for (var i = 1; i < arguments.length; i++) {
        var el = theForm.elements[arguments[i]];
        if (el.value == "") continue;
        var avalue = parseFloat(el.value);
        if (isNaN(avalue)) {
            alert("Field is not a valid floating point number");
            el.focus();
            return false;
        }
    }
    return true;
}

function CheckDate(theForm) {
    for (var i = 1; i < arguments.length; i++) {
        var el = theForm.elements[arguments[i]];
        if (el.value == "") continue;
        var avalue = el.value;
        if (isNaN(Date.parse(avalue))) {
            alert("Field is not a valid date");
            el.focus();
            return false;
        }
    }
    return true;
}

function CheckTime(theForm) {
    var regexp = /^[0-9]+:[0-9]+:[0-9]+/i;    
    if (!Check(theForm, "time", regexp,  doslice(arguments, 1)))
        return false;                 
    for (var i = 1; i < arguments.length; i++) {
        var el = theForm.elements[arguments[i]];
        if (el.value == "") continue;
        var avalue = el.value;
        if (isNaN(Date.parse("1/1/1970 " + avalue))) {
            alert("Field is not a valid time");
            el.focus();
            return false;
        }
    }
    return true;
}

function CheckRequiredFields(theForm) {    
    for (var i = 1; i < arguments.length; i++) {
        var el = theForm.elements[arguments[i]];
        if (el.value=="") {
            alert("This field may not be empty");
            el.focus();
            return false;
        }
    }
    return true;
}

function CheckForm(theForm) {
    return (
        CheckRequiredFields(theForm<?php echo isset($fld_indices_notempty) ? ", " . $fld_indices_notempty : "" ?>) &&
        CheckEmail(theForm<?php echo isset($fld_indices_Email) ? ", " . $fld_indices_Email : "" ?>) &&
        CheckAlpha(theForm<?php echo isset($fld_indices_Alpha) ? ", " . $fld_indices_Alpha : "" ?>) &&
        CheckAlphaNum(theForm<?php echo isset($fld_indices_AlphaNum) ? ", " . $fld_indices_AlphaNum : "" ?>) &&
        CheckNumeric(theForm<?php echo isset($fld_indices_Numeric) ? ", " . $fld_indices_Numeric : "" ?>) &&
        CheckFloat(theForm<?php echo isset($fld_indices_Float) ? ", " . $fld_indices_Float : "" ?>) &&
        CheckDate(theForm<?php echo isset($fld_indices_Date) ? ", " . $fld_indices_Date : "" ?>) &&
        CheckTime(theForm<?php echo isset($fld_indices_Time) ? ", " . $fld_indices_Time: "" ?>)
    )
}


function getChkVal(formName){

    for (i=0; i < document.forms[formName].length; i++){
        if (document.forms[formName].elements[i].type=="radio"
        &&document.forms[formName].elements[i].checked){
        return(document.forms[formName].elements[i].value);
        }
    }
}


 //-->
</script>
</head>
<body onload="ajaxFunctionForm()">
  <script language="javascript" type="text/javascript">
<!--
function getAjaxRequest() {
var ajaxRequest;  // The variable that makes Ajax possible! 
 try{
   // Opera 8.0+, Firefox, Safari
   ajaxRequest = new XMLHttpRequest();
 }catch (e){
   // Internet Explorer Browsers
   try{
      ajaxRequest = new ActiveXObject("Msxml2.XMLHTTP");
   }catch (e) {
      try{
         ajaxRequest = new ActiveXObject("Microsoft.XMLHTTP");
      }catch (e){
         // Something went wrong
         alert("Your browser broke!");
         return false;
      }
   }
 }
 return ajaxRequest;
}
function replaceDivElements (parent, child, new_value) {
    //document.getElementById("ajaxDiv").innerText =    ajaxDisplay.value;
   
      if(document.getElementById) { //Open standards method
      
        var childDiv = document.getElementById(child);
      
          
      }
      else if(document.all) { //IE method
        var childDiv=document.all[child]; 
      }

        
      var newDiv = document.createElement('div');
      //alert ("before adding child"+newDiv+"Child "+child ); 
      newDiv.id = childDiv.id ;
      
      newDiv.innerHTML = new_value;
      var parentElem =  document.getElementById(parent);
      while (parentElem.firstChild) {
         parentElem.removeChild(parentElem.firstChild);
      }
      parentElem.appendChild(newDiv)  ;
}

function ajaxFunctionUpdateInsert(mode){
     var ajaxRequest;  // The variable that makes Ajax possible!
      ajaxRequest =  getAjaxRequest();
     // Create a function that will receive data 
     // sent from the server and will update
     // div section in the same page.
     ajaxRequest.onreadystatechange = function(){
       if(ajaxRequest.readyState == 4){
         replaceDivElements ( "parentForAjaxAction", "ajaxActionsDiv",ajaxRequest.responseText); 
       }
     }
     // Now get the value from user and pass it to
     // server script.
     //alert (key);
     if (typeof mode != "undefined" ) {

      var checked_rec_key = getChkVal("ActionForm");
      var queryString = "?mode="+mode+"&RKEY="+checked_rec_key;//+"&DBGSESSID=1;d%3D1";
      ajaxRequest.open("GET","table_tbllob.php"+queryString, true);
      ajaxRequest.send(null); 
    }                            
}                              

function ajaxFunctionDelete(){
     var ajaxRequest;  // The variable that makes Ajax possible!
      ajaxRequest =  getAjaxRequest();
     // Create a function that will receive data 
     // sent from the server and will update
     // div section in the same page.
     ajaxRequest.onreadystatechange = function(){
       if(ajaxRequest.readyState == 4){
         replaceDivElements ( "parentForAjaxForm", "ajaxFormDiv",ajaxRequest.responseText); 
       }
     }
     // Now get the value from user and pass it to
     // server script.
     //alert (key);
     
      var checked_rec_key = getChkVal("ActionForm");
      var queryString = "?mode=d&RKEY="+checked_rec_key;//+"&DBGSESSID=1;d%3D1";
      ajaxRequest.open("GET","table_tbllob.php"+queryString, true);
      ajaxRequest.send(null); 
                               
}
//Browser Support Code          
function ajaxFunctionForm(key, start){
 var ajaxRequest;  // The variable that makes Ajax possible!
      ajaxRequest =  getAjaxRequest();
     // Create a function that will receive data 
     // sent from the server and will update
     // div section in the same page.
    
     ajaxRequest.onreadystatechange = function(){
     
       if(ajaxRequest.readyState == 4){
       //  alert ( ajaxRequest.responseText);
           replaceDivElements ( "parentForAjaxForm", "ajaxFormDiv",ajaxRequest.responseText); 
       }
     }
     // Now get the value from user and pass it to
     // server script.
     //alert (key);
 // Now get the value from user and pass it to
 // server script.
 //alert (key);
 var queryString = "?";
  
 if (typeof key != "undefined") 
   queryString = queryString + "&order_by=" + key;//+"&DBGSESSID=1;d%3D1";
 if (typeof start != "undefined") 
   queryString = queryString + "&start=" + start;
 
 ajaxRequest.open("GET", "table_tbllob.php" +  queryString, true);
 ajaxRequest.send(null); 
 }
 
//-->
</script>
<div id="parentForAjaxAction">
  <div id="ajaxActionsDiv">
    <form id="theForm" name="ActionForm" method="post" onsubmit=return CheckForm(this) action="table_tbllob.php">
  <div id="parentForAjaxForm">
   <div id="ajaxFormDiv"><input type='button' onclick='ajaxFunctionForm()' value='Display Form'/>
  </div>
</div>                  
<table cellpadding="1" cellspacing="0" border="0" bgcolor="#ababab"><tr><td>
<table cellpadding="1" cellspacing="0" border="0" bgcolor="#fcfcfc"><tr><td>
    <input type="button" value="insert" onclick='ajaxFunctionUpdateInsert("i")' />&nbsp;
    <input type="button" value="update" onclick='ajaxFunctionUpdateInsert("u")' />&nbsp;
    <input type="button" value="delete" onclick='ajaxFunctionDelete()' />
</td></tr>
</table>
</td></tr>
</table>                        

</form>

</div>
</div>                  


</body>
</html>