
function getObject(s_id,o_doc) {
        domDoc = o_doc || document;
        return domDoc.getElementById(s_id);
       }
//function getObject(s_id,o_doc) {
//	domDoc = o_doc || document;
//	if ((typeof (domDoc.getElementById(s_id)) != "undefined")) {
//		return domDoc.getElementById(s_id);
//	} else {
//		return null;
//}

function getContents(s_id) {
    o_element = getObject(s_id);
    if(! o_element) {return '';}
    s_element = o_element.innerHTML;
    o_element = null;
    return s_element;
}
function getValue(s_id) {
    o_element = getObject(s_id);
    if(o_element==null) {return '';}
    if(typeof(o_element)=='select') {return o_element.selectedValue;}
    s_element=o_element.value;
    o_element = null;
    //show the value that was gotten
    //alert(s_element);
    return s_element;
}
function setContents(s_id,s_value) {
    o_element = getObject(s_id);
    if((typeof(s_value)!="undefined")&&(o_element)){o_element.innerHTML=s_value;}
    //alert('o_element.innerHTML ' +o_element.id + o_element.innerHTML);
    o_element = null;
}
function ClearContents(s_id) {
    o_element = getObject(s_id);
    if(o_element){o_element.innerHTML='';}
    //alert('o_element.innerHTML ' +o_element.id + o_element.innerHTML);
    o_element = null;
}
function setValue(s_id,s_value) {
    o_element = getObject(s_id);
    if(o_element){o_element.value=s_value;}
    o_element = null;
}
function copyContents(s_sourceID,s_targetID) {
    s_source = getContents(s_sourceID);
    //alert(s_source);
    setContents(s_targetID,s_source);
}
function copyValue(s_sourceID,s_targetID) {
    s_source = getValue(s_sourceID);
    //alert(s_source);
    setValue(s_targetID,s_source);
}
function buttonClick(s_id) {
    o_element = getObject(s_id);
    if(o_element){
//        if (o_element.dispatchEvent) {
//            var e = document.createEvent("MouseEvents"); 
//            e.initEvent("click", true, true);
//            o_element.dispatchEvent(e); 
//        }else{
            if(o_element.click){
                o_element.click();
            }else{
                //hrefCode=o_element.href.replace(/^javascript:/,'').replace(/%20/g,' ').replace(/%22/g,'&quot;');
                //works for firefox most the time 
                window.location=o_element.href;
            }
//        }
    }
    o_element = null;
}

function setDisabled(s_id,s_value) {
    o_element = getObject(s_id);
    if(o_element){o_element.disabled=s_value;}
    o_element = null;
}
function setReadOnly(s_id,b_value) {
    o_element = getObject(s_id);
    if(o_element){o_element.readOnly=b_value;}
    o_element = null;
}
function swapValues(s_text1,s_text2) {
    //Save this because it is being over written
    s_stash = getValue(s_text2);
    setValue(s_text2,getValue(s_text1));
    setValue(s_text1,s_stash);
}
function getCssClass(s_control) {
    o_control=getObject(s_control);
    if(o_control){s_class=o_control.className;}else{s_class=''}
    o_control=null;
    return s_class;
}
function setCssClass(s_control,s_class) {
    o_control=getObject(s_control);
    if(o_control){o_control.className=s_class;}
    o_control=null;
}
function sourceFile(file,path) {
    if(! path) {path = '/cms/res/ajax/';}
    document.write('<script src="' + path + file +  '" type="text/javascript"></script>');
}

function HasExitedElement(s_element,e) {
	    //set up mozilla contains
	if (window.Node && Node.prototype && !Node.prototype.contains) { Node.prototype.contains = function (arg) { return !!(this.compareDocumentPosition(arg) & 16) } } 

    o_element= getObject(s_element);
    //alert(arguments);
    o_toElement = GetToElement(e);

    b_HasExitedElement=false;
//    alert('the div: ' + o_element.id);
//   alert('the to element: ' + o_toElement.id);

    //If doesnot find a toElement then you can not determine whether you have exited element
    //Just assume you havent'....
    if(o_toElement) {    
    //If toElement is in current element or equal to then you havenot exited
        if(o_element.contains(o_toElement)||o_element==o_toElement) {
            b_HasExitedElement=false;
         }else{
            b_HasExitedElement=true;     
         }
    }     
    o_element=null;
    o_toElement=null;
    return b_HasExitedElement;
}
function GetToElement(e) {
        //need to pass args from the 1st function being called by event
//        theEvent=window.event || args.callee.caller.arguments[1];               
        theEvent=window.event || e;               
//alert(window.event.toElement);
        if(window.event) {
                theToElement=theEvent.toElement;
          }else{
                //Have to do this becuase mouseout targeted is where your mousing from in mozilla. dumb.
                if(theEvent.type=='mouseout') {
                        theToElement=theEvent.relatedTarget;
                        //If related target doesn't exist then try currentTarget
                        if(!(theToElement)) {theEvent.currentTarget}
                }else{
                        theToElement=theEvent.target;
                }
          }
//alert('theToElement: ' + theToElement);
        return theToElement;
}
    
