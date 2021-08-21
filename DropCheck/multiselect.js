var curVisible=null;
var curObjID=null;

document.attachEvent('onclick',handleClick);

// Detect if the browser is IE or not.
// If it is not IE, we assume that the browser is NS.
var IE = document.all?true:false

// If NS -- that is, !IE -- then set up for mouse capture
if (!IE) document.captureEvents(Event.MOUSEMOVE)


function findPos(obj) {
	var curleft = curtop = 0;
	if (obj.offsetParent) {
		curleft = obj.offsetLeft
		curtop = obj.offsetTop
		while (obj = obj.offsetParent) {
			curleft += obj.offsetLeft
			curtop += obj.offsetTop
		}
	}
	return [curleft,curtop];
}

function placeDiv(objid) {
    if(curObjID == objid)
    { 
        removeDiv(curVisible); return; 
    }
    if(curVisible!=null) 
    { 
        removeDiv(curVisible); 
    }
    //debugger;
    var dd = document.getElementById('ContentPlaceHolder1_DropCheck1');
	var div = document.getElementById(objid+'div');
	var Parent1 = document.getElementById(objid+'Parent');
	
	var checkboxes = div.getElementsByTagName('input');
	var label = div.getElementsByTagName('label');
    var hidden_fields_array=dd.value.split('');
    var k=0;
	var d=0;
	for (i=0; i<checkboxes.length; i++) {
		label[i].style.color="black";
		label[i].style.background="transparent";
		for (k=0;k<hidden_fields_array.length;k++) 
		{
		    //hiddenFields[Trim(hidden_fields_array[i])]=true;
		    if(checkboxes[i].value==hidden_fields_array[k])
		    {
		        checkboxes[i].checked=true ;
		        label[i].style.color="red";
		        label[i].style.background="#FDFDCD";
		        d++;
		    }
		}
	}
	if(d==checkboxes.length)
	{
	    var mainchk=document.getElementById(objid + 'MainChk');
	    mainchk.checked=true ;
	}

	setLyr(dd,Parent1);
	showItem(Parent1);
	Parent1.focus();
	curVisible=Parent1;
	curObjID=objid;
}

function removeDiv(div) {
    //debugger ;
    //var dd  = document.getElementById(div.id.replace('Parent',''));
    var dd = document.getElementById('ContentPlaceHolder1_DropCheck1');
	var checkboxes = div.getElementsByTagName('input');
	var returnArray=new Array(0);
	var returnArray2=new Array(0); //to hold the selected Text not value
	
	for (i=0; i<checkboxes.length; i++) 
	{
	    if(checkboxes[i].title!="Check/Uncheck All")
	    {
		    if(checkboxes[i].checked) 
		    {
			    returnArray.push(checkboxes[i].value);
			    returnArray2.push(checkboxes[i].title);
		    }
		}
	}

dd.value= returnArray.join(',');
	
	hideItem(div);
	curVisible=null;
	curObjID=null;
	
	//__doPostBack(dd.getAttribute('alt'),'@@@AutoPostBack');
	PerformPostActions(dd.getAttribute('alt'));
}

function selectAll(div) {
    //debugger ;
    //var dd  = document.getElementById(div.id.replace('Parent',''));
    var dd = document.getElementById('ContentPlaceHolder1_DropCheck1');
	var label = div.getElementsByTagName('label');
	var checkboxes = div.getElementsByTagName('input');
	var returnArray=new Array(0);
	var returnArray2=new Array(0); //to hold the selected Text not value
	
	for (i=0; i<checkboxes.length; i++) 
	{
	    if(div.getElementsByTagName("input")[i].type == "checkbox")
	    {
	        if(checkboxes[i].title!="Check/Uncheck All")
	        {
	            checkboxes[i].checked=true ;
		        returnArray.push(checkboxes[i].value);
		        returnArray2.push(checkboxes[i].title);
		    }
		}
	}
	for (i=0; i<label.length; i++) 
	{
	    label[i].style.color="red";
		label[i].style.background="#FDFDCD";
	}
	
	dd.SelectedItem = returnArray.join('');
	
	//hideItem(div);
	//curVisible=null;
	//curObjID=null;
	
	//__doPostBack(dd.getAttribute('alt'),'@@@AutoPostBack');
	PerformPostActions(dd.getAttribute('alt'));
}

