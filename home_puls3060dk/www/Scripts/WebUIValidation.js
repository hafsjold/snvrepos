var Page_ValidationVer = "112";
var Page_IsValid = true;

function ValidatorUpdateDisplay(val) {
    if (typeof(val.display) == "string") {    
        if (val.display == "None") {
            return;
        }
        if (val.display == "Dynamic") {
            val.style.display = val.isvalid ? "none" : "inline";
            return;
        }
    }
    val.style.visibility = val.isvalid ? "hidden" : "visible";
}

function ValidatorUpdateIsValid() {
    var i;
    for (i = 0; i < Page_Validators.length; i++) {
        if (!Page_Validators[i].isvalid) {
            Page_IsValid = false;
            return;
        }
   }
   Page_IsValid = true;
}

function ValidatorHookupControl(control, val) {
    if (typeof(control.tagName) == "undefined" && typeof(control.length) == "number") {
        var i;
        for (i = 0; i < control.length; i++) {
            if (control[i].type == "radio") {
                ValidatorHookupControl(control[i], val);
            } 
            else {
                throw new Error(Page_ValidationBadID + ((control[i].id == "") ? control[i].name : control[i].id));
            }
        }
        return;
    }
    else {
        if (typeof(control.Validators) == "undefined") {
            control.Validators = new Array;
            var ev;
            if (control.type == "radio") {
                ev = control.onclick;
            } else {
                ev = control.onchange;
            }
            if (typeof(ev) == "function" ) {            
                ev = ev.toString();
                ev = ev.substring(ev.indexOf("{") + 1, ev.lastIndexOf("}"));
            }
            else {
                ev = "";
            }
            var func = new Function("ValidatorOnChange(); " + ev);
            if (control.type == "radio") {
                control.onclick = func;
            } else {            
                control.onchange = func;
            }

        }
        control.Validators[control.Validators.length] = val;
    }    
}

function ValidatorGetValue(id) {
    var control;
    control = document.all[id];
    if (typeof(control.value) == "string") {
        return control.value;
    }
    if (typeof(control.tagName) == "undefined" && typeof(control.length) == "number") {
        var j;
        for (j=0; j < control.length; j++) {
            if (control[j].type == "radio") {
                if (control[j].status == true) {
                    return control[j].value;
                }
            }
        }
    }
    return "";
}

function ValidatorCommonOnSubmit() {
    try {    
        var i;
        for (i = 0; i < Page_Validators.length; i++) {
            ValidatorValidate(Page_Validators[i]);
        }
        ValidatorUpdateIsValid();    
        ValidationSummaryOnSubmit();
        event.returnValue = Page_IsValid;        
    }
    catch (e) {
        ValidatorDisplayError(e);
        Page_ValidationActive = false;
    }
}

function ValidatorEnable(val, enable) {
    val.enabled = (enable != false);
    ValidatorValidate(val);
    ValidatorUpdateIsValid();
}

function ValidatorOnChange() {
    var vals = event.srcElement.Validators;
    var i;
    for (i = 0; i < vals.length; i++) {
        ValidatorValidate(vals[i]);
    }
    ValidatorUpdateIsValid();    
}

function ValidatorValidate(val) {    
    val.isvalid = true;
    if (val.enabled != false) {
        if (typeof(val.evaluationfunction) == "function") {
            val.isvalid = val.evaluationfunction(val); 
        }
    }
    ValidatorUpdateDisplay(val);
}

function ValidatorDisplayError(e) {
    var s = Page_ValidationErrorPrefix + " " + e;
    if (typeof(e.number) != "undefined") {
        s += " " + e.number;
    }
    if (typeof(e.description) != "undefined") {
        s += " " + e.description;
    }
    alert(s);
}

function ValidatorOnLoad() {
    try {
        CompareValidatorOnLoad();
        RangeValidatorOnLoad();    
        var i, val;
        for (i = 0; i < Page_Validators.length; i++) {
            val = Page_Validators[i];
            if (typeof(val.evaluationfunction) == "string") {
                eval("val.evaluationfunction = " + val.evaluationfunction + ";");
            }
            if (typeof(val.isvalid) == "string") {
                if (val.isvalid == "False") {
                    val.isvalid = false;                                
                    Page_IsValid = false;
                } 
                else {
                    val.isvalid = true;
                }
            } else {
                val.isvalid = true;
            }
            if (typeof(val.enabled) == "string") {
                val.enabled = (val.enabled != "False");
            }
            if (typeof(val.controltovalidate) == "string") {
                ValidatorHookupControl(document.all[val.controltovalidate], val);
            }
        }
        Page_ValidationActive = true;
    }
    catch (e) {
        ValidatorDisplayError(e);
        Page_ValidationActive = false;
    }
}