function IsChecked(s_cb,b_warn) {
    o_cb = getObject(s_cb);
    if(! b_warn) {b_warn=0;}
    if(! o_cb) {
        if(b_warn==1) {alert(s_cb + ' cb not defined');}
        return false;
    }else{
        b_cb = o_cb.checked;
        o_cb=null;
        return b_cb;
    }    
}
function MyIsEmpty(s_input,b_warn) {
//Had to call this MyIsEmpty instead of IsEmpty because it was overriding a cute editor function IsEmpty
//Why they made that global i don't know
//alert(IsEmpty.caller);
    o_input = getObject(s_input);
    if(! o_input) {if(b_warn) {alert(s_input + ' input not defined');}return true;}
    if(o_input.value) {return false;}
    return true;
}
function IncrementTextbox(s_input,i_inc,i_min) {
    if(!(i_min)) {i_min=0}
    o_input = getObject(s_input);
    if(! o_input) {alert(s_input + ' input not defined');return false;}
    s_value = o_input.value;
    s_value=s_value.replace(/\,/g,'');
    s_value = +s_value + i_inc;
    if(s_value < i_min) {s_value =i_min;}
    o_input.value=FormatNumber(s_value);
    o_input=null;
}

function CheckAllBoxesInGroup(s_master,s_slaves) {
    this.s_master = s_master;
    this.s_slaves = s_slaves;    
    this.checkMaster = function() {
	    a_cb = new Array;
	    a_cb = this.s_slaves.split(",");
	    SetCheck(this.s_master);
	
    	for(var n = 0; n < a_cb.length; n++) {
	        if(!(IsChecked(a_cb[n]))){
	            //alert(a_cb[n]);
    	        ClearCheck(this.s_master);
	            break;
            }    
        }	
        a_cb=null;
    }
    this.checkMaster();
    this.click = function() {
        CheckAllInList(IsChecked(this.s_master),this.s_slaves);
    }
}

function CheckAllInList(b_checked,cb_list,b_warn)
{
	a_cb = new Array;
	a_cb = cb_list.split(",");
	for(var n = 0; n < a_cb.length; n++) {
	    o_cb = getObject(a_cb[n])
		if(o_cb) {
		    o_cb.checked = b_checked;
		}else {
            if(b_warn) {alert(a_cb[n] + ' cb not defined')};
		}
	}	
	o_cb=null;
	a_cb=null;
}
function SetContentsInList(s_value,s_list)
{
	a_list = new Array;
	a_list = s_list.split(",");
	for(var n = 0; n < a_list.length; n++) {
	    setContents(a_list[n],s_value);
	}	
	a_list=null;
}
function DisplayContentsInList(b_display, s_list)
{
	a_list = new Array;
	a_list = s_list.split(",");
	for(var n = 0; n < a_list.length; n++) {
	    if(b_display){
	        ShowControl(a_list[n]);
	    }else{
	        HideControl(a_list[n]);	    
	    }
	}	
	a_list=null;
}
    
function selectID(s_link) {
	o_link = getObject(s_link); 
	if(o_link) {
		o_link.className="current";
	}
	return o_link;	
}

function showID(s_new) {
	o_new = getObject(s_new);       
	if(o_new) {
       		o_new.style.display = "block";
	}
	return o_new;
}

function showInit(caller,o_init,o_curr) {
	ToElement = event.toElement;
	IsInside = caller.contains(ToElement);
	
	if (IsInside) { 
		return 0;
	}

	if(o_curr) {
	       		o_curr.style.display = "";
	}
	if(o_init) {
       		o_init.style.display = "block";
	}
	return 1;

}

function selectInit(caller,o_init,o_curr) {

	ToElement = event.toElement;
//alert("showthis" + event.type);
//	alert("to elem " + ToElement);
	IsInside = caller.contains(ToElement);
//	alert("is in " + IsInside);
//	alert("call " + caller);
	
	if (IsInside) { 
		return 0;
	}
	
	if(o_curr) {
       		o_curr.className = "";
	}
	if(o_init) {
       		o_init.className = "current";
	}
	return 1;
}


function hideCurrent(o_current) {	
			if(o_current) {
				o_current.style.display = "";
			}
}

function deSelectCurrent(o_current) {
			if(o_current) {
				o_current.className= "";
			}
}

function SwapImageID(s_image,s_imagefile) {
	o_image = getObject(s_image);
	o_image.src = s_imagefile;
}

function SwapImage(o_image,s_imagefile) {
//alert("before");
	o_image.src = s_imagefile;
//	alert(s_imagefile);
}


function ToggleControl (s_showControlID,s_hideControlID,s_display) {
o_showControl=getObject(s_showControlID);
if(o_showControl) {o_showControl.style.display=s_display;}
o_hideControl=getObject(s_hideControlID);
if(o_hideControl) {o_hideControl.style.display='none';}
o_showControl = null; o_hideControl = null;
}

function ToggleDisplay(s_ControlID,s_display) {
    if(IsVisible(s_ControlID)) {
        HideControl(s_ControlID);
    }else{
        ShowControl(s_ControlID,s_display);
    }
}

//s_displayStyle can be: block (like a div), inline (like a span), not set makes it default
function ShowControl (s_controlID, s_displayStyle) {
    o_showControl = getObject(s_controlID);
    if(!(s_displayStyle)) {s_displayStyle='';}
    if(o_showControl) {
		o_showControl.style.display=s_displayStyle;
    }
    o_showControl = null;
}

function HideControl(s_hideControlID) {
	o_hide = getObject(s_hideControlID);
	if(o_hide) {o_hide.style.display='none';}
	o_hide=null;
}

