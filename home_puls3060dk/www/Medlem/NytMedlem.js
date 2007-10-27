  var Page_CompareValidators =  new Array(document.all["fodtdato_val_3"]);
  var Page_ValidationSummaries =  new Array(document.all["valsum1"]);
  var Page_Validators =  new Array(
  	document.all["fornavn_val_1"], 
  	document.all["efternavn_val_1"], 
  	document.all["adresse_val_1"], 
  	document.all["postnr_val_1"], 
  	document.all["postnr_val_2"], 
  	document.all["bynavn_val_1"], 
  	document.all["tlfnr_val_1"], 
  	document.all["tlfnr_val_2"], 
  	document.all["mailadr_val_1"], 
  	document.all["mailadr_val_2"], 
  	document.all["fodtdato_val_1"], 
  	document.all["fodtdato_val_2"], 
  	document.all["fodtdato_val_3"], 
  	document.all["fodtdato_val_4"], 
  	document.all["kon_val_1"], 
  	document.all["kon_val_2"]);

  var Page_ValidationErrorPrefix = "Validation script error: ";
  var Page_ValidationBadID = "Client ID is not unique: ";
  var Page_ValidationBadFunction = "Invalid ClientValidationFunction: ";
  var Page_ValidationActive = false;
  if (typeof(Page_ValidationVer) == "undefined")
      alert("Warning! Unable to find script library 'WebUIValidation.js'.");
  else if (Page_ValidationVer != "112")
      alert("Warning! This page is using the wrong version of 'WebUIValidation.js'. Page expects version '112'. Script library is '" + Page_ValidationVer + "'.");
  else
      ValidatorOnLoad();

  function ValidatorOnSubmit() {
      if (Page_ValidationActive) {
          ValidatorCommonOnSubmit();
      }
  }
  
  function CheckAlder(val, value) {
      var day, month, year, m, yearLastExp;
      yearLastExp = new RegExp("^\\s*(\\d{1,2})([-./])(\\d{1,2})\\2(\\d{4})\\s*$");
      m = value.match(yearLastExp);
      day = m[1];
      month = m[3];
      year = m[4];
      var now = new Date();
      var date = new Date(year, (month - 1), day);
	  var alder = now.getFullYear() - date.getFullYear() + 1;
      return (alder > 1) && (alder < 100);
  }