function ValidatorConvert(op, dataType, val) {
    var num, cleanInput, m, exp;
    if (dataType == "Integer") {
        exp = /^\s*[-\+]?\d+\s*$/;
        if (op.match(exp) == null) 
            return null;
        num = parseInt(op, 10);
        return (isNaN(num) ? null : num);
    }
    else if(dataType == "Double") {
        exp = new RegExp("^\\s*([-\\+])?(\\d+)(\\" + val.decimalchar + "(\\d+))?\\s*$");
        m = op.match(exp);
        if (m == null)
            return null;
        cleanInput = m[1] + m[2] + "." + m[4];
        num = parseFloat(cleanInput);
        return (isNaN(num) ? null : num);            
    } 
    else if (dataType == "Currency") {
        exp = new RegExp("^\\s*([-\\+])?(((\\d+)\\" + val.groupchar + ")*)(\\d+)"
                        + ((val.digits > 0) ? "(\\" + val.decimalchar + "(\\d{1," + val.digits + "}))?" : "")
                        + "\\s*$");
        m = op.match(exp);
        if (m == null)
            return null;
        var intermed = m[2] + m[5] ;
        cleanInput = m[1] + intermed.replace(new RegExp("(\\" + val.groupchar + ")", "g"), "") + ((val.digits > 0) ? "." + m[7] : 0);
        num = parseFloat(cleanInput);
        return (isNaN(num) ? null : num);            
    }
    else if (dataType == "Date") {
        var yearFirstExp = new RegExp("^\\s*(\\d{4})([-./])(\\d{1,2})\\2(\\d{1,2})\\s*$");
        m = op.match(yearFirstExp);
        var day, month, year;
        if (m != null) {
            day = m[4];
            month = m[3];
            year = m[1];
        }
        else {
            if (val.dateorder == "ymd"){
                return null;		
            }						
            var yearLastExp = new RegExp("^\\s*(\\d{1,2})([-./])(\\d{1,2})\\2(\\d{4})\\s*$");
            m = op.match(yearLastExp);
            if (m == null) {
                return null;
            }
            if (val.dateorder == "mdy") {
                day = m[3];
                month = m[1];
                year = m[4];
            }
            else {
                day = m[1];
                month = m[3];
                year = m[4];
            }
        }
        month -= 1;
        var date = new Date(year, month, day);
        return (typeof(date) == "object" && year == date.getFullYear() && month == date.getMonth() && day == date.getDate()) ? date.valueOf() : null;
    }
    else {
        return op.toString();
    }
}

function ValidatorCompare(operand1, operand2, operator, val) {
    var dataType = val.type;
    var op1, op2;
    if ((op1 = ValidatorConvert(operand1, dataType, val)) == null)
        return false;    
    if (operator == "DataTypeCheck")
        return true;
    if ((op2 = ValidatorConvert(operand2, dataType, val)) == null)
        return true;
    switch (operator) {
        case "NotEqual":
            return (op1 != op2);
        case "GreaterThan":
            return (op1 > op2);
        case "GreaterThanEqual":
            return (op1 >= op2);
        case "LessThan":
            return (op1 < op2);
        case "LessThanEqual":
            return (op1 <= op2);
        default:
            return (op1 == op2);            
    }
}

function CompareValidatorOnLoad() {
    if (typeof(Page_CompareValidators) == "undefined") 
        return;            
    var i, val;    
    for (i = 0; i < Page_CompareValidators.length; i++) {
        val = Page_CompareValidators[i];
        if (typeof(val.controltocompare) == "string") {
            ValidatorHookupControl(document.all[val.controltocompare], val);
        }
    }
}

function CompareValidatorEvaluateIsValid(val) {
    var value = ValidatorGetValue(val.controltovalidate);
    if (value == "") 
        return true;
    var compareTo = "";
    if (null == document.all[val.controltocompare]) {
        if (typeof(val.valuetocompare) == "string") {
            compareTo = val.valuetocompare;
        }
    }
    else {
        compareTo = ValidatorGetValue(val.controltocompare);
    }
    return ValidatorCompare(value, compareTo, val.operator, val);
}