function IsVisible(s_control) {
    o_control=getObject(s_control);
    if(!(o_control)) {return false;}
    if(o_control.style.display=='none') {
        return false;
    }else{
        return true;
    }
    o_control=null;
}
//This used to make buttons only clickable once unless page is invalid
function EnableButtonIfPageInvalid(s_btn,s_errSummary){
    if(IsVisible(s_errSummary)) {
        ToggleControl(s_btn,s_btn + 'Disabled','');                                                        
    }
}
function DisableButtonIfPageValid(s_btn,s_errSummary){
    ToggleControl(s_btn + 'Disabled',s_btn,'');
    setTimeout("EnableButtonIfPageInvalid('" + s_btn + "','" + s_errSummary + "')",1000);
}

function SelectDropByIndex(s_drop,s_index) {
    o_drop=getObject(s_drop);
    if(o_drop) {
    o_drop.selectedIndex=s_index;
    }
    o_drop=null;
}

function SelectDropByValue(s_drop,s_value) {
    o_drop=getObject(s_drop);
    for(i=0;i<o_drop.options.length;i++) {
        if(o_drop.options[i].value==s_value) {o_drop.options[i].selected=true;}else{o_drop.options[i].selected=false;}
    }
    o_drop=null;
}

function GetDropValue(s_drop) {
    o_drop=getObject(s_drop);
    return o_drop.options[o_drop.selectedIndex].value;
    o_drop=null;
}
function GetRBLSelectedIndex(rbl) {
    chosen = -1
    len = rbl.length

    for (i = 0; i <len; i++) {
        if (rbl[i].checked) {
 //           chosen = document.f1.r1[i].value;
           chosen = i;
        }
    }
    return chosen; 
}
function setRBLSelectedIndex(rbl,index) {
    if(!(rbl)) {return;}
    len = rbl.length

    for (i = 0; i <len; i++) {
        if (i==index) {
           rbl[i].checked=true;
        }else{
            rbl[i].checked=false
        }
    }
}
function GetRBLSelectedValue(rbl) {
    chosen = ""
    len = rbl.length
    for (i = 0; i <len; i++) {
        if (rbl[i].checked) {
           chosen = rbl[i].value;
        }
    }
    return chosen; 
}
function setRBLSelectedValue(rbl,value) {
    len = rbl.length
    for (i = 0; i <len; i++) {
        if (value==rbl[i].value) {
           rbl[i].checked=true;
        }else{
           rbl[i].checked=false;        
        }
    }
    return chosen; 
}


function ReplaceSource(s_drop,s_source) {        
    //myform = document.getElementById('CMSimportForm');
    //myrblcomm = myform.CommunityNumber;
    o_drop = document.getElementById(s_drop);
    if(! s_source) {s_source='Source';}
    o_source =  document.getElementById(s_source);
    for(i=0;i<o_drop.options.length;i++) {
        o_source.options[i]=null;
    }
//    alert(o_source.options[0].text);
        o_source.options[0] = new Option(o_drop.options[0].text,o_drop.options[0].value);
    for(i=0;i<o_drop.options.length;i++) {
        o_source.options[i] = new Option(o_drop.options[i].text,o_drop.options[i].value);
        o_source.options[i].style.color = o_drop.options[i].style.color;
    }
    o_source=null;
    o_drop=null;
}

function ToggleCheckBox(s_cb) {
o_cb=getObject(s_cb);
if(o_cb.checked) {o_cb.checked=false;}
else {o_cb.checked=true;
}
}

function ShowIfHidden(s_show,s_display)
{
	o_show = getObject(s_show);
	if(o_show.style.display=='none') {
			ShowControl(s_show,s_display);
	}else{
			HideControl(s_show);
	}			
}

function ShowOnDropChange(drop_caller,s_target,index,s_display) {
	o_target = document.getElementById(s_target);
	if(drop_caller.selectedIndex == index) {
		o_target.style.display = s_display;
	}else {
		o_target.style.display = "none";
	}
}
function HideOnDropChange(drop_caller,s_target,index,s_display) {
	o_target = document.getElementById(s_target);
	if(drop_caller.selectedIndex == index) {
		o_target.style.display = "none";
	}else {
		o_target.style.display = s_display;
	}
}

function ToggleOnDropChange(drop_caller,s_target,index,s_display) {
	o_target = document.getElementById(s_target);
	if(drop_caller.selectedIndex == index) {
		o_target.style.display = s_display;
		drop_caller.style.display = "none";
	}
}

function ClearSelectToggle(s_drop,s_text,s_display) {
o_drop=getObject(s_drop);
o_text=getObject(s_text);
if(o_drop.length>1) {ToggleControl(s_drop,s_text,s_display);o_text.value="";if(o_drop.length==o_drop.selectedIndex+1) {o_drop.selectedIndex=0}}
}

function ClearSelect(s_drop) {
o_drop=getObject(s_drop);
o_drop.selectedIndex =  -1;
o_drop = null;
}
function ClearDrop(s_drop) {
o_drop=getObject(s_drop);
if(o_drop){o_drop.selectedIndex =  0;}
o_drop = null;
}

function ClearText(s_text) {
o_text=getObject(s_text);
o_text.value =  "";
o_text=  null;
}
function SetText(s_text,s_value) {
o_text=getObject(s_text);
o_text.value =  s_value;
o_text=  null;
}

function ClearCheck(s_check) {
o_check=getObject(s_check);
o_check.checked = false;
o_check=  null;
}
function SetCheck(s_check,b_state) {
if(typeof(b_state)=="undefined") {b_state=true}
o_check=getObject(s_check);
o_check.checked = b_state;
o_check=  null;
}
function RemoveWhiteSpaceMakeUCase(s_input) {
    s_value=getValue(s_input);
    s_value=s_value.replace(/ /,'');
    s_value=s_value.toUpperCase();
    setValue(s_input,s_value);
    return s_value;
}