function selectNone(div) {
    //debugger ;
    //var dd  = document.getElementById(div.id.replace('Parent',''));
    var dd = document.getElementById('ContentPlaceHolder1_DropCheck1');
	var label = div.getElementsByTagName('label');
	var checkboxes = div.getElementsByTagName('input');
	var returnArray=new Array(0);
	var returnArray2=new Array(0); //to hold the selected Text not value
	
	for (i=0; i<checkboxes.length; i++) 
	{
	    if(div.getElementsByTagName("input")[i].type == "checkbox")
	    {
	        checkboxes[i].checked=false ;
		}
	}
	for (i=0; i<label.length; i++) 
	{
	    label[i].style.color="black";
		label[i].style.background="transparent";
	}

dd.value = '';
	
	//hideItem(div);
	//curVisible=null;
	//curObjID=null;
	
	//__doPostBack(dd.getAttribute('alt'),'@@@AutoPostBack');
	PerformPostActions(dd.getAttribute('alt'));
}

function handleTopCheck(obj)
{
    if(obj.checked==true )
    {
        selectAll(curVisible)
    }
    else
    {
        selectNone(curVisible)
    }
}

function searchItem(div,obj) {
    //debugger ;
    //var dd  = document.getElementById(div.id.replace('Parent',''));
    var dd = document.getElementById('ContentPlaceHolder1_DropCheck1');
	var label = div.getElementsByTagName('label');
	var checkboxes = div.getElementsByTagName('input');
	var returnArray=new Array(0);
	var returnArray2=new Array(0); //to hold the selected Text not value
	
	for (i=0; i<checkboxes.length; i++) 
	{
	    //debugger;
	    if(div.getElementsByTagName("input")[i].type == "checkbox")
	    {
	        if(checkboxes[i].title != "Check/Uncheck All")
	        {
	            if (checkboxes[i].title.replace("''", "").toLowerCase().match(obj.value.replace("''", "").toLowerCase()) != null)
	            {
	                var chkrow  = document.getElementById(checkboxes[i].id + 'row');
	                chkrow.style.height='20px';
	                chkrow.style.display='';
		        }
		        else 
		        {
		            var chkrow  = document.getElementById(checkboxes[i].id + 'row');
		            chkrow.style.height='0px';
	                chkrow.style.display='none';
		        }
		    }
		}
	}
	
	PerformPostActions(dd.getAttribute('alt'));
	//alert(div.id);
	//alert(obj.value);
}

function setLyr(obj,lyr) {
	var coors = findPos(obj);
	coors[1] += 21;
	lyr.style.top = coors[1] + 'px';
	lyr.style.left = coors[0] + 'px';
}

function showItem(obj) {
	obj.style.visibility='visible';
}

function hideItem(obj) {
	obj.style.visibility='hidden';
}

function handleClick() {
	if(curVisible!=null) //is a div visible?
	{
		var r = {l: curVisible.offsetLeft, t: curVisible.offsetTop, r: curVisible.offsetWidth, b: curVisible.offsetHeight};
		var curVisibleOP = curVisible.offsetParent; 
		r.l += curVisibleOP.offsetLeft;
		r.t += curVisibleOP.offsetTop;
		r.r += (r.l);
		r.b += r.t;
		r.t -= 22;
		
		var p = getMouseXY(document);
		
		if( (p.x>r.r) || (p.x<r.l) || (p.y>r.b) || (p.y<r.t) )  //no hit!
		{
			removeDiv(curVisible);
		}
	}	
}



function getMouseXY(e) {
  if (IE) { // grab the x-y pos.s if browser is IE
    tempX = event.clientX + document.body.scrollLeft;
    tempY = event.clientY + document.body.scrollTop;
  } else {  // grab the x-y pos.s if browser is NS
    tempX = e.pageX;
    tempY = e.pageY;
  }  
  if (tempX < 0){tempX = 0;}
  if (tempY < 0){tempY = 0;}  
  return {x: tempX, y: tempY};
}