function CustomValidatorEvaluateIsValid(val) {
    var value = "";
    if (typeof(val.controltovalidate) == "string") {
        value = ValidatorGetValue(val.controltovalidate);
        if (value == "")
            return true;
    }
    var valid = true;
    if (typeof(val.clientvalidationfunction) == "string") {
        try {
            eval("valid = (" + val.clientvalidationfunction + "(val, value) != false);");
        } catch (e) {
            alert(Page_ValidationBadFunction + val.clientvalidationfunction + " : " + e);
        }
    }        
    return valid;
}

function RegularExpressionValidatorEvaluateIsValid(val) {
    var value = ValidatorGetValue(val.controltovalidate);
    if (value == "")
        return true;        
    var rx = new RegExp(val.validationexpression);
    var match = rx.exec(value);
    if (match == null) {
        return false;
    }
    return (match.index == 0 && match.lastIndex == value.length);
}

function RequiredFieldValidatorEvaluateIsValid(val) {
    return (ValidatorGetValue(val.controltovalidate) != val.initialvalue)
}

function RangeValidatorOnLoad() {
    if (typeof(Page_RangeValidators) == "undefined")
        return;
    var i, val;
    for (i = 0; i < Page_RangeValidators.length; i++) {
        val = Page_RangeValidators[i];
        if (typeof(val.minimumcontrol) == "string") {
            ValidatorHookupControl(document.all[val.minimumcontrol], val);
        }
        if (typeof(val.maximumcontrol) == "string") {
            ValidatorHookupControl(document.all[val.maximumcontrol], val);
        }
    }
}

function RangeValidatorEvaluateIsValid(val) {
    var value = ValidatorGetValue(val.controltovalidate);
    if (value == "") 
        return true;
    var minimum = "";
    if (typeof(val.minimumcontrol) != "string") {
        if (typeof(val.minimumvalue) == "string") {
            minimum = val.minimumvalue;
        } 
    } else {
        minimum = ValidatorGetValue(val.minimumcontrol);
    }
    var maximum = "";
    if (typeof(val.maximumcontrol) != "string") {
        if (typeof(val.maximumvalue) == "string") {
            maximum = val.maximumvalue;
        } 
    } else {
        maximum = ValidatorGetValue(val.maximumcontrol);
    }        
    return (ValidatorCompare(value,minimum,"GreaterThanEqual",val) &&
            ValidatorCompare(value,maximum,"LessThanEqual",val));
}

function ValidationSummaryOnSubmit() {
    if (typeof(Page_ValidationSummaries) == "undefined") 
        return;
    var summary, sums, s;
    for (sums = 0; sums < Page_ValidationSummaries.length; sums++) {
        summary = Page_ValidationSummaries[sums];
        summary.style.display = "none";
        if (!Page_IsValid) {
            if (summary.showsummary != "False") {
                summary.style.display = "";
                if (typeof(summary.displaymode) != "string") {
                    summary.displaymode = "BulletList";
                }
                switch (summary.displaymode) {
                    case "List":
                        headerSep = "<br>";
                        first = "";
                        pre = "";
                        post = "<br>";
                        final = "";
                        break;
                        
                    case "BulletList":
                    default: 
                        headerSep = "";
                        first = "<ul>";
                        pre = "<li>";
                        post = "</li>";
                        final = "</ul>";
                        break;
                        
                    case "SingleParagraph":
                        headerSep = " ";
                        first = "";
                        pre = "";
                        post = " ";
                        final = "<br>";
                        break;
                }
                s = "";
                if (typeof(summary.headertext) == "string") {
                    s += summary.headertext + headerSep;
                }
                s += first;
                for (i=0; i<Page_Validators.length; i++) {
                    if (!Page_Validators[i].isvalid && typeof(Page_Validators[i].errormessage) == "string") {
                        s += pre + Page_Validators[i].errormessage + post;
                    }
                }   
                s += final;
                summary.innerHTML = s; 
                window.scrollTo(0,0);
            }
            if (summary.showmessagebox == "True") {
                s = "";
                if (typeof(summary.headertext) == "string") {
                    s += summary.headertext + "<BR>";
                }
                for (i=0; i<Page_Validators.length; i++) {
                    if (!Page_Validators[i].isvalid && typeof(Page_Validators[i].errormessage) == "string") {
                        switch (summary.displaymode) {
                            case "List":
                                s += Page_Validators[i].errormessage + "<BR>";
                                break;
                                
                            case "BulletList":
                            default: 
                                s += "  - " + Page_Validators[i].errormessage + "<BR>";
                                break;
                                
                            case "SingleParagraph":
                                s += Page_Validators[i].errormessage + " ";
                                break;
                        }
                    }
                }
                span = document.createElement("SPAN");
                span.innerHTML = s;
                s = span.innerText;
                alert(s);
            }
        }
    }
}