function GetCurrentDateOnly() {
    now = new Date();
    return (now.getMonth() +  1) + '/' + now.getDate() + '/' + now.getFullYear();
}

function CountLBSelections(o_lb) {
    c=0;
      for (var n = 0; n < o_lb.options.length ; n++)
          {
              if (o_lb.options[n].selected == true) {c=c+1;}
          }    
    return c;
}
function GetLBSelectionsString(o_lb) {
      s='';
      for (var n = 0; n < o_lb.options.length ; n++)
          {
              if (o_lb.options[n].selected == true) {
                    s+=o_lb.options[n].value+",";
              }
          }
          s=s.replace(/,$/, '');
    return s;
}
function ToggleIfTrue(b_true,s_show,s_hide,s_display)
{
	if(b_true) {
			ToggleControl(s_show,s_hide,s_display);
	}else{
			ToggleControl(s_hide,s_show,s_display);
	}			

}

function ToggleIfChecked(s_cb,s_show,s_hide,s_display)
{
	o_cb = getObject(s_cb);
	if(! o_cb) {return;}
	if(o_cb.checked) {
			ToggleControl(s_show,s_hide,s_display);
	}else{
			ToggleControl(s_hide,s_show,s_display);
	}			
}

function ToggleIfTextFilled(s_text,s_show,s_hide,s_display)
{
	o_text = getObject(s_text);
	if (o_text.value.length == 0) {
			ToggleControl(s_hide,s_show,s_display);
	}else{
			ToggleControl(s_show,s_hide,s_display);
	}
	o_text=null;
}

function PostbackSelectToggle(s_drop,s_text,s_display){
			if(getObject(s_drop)) {o_drop=getObject(s_drop)};
			var o_text = getObject(s_text);
			if(getObject(s_text)) {
 //				if(getObject(s_text).value || getObject(s_drop).length==1) {
				if(getObject(s_text).value || o_drop.length==o_drop.selectedIndex+1) {
					ToggleControl(s_text,s_drop,s_display)
				}else{
					if(o_drop.length==o_drop.selectedIndex+1) {o_drop.selectedIndex=0}
				}
			}
}

//function setCursorAtEnd(sTextboxID) {    var oTextbox = document.all.item(sTextboxID);    if (oTextbox .createTextRange) {        var r = (oTextbox.createTextRange());        r.moveStart('character', (oTextbox.value.length));        r.collapse();        r.select();    }}

function setCursorAtEnd(sTextboxID) { 
   o_text = getObject(sTextboxID);
   if(IsVisible(sTextboxID)) {o_text.focus();}else{return;}
       if (o_text.createTextRange) {
               var r = (o_text.createTextRange());
               r.moveStart('character', (o_text.value.length));
               r.collapse();        
               r.select();    
               }
}
function setCursorAtStart(sTextboxID) {
   o_text = getObject(sTextboxID);
   if(IsVisible(sTextboxID)) {o_text.focus();}else{return false;}
       if (o_text.createTextRange) {
               var r = (o_text.createTextRange());
               r.moveStart('character', 0);
               r.collapse();
               r.select();
       }
}

function enableRequiredIfChecked(s_cb,req_list)
{
	o_cb = getObject(s_cb);
	if(!(o_cb)) {return;}
	a_req = new Array;
	a_req = req_list.split(",");
	for(var n = 0; n < a_req.length; n++) {
		if(getObject(a_req[n])) {
			o_req = getObject(a_req[n])
			if(o_cb.checked) {
				o_req.enabled = "True"
			}else{
				o_req.enabled = "False"			
			}			
		}
	}	
	//Need this aspx function to run to read in new required field state
	ValidatorOnLoad();
}

	
function disableRequiredIfChecked(s_cb,req_list)
{
	o_cb = getObject(s_cb);
	a_req = new Array;
	a_req = req_list.split(",");
	for(var n = 0; n < a_req.length; n++) {
		if(getObject(a_req[n])) {
			o_req = getObject(a_req[n])
			if(o_cb.checked) {
				o_req.enabled = "False"			
			}else{
				o_req.enabled = "True"
			}			
		}
	}	
	//Need this aspx function to run to read in new required field state
	ValidatorOnLoad();	
}

function enableIfIsTextbox(s_drop,req_list) {
	var o_drop = getObject(s_drop);
	var a_req = new Array;
	a_req = req_list.split(",");
	for(var n = 0; n < a_req.length; n++) {
	    var o_req = getObject(a_req[n]);
		if(o_req) {
			if(o_drop.length==o_drop.selectedIndex+1) {
				o_req.enabled = true;
			}else{
				o_req.enabled = false;		
			}
		}
		o_req = null;
	}	
	o_drop = null;
		
	//Need this aspx function to run to read in new required field state
ValidatorOnLoad();
}
function enableIfEmptyDrop(s_drop,req_list) {
	if(getObject(s_drop)) {o_drop = getObject(s_drop);}else{return}

		if(o_drop[o_drop.selectedIndex].value.length==0) {
				s_enabled = true;
			}else{
				s_enabled = false;			
			}			
	enableRequiredList(s_enabled,req_list)
}

//req_list is a csv list of validator IDs
function enableRequiredList(b_enable, req_list) {
	a_req = new Array;
	a_req = req_list.split(",");
	for(var n = 0; n < a_req.length; n++) {
			o_req = getObject(a_req[n]);
		if(o_req) {
			o_req.enabled = b_enable;
		}
		o_req=null;
	}	
	//Need this aspx function to run to read in new required field state
	ValidatorOnLoad();
}
function IsRequiredEnabled(s_id) {
    o_req = getObject(s_id);
    if(o_req) {
        return o_req.enabled;
    }
	o_req=null;
    return null;
}

function setRegValExp(s_reg,s_exp,s_msg)
{
//alert(s_reg + ' ' + s_exp + ' : ' + s_msg);
			o_reg = getObject(s_reg);
//			alert('1: ' + o_reg );
			o_reg.validationexpression = s_exp;
//			alert('2: ' + o_reg.validationexpression );
			o_reg.errormessage = s_msg;
	//Need this aspx function to run to read in new required field state
	ValidatorOnLoad();
}

//This is for setting up James's calendar on the page
				function SetupCalendar(s_text) {
				    //Don't use the calendar if user has Safari browser
                	var isMac = navigator.userAgent.indexOf("Mac") != -1;
	                var isSafari = navigator.userAgent.indexOf("Safari") != -1;
	                //setContents('lblDebug','<span style="color:white">HideShowAgent:' + navigator.userAgent + '</span>');
	                //setContents('lblDebug','<span style="color:black">HideShowAgent:</span>');
	                //alert(navigator.userAgent);
	                //if((isMac)||(isSafari)) {return ;}
	                if(isSafari) {return ;}

					if(getObject(s_text)) {
													Calendar.setup({
													inputField     :    s_text,      // id of the input field
													ifFormat       :    "%m/%d/%Y",       // format of the input field
													showsTime      :    false,            // will display a time selector
													button         :    s_text   // trigger for the calendar (button ID)
													});
					}								
				}													

//function that check for "Enter" key being hit
function FocusEnterKey(event,s_btn) {
	if (event.keyCode == 13) {
		if(document.getElementById(s_btn)) {
			document.getElementById(s_btn).focus();
		}
	}
}

function IsEnterKey(event,keycode) {
    if(!(event)) {event=window.event}
    if(!(keycode)) {keycode=event.keyCode}
    if(keycode==13) {return true}else{return false;}
}
function IsEscapeKey(event,keycode) {
    if(!(event)) {event=window.event}
    if(!(keycode)) {keycode=event.keyCode}
    if(keycode==27) {return true}else{return false;}
}
function IsTabKey(event,keycode) {
    if(!(event)) {event=window.event}
    if(!(keycode)) {keycode=event.keyCode}
    if(keycode==9) {return true}else{return false;}
}
function IsBackSpaceKey(event,keycode) {
    if(!(event)) {event=window.event}
    if(!(keycode)) {keycode=event.keyCode}
    if(keycode==8) {return true}else{return false;}
}
function IsCommaKey(event,keycode) {
    if(!(event)) {event=window.event}
    if(!(keycode)) {keycode=event.keyCode}
    if(keycode==188) {return true}else{return false;}
}
function IsHorizArrowKey(event,keycode) {
    if(!(event)) {event=window.event}
    if(!(keycode)) {keycode=event.keyCode}
    if((keycode==37)||(keycode==39)) {return true}else{return false;}
}
//These still work and kept for backwards compatibility.
function IsEnterKeyPass(keyCode) {
    if(keyCode==13) {return true}else{return false;}
}
function IsEscapeKeyPass(keyCode) {
    if(keyCode==27) {return true}else{return false;}
}

function setKeyDownEvent(s_body,s_btn) {
//alert(getObject(s_body).onkeydown);
    getObject(s_body).onkeydown = function () {FocusEnterKey(event,s_btn)};
//alert(getObject(s_body).onkeydown);
}

//This function should be set to keydown events to return to initial control in tab sequence
function returnToStartOfTabSequence(s_control) {
    if(event.which || event.keyCode){
        if ((event.which == 9) || (event.keyCode == 9)){
            o_control=getObject(s_control);
            o_control.focus();
            o_control=null;
            return false;
        }
    }else{
        return true;
    }
}

function focusOnLoad(obj) {
    if(obj) {}else{return;}
    obj.focus();
}

function placeFocus(s_override,s_exceptions) {
    if(s_override) {
        o_override = getObject(s_override);
        focusOnLoad(o_override);
        //alert(o_override.id);
        o_override = null;
        return;
    }
    a_exceptions = new Array;
    if(s_exceptions) {
            a_exceptions=s_exceptions.split(",");
    }
    if (document.forms.length > 0) {
        var field = document.forms[0];
        for (i = 0; i < field.length; i++) {
//This was making the first select focused. ie.the goto menu very obnoxious
//if ((field.elements[i].type == "text") || (field.elements[i].type == "textarea") || (field.elements[i].type.toString().charAt(0) == "s")) {
//alert(field.elements[i].name + field.elements[i].type );
            if ((field.elements[i].type == "text") || (field.elements[i].type == "textarea") || (field.elements[i].type.toString().charAt(0) == "s")) {
//pass exceptions you don't want to be selected that would make things annoyying
                exceptioncontinue=false;
                for (j = 0; j < a_exceptions.length; j++) {
                    if(field.elements[i].name == a_exceptions[j]) {exceptioncontinue=true;continue;}
                }    
                    if(exceptioncontinue) {continue};
    //don't let goto menu befirst selected. too obnoxious.
                if (/AdminNavigation1\$/.test(field.elements[i].name)) continue;
                if (/Topbanner1\$/.test(field.elements[i].name)) continue;  
                if (field.elements[i].style.display=="none") continue;                  
                field.elements[i].focus();
                if(field.elements[i].type == "text") setCursorAtEnd(field.elements[i].id);
                break;
            }
        }
    }
}

//function that lets javascript change the form variables
	function changeFormValue(s_name, s_value) {
		var o_input;
		o_input = document.getElementById(s_name);
		o_input.value = s_value;
	}

function calculateProduct(s_answer,s_input,f_input,i_decimal) {
 
 //alert(s_answer + s_input + f_input + i_decimal);
 
    //Get rid of any commas in the values    
    f_input0 = getObject(s_input).value;
    f_input0 = f_input0.toString().replace(/[,\$]/,'');
    f_input = f_input.toString().replace(/[,\$]/,'');
    getObject(s_answer).value = FormatFloat(f_input0 * f_input,i_decimal)
    
}
function calculateProductNew(s_answer,f_input0,f_input,i_decimal) {
  //alert(s_answer + s_input + f_input + i_decimal);

    //Get rid of any commas in the values    
    f_input0 = f_input0.toString().replace(/[,\$]/,'');
    f_input = f_input.toString().replace(/[,\$]/,'');
    //alert(f_input0 + ' ' + f_input );
    getObject(s_answer).value = FormatFloat(f_input0 * f_input,i_decimal)   
}

function FormatFloat(pFloat, pDp){
//    var m = Math.pow(10, pDp);
//    return parseInt(pFloat * m, 10) / m;
    return pFloat.toFixed(pDp)

}
function FormatCurrency(num) {
num = num.toString().replace(/\$|\,/g,'');
if(isNaN(num))
num = "0";
sign = (num == (num = Math.abs(num)));
num = Math.floor(num*100+0.50000000001);
cents = num%100;
num = Math.floor(num/100).toString();
if(cents<10)
cents = "0" + cents;
for (var i = 0; i < Math.floor((num.length-(1+i))/3); i++)
num = num.substring(0,num.length-(4*i+3))+','+
num.substring(num.length-(4*i+3));
return (((sign)?'':'-') + '$' + num + '.' + cents);
}

function FormatCurrencyNoCents(num) {
cents = num.toString().replace(/^[^\.]*/,'');
if(cents.length>3){cents=cents.substring(0,3)}
num = num.toString().replace(/\$|\,/g,'');
if(isNaN(num))
num = "0";
sign = (num == (num = Math.abs(num)));
num = Math.floor(num*100+0.50000000001);
num = Math.floor(num/100).toString();

for (var i = 0; i < Math.floor((num.length-(1+i))/3); i++)
num = num.substring(0,num.length-(4*i+3))+','+
num.substring(num.length-(4*i+3));
return (((sign)?'':'-') + '$' + num + cents);
}

function FormatNumber(num) {
cents = num.toString().replace(/^[^\.]*/,'');
if(cents.length>3){cents=cents.substring(0,3)}
num = num.toString().replace(/\$|\,/g,'');
if(isNaN(num))
num = "0";
sign = (num == (num = Math.abs(num)));
num = Math.floor(num*100+0.50000000001);
num = Math.floor(num/100).toString();

for (var i = 0; i < Math.floor((num.length-(1+i))/3); i++)
num = num.substring(0,num.length-(4*i+3))+','+
num.substring(num.length-(4*i+3));
return (((sign)?'':'-') + num + cents);
}

function dropSelectByValue(drop,value) {
    for (i = 0; i < drop.length; i++) {
        if (drop[i].value == value) {
           drop.selectedIndex = i;
        }
    }
}
function ValidateCClist(source,args) {
            args.IsValid = true;
    a_emails = new Array;
    //alert(args.Value);
    s_emails = args.Value.replace(/;/g,",");
    //alert(s_emails);
    a_emails = s_emails.split(",");
//    alert(a_emails.length);    
    for (i = 0; i < a_emails.length; i++) {
        a_emails[i]=Trim(a_emails[i]);
//        alert("email : " + a_emails[i]);
        if (a_emails[i].match(/^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$/)){  
           if(a_emails[i].match(/[\.]$/)) {
                args.IsValid=false;
            }
        }else{
            args.IsValid=false;
        }
    }
}
//same as function above, just different name
function ValidateEmailList(source,args) {
	ValidateCClist(source,args);
}

function ValidateCurrency(source,args) {
       args.IsValid=IsValidCurrency(args.Value);
}
function IsValidDate(s_value) {
    if(!(s_value)) {return true}
    if(/\d{1,2}\/\d{1,2}\/(\d{2}|\d{4})/.test(s_value)) {return true}
    return false;
}
function IsValidCurrency(s_value) {
    if(!(s_value)) {return true}
    if(/^-?\$?((([1-9]?[0-9]?,)?([0-9]{3},){0,3}[0-9]{3})|[0-9]{0,13})(\.[0-9]{0,2})?$/.test(s_value)) {return true}
    return false;
}

function Trim(string,character) {
    if(! character) {character=' '}
    re = new RegExp('^' + character + '*');
    string = string.replace(re,'');
    re = new RegExp(character + '*$');
    string = string.replace(re,'');
    return string;
}
function ValidateCuteEditor(source,args) {
            args.IsValid = true;
            cebody = getObject("ceBody");
            if (cebody && cebody.value.length > 0) {
                args.IsValid = true;
            }else{            
                args.IsValid = false;
            }
}

function GetChildren(s_form)
{
	var parent = document.getElementById(s_form);
	for (i=0; i<parent.childNodes.length; i++) {
			node = parent.childNodes[i];
	//alert(node.nodeName + node.name);
  }
}

function makeArray() {
     this.length = makeArray.arguments.length-1;
    for (i = 0; i < makeArray.arguments.length; i++)
        this[i] = makeArray.arguments[i];
}

function getAllLinks(el,a_links) {
    try {
//        a_links = new Array();                    
        //  if this is a link
        if (el.tagName && el.tagName.toLowerCase() == "a") {
            a_links.push(el);
        }
    }
    catch(E){ }
                
    if (el.childNodes && el.childNodes.length > 0) {
        for (var x = 0; x < el.childNodes.length; x++) {
            getAllLinks(el.childNodes[x],a_links);
        }
    }
}

function toggleDisabled(s_ID,b_force,setPersistDisabled) {    
    el = getObject(s_ID);
    if(! el) {return;}
//digs down and also disables controls
    try {
        myTagName = el.tagName.toLowerCase();
        if(myTagName) {        
            if(setPersistDisabled) {el.PersistDisabled=el.disabled}
            if(b_force==null) {
                el.disabled = el.disabled ? false : true;
            }else{
                el.disabled = b_force;                            
            }            
            //if(debug) {alert(el.disabled)}
            if(el.PersistDisabled) {el.disabled=true}
            //if(debug) {alert(el.disabled)}
        }        
    }
    catch(E){ }
    toggleDisabledControls(el,true,b_force,setPersistDisabled);

    el = null;
}

function toggleDisabledControls(el,childOnly,b_force,setPersistDisabled) {    
//works for a,select,input,textarea
//can't just disable every element cuz it crashes links in tables buggy
    try {
        myTagName = el.tagName.toLowerCase();
        if((myTagName)&&(!childOnly)) {
            myIsControl = false;  
            if(myTagName == "a") {myIsControl=true;}
            if(myTagName == "select") {myIsControl=true;}
            if(myTagName == "input") {myIsControl=true;}
            if(myTagName == "textarea") {myIsControl=true;}
            
            if(myIsControl) {
                //Now we are chekcing so that we don't do erroroneously try to set this twice and stuff doesn't enable correctly
                if (typeof(el.PersistDisabledAlreadySet)=='undefined') {el.PersistDisabledAlreadySet=false;}
                if(setPersistDisabled && (!el.PersistDisabledAlreadySet)) {el.PersistDisabled=el.disabled}
                if(b_force==null) {
                    el.disabled = el.disabled ? false : true;
                }else{
                    el.disabled = b_force;
                }
                if(el.PersistDisabled) {el.disabled=true}
                //If html is not proper sometimes controls are examined twice don't let this happen
                if(setPersistDisabled) {
                    el.PersistDisabledAlreadySet=true;
                }else{
                    el.PersistDisabledAlreadySet=false;
                }

            }
            if(myTagName == "a") {
                if(el.disabled) {
                    if (typeof(el.onclickOLD)=='undefined') {
                        el.onclickOLD = el.onclick;}                    
                    if (typeof(el.hrefOLD)=='undefined') {
                        el.hrefOLD = el.href;}                    
                    el.onclick = function anonymous(){return false;};
                    el.href = "#";
                    //alert(el.id + el.href + el.onclick);
                }else{
                    //el.onclick=el.onclickOLD ;
                    //el.href= el.hrefOLD;
                    if (typeof(el.onclickOLD) != 'undefined') {
                        el.onclick = el.onclickOLD;}
                    if (typeof(el.hrefOLD) != 'undefined') {
                        el.href= el.hrefOLD;}
                    
                }
            }
        }
    }
    catch(E){ }
                
    if (el.childNodes && el.childNodes.length > 0) {
        for (var x = 0; x < el.childNodes.length; x++) {
            toggleDisabledControls(el.childNodes[x],false,b_force,setPersistDisabled);
        }
    }
}

function IsPartialPostback() {
    return Sys.WebForms.PageRequestManager.getInstance()._postBackSettings.async==true || Sys.WebForms.PageRequestManager.getInstance().get_isInAsyncPostBack()==true;
}

function clearForm(s_ID) {
    o_element = getObject(s_ID);
    clearFormByObj(o_element);
    o_element = null;
}
function clearFormByObj(el) {    
//works for a,select,input,textarea
//can't just disable every element cuz it crashes links in tables buggy
    try {
        myTagName = el.tagName.toLowerCase();
        if(myTagName) {
            //alert(myTagName);
            if(myTagName=="select") {
                if(el.multiple) {
                    el.selectedIndex =  -1;
                }else{
                    el.selectedIndex =  0;
                }
            }    
            if(myTagName=="input") {
                if(el.type=="checkbox") {el.checked=false;}
                if(el.type=="radio") {el.checked=false;}
                if(el.type=="text") {el.value="";}
                if(el.type=="password") {el.value="";}
            }            
            if(myTagName == "textarea") {
                if("value" in el) {el.value=""}                
            }
            
            if(myTagName == "span") {
//alert(myTagName  + " id: " + el.id + " class: " + el.className);
                if(el.className=="validator") {el.style.display="none"}
            }
        }
    }
    catch(E){ }
                
    if (el.childNodes && el.childNodes.length > 0) {
        for (var x = 0; x < el.childNodes.length; x++) {
            clearFormByObj(el.childNodes[x]);
        }
    }
}

function AddToSeperatedList(add,list,sep) {
    add=Trim(add);
    if(!(add)) {return list} 

    b_repeat = false;
    a_items = new Array;
	a_items  = list.split(sep);
	for(var n = 0; n < a_items.length; n++) {
		if(Trim(a_items[n])==add) {
		    b_repeat=true;
		    break;
		}
	}	
    if(!(b_repeat)) {
        if(list) {list += sep;}
        list += add;
    }
    return list;
}
function RemoveFromSeperatedList(remove,list,sep) {
    remove=Trim(remove);
    if(!(remove)) {return list} 

    s_keep = '';
    a_items = new Array;
	a_items  = list.split(sep);
	for(var n = 0; n < a_items.length; n++) {
	    s_email = Trim(a_items[n]);
		if(!(s_email==remove)) {
            if(s_keep) {s_keep += sep;}
            s_keep += s_email;
		}
	}
    return s_keep;
}
function ConvertToCurrencyAsTyping(o_text) {
    if((o_text.value.length>0)&&(o_text.value!='$')&&(o_text.value!='-')&&(o_text.value!=',')&&(!(IsCommaKey()))&&(!(IsHorizArrowKey()))&&(!(IsBackSpaceKey()))){o_text.value=FormatCurrencyNoCents(o_text.value)}
}
function ConvertToNumberWithCommasAsTyping(o_text) {
    if((o_text.value.length>0)&&(o_text.value!='-')&&(o_text.value!=',')&&(!(IsCommaKey()))&&(!(IsHorizArrowKey()))&&(!(IsBackSpaceKey()))){o_text.value=FormatNumber(o_text.value)}
}
function CurrencyGreaterThanZero(sender,args) {
    args.IsValid=false;
    if(args.Value.replace(/[\,\$]/g,'')>=0) {args.IsValid=true;};
}

var submitClicked=false;
var disableLinkButtonsList;
var disableInputButtonsList;
var disableCancelButtonsList;

function disableSubmitWrapper(source,args) {
//			disableSubmit();
			args.IsValid = true;
}

function disableSubmit() {
	if (submitClicked) {return ;}

	disableLinkButtons();
	disableInputButtons();

	submitClicked=true;

}

function disableLinkButtons() {
//functions that is called by asp.net validator to disable buttons eliminating double click
//disables both buttons with validators and those without validators
//ie call non validators cancel buttons
	comma = "";
	if((disableLinkButtonsList)&&(disableCancelButtonsList)) {
		comma = ",";
	}
	buttonList = disableLinkButtonsList + comma + disableCancelButtonsList;

	a_btn = new Array;
	a_btn= buttonList.split(",");
	
	for(var n = 0; n < a_btn.length; n++) {
		if(getObject(a_btn[n])) {
				getObject(a_btn[n]).href="#";
		}
	}
													
}

function disableInputButtons(disableInputButtonsList,disable) {
//function called from onsubmit event of form
	a_btn = new Array;
	a_btn= disableInputButtonsList.split(",");
	
	for(var n = 0; n < a_btn.length; n++) {
		if(getObject(a_btn[n])) {
				getObject(a_btn[n]).disabled=disable;
				getObject(a_btn[n]).onclick=null;
		}
	}
													
}

function disableButtonsOnCancelClick(clicked) {
	if (submitClicked) {return false;}

	disableSubmit();
	__doPostBack(clicked,'');
}

function setupDisableSubmit() {
//use to put call to disable buttons on the on click event of non validating buttons
	a_btn = new Array;
	a_btn= disableCancelButtonsList.split(",");

		for(var n = 0; n < a_btn.length; n++) {		
			if(getObject(a_btn[n])) {
				var button = a_btn[n];
				getObject(a_btn[n]).onclick= function anonymous() {disableButtonsOnCancelClick(this.id);};
			}
		}
	a_btn2 = new Array;
	a_btn2= disableLinkButtonsList.split(",");

		for(var n = 0; n < a_btn2.length; n++) {		
			if(getObject(a_btn2[n])) {
				var button = a_btn2[n];
				getObject(button).href = "javascript:{if (typeof(Page_ClientValidate) != 'function' ||  Page_ClientValidate()) disableButtonsOnCancelClick('" + button + "')}" ;
			}
		}

	a_btn3 = new Array;
	a_btn3= disableInputButtonsList.split(",");

		for(var n = 0; n < a_btn3.length; n++) {		
			if(getObject(a_btn3[n])) {
				var button = a_btn3[n];
				//if onclick exists we have a validating button
				if (getObject(button).onclick) {
					getObject(button).onclick = function anonymous() {if (typeof(Page_ClientValidate) == 'function') {if(Page_ClientValidate()) {disableButtonsOnCancelClick(this.id);}else{return false}} };
//					alert("onclick " + button + " "+ getObject(button).onclick);
				}else {
					getObject(button).onclick= function anonymous() {disableButtonsOnCancelClick(this.id);};
				}
			}
		}

}

function concatObject(obj) {
  str='';
  for(prop in obj)
  {
    str+=prop + " value :"+ obj[prop]+"\n";
  }
  return(str);
}

//	var thisChild = parent1.firstChild;
//	while ( thisChild != parent1.lastChild )
//	{
//		if ( thisChild.nodeType == 1 )
//		{
//			alert(thisChild + thisChild.id);
//		}
//		thisChild = thisChild.nextSibling;
//	}



//end of clean
