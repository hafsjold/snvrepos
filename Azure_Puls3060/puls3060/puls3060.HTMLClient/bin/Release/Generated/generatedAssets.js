﻿function Mod10(n,t,i){var p=n.getUTCFullYear().toString().substr(2,2),w=padDigits((n.getMonth()+1).toString(),2),b=padDigits(n.getDate().toString(),2),o=p+w+b+padDigits(t,5)+padDigits(i,3),k="0123456789",s=o.length,u=parseInt(o),c=o.toString(),e,h,y;c=c.replace(/^\s+|\s+$/g,"");var l=0,a=!0,f=!1,v,r;for(e=0;e<s;e++)v=""+c.substring(e,e+1),k.indexOf(v)=="-1"&&(a=!1);if(a||(f=!1),s==0&&f)f=!1;else if(s==14){for(h=s;h>0;h--){r=parseInt(u)%10,r=parseInt(r),r=r*2;switch(r){case 10:r=1;break;case 12:r=3;break;case 14:r=5;break;case 16:r=7;break;case 18:r=9;break;default:r=r}l+=r,h--,u=u/10,r=parseInt(u)%10,r=parseInt(r),l+=r,u=u/10}y=10-l%10,f=o.toString()+y.toString()}return f}function padDigits(n,t){return Array(Math.max(t-String(n).length+1,0)).join(0)+n}window.myapp=msls.application,function(n){function e(n){i.call(this,n)}function t(n){i.call(this,n)}function o(n){i.call(this,n)}function c(n){s.call(this,n)}function l(n){s.call(this,n)}function y(){a.call(this)}var i=msls.Entity,s=msls.DataService,a=msls.DataWorkspace,r=msls._defineEntity,h=msls._defineDataService,v=msls._defineDataWorkspace,u=msls.DataServiceQuery,f=msls._toODataString;msls._addToNamespace("msls.application",{tblMedlem:r(e,[{name:"Navn",type:String},{name:"Kaldenavn",type:String},{name:"Adresse",type:String},{name:"Postnr",type:String},{name:"Bynavn",type:String},{name:"Telefon",type:String},{name:"Email",type:String},{name:"Status",type:Number},{name:"Nr",type:Number},{name:"Kon",type:String},{name:"FodtDato",type:Date},{name:"Bank",type:String},{name:"tblFikBetalings",kind:"virtualCollection",elementType:t}]),tblFikBetaling:r(t,[{name:"Id",type:Number},{name:"tblProjekt",kind:"reference",type:o},{name:"tblMedlem",kind:"virtualReference",type:e},{name:"Belob",type:String},{name:"BetalingsDato",type:Date},{name:"FIKnr",type:String},{name:"tblMedlem_Nr",type:Number},{name:"CreatedBy",type:String,isReadOnly:!0},{name:"Created",type:Date,isReadOnly:!0},{name:"ModifiedBy",type:String,isReadOnly:!0},{name:"Modified",type:Date,isReadOnly:!0},{name:"RowVersion",type:Array}]),tblProjekt:r(o,[{name:"Navn",type:String},{name:"Projektnr",type:Number},{name:"Id",type:Number},{name:"tblFikBetalings",kind:"collection",elementType:t},{name:"CreatedBy",type:String,isReadOnly:!0},{name:"Created",type:Date,isReadOnly:!0},{name:"ModifiedBy",type:String,isReadOnly:!0},{name:"Modified",type:Date,isReadOnly:!0},{name:"RowVersion",type:Array}]),dbPuls3060MedlemData:h(c,n.rootUri+"/dbPuls3060MedlemData.svc",[{name:"tblMedlems",elementType:e}],[{name:"tblMedlems_SingleOrDefault",value:function(t){return new u({_entitySet:this.tblMedlems},n.rootUri+"/dbPuls3060MedlemData.svc/tblMedlems(Nr="+f(t,"Int32?")+")")}}]),ApplicationData:h(l,n.rootUri+"/ApplicationData.svc",[{name:"tblFikBetalings",elementType:t},{name:"tblProjekts",elementType:o}],[{name:"tblFikBetalings_SingleOrDefault",value:function(t){return new u({_entitySet:this.tblFikBetalings},n.rootUri+"/ApplicationData.svc/tblFikBetalings(Id="+f(t,"Int32?")+")")}},{name:"tblProjekts_SingleOrDefault",value:function(t){return new u({_entitySet:this.tblProjekts},n.rootUri+"/ApplicationData.svc/tblProjekts(Id="+f(t,"Int32?")+")")}}]),DataWorkspace:v(y,[{name:"dbPuls3060MedlemData",type:c},{name:"ApplicationData",type:l}])})}(msls.application),function(n){function u(i,r){r||(r=new n.DataWorkspace),t.call(this,r,"AddEditBetalinger",i)}function f(i,r){r||(r=new n.DataWorkspace),t.call(this,r,"BrowseBetalingers",i)}function e(i,r){r||(r=new n.DataWorkspace),t.call(this,r,"ViewBetalinger",i)}function o(i,r){r||(r=new n.DataWorkspace),t.call(this,r,"AddEditProjekt",i)}function s(i,r){r||(r=new n.DataWorkspace),t.call(this,r,"BrowseProjekts",i)}function h(i,r){r||(r=new n.DataWorkspace),t.call(this,r,"ViewProjekt",i)}function c(i,r){r||(r=new n.DataWorkspace),t.call(this,r,"Medlemmer",i)}var t=msls.Screen,i=msls._defineScreen,l=msls.DataServiceQuery,a=msls._toODataString,r=msls._defineShowScreen;msls._addToNamespace("msls.application",{AddEditBetalinger:i(u,[{name:"tblFikBetaling",kind:"local",type:n.tblFikBetaling}],[]),BrowseBetalingers:i(f,[{name:"tblFikBetalings",kind:"collection",elementType:n.tblFikBetaling,createQuery:function(){return this.dataWorkspace.ApplicationData.tblFikBetalings.expand("tblProjekt")}}],[]),ViewBetalinger:i(e,[{name:"tblFikBetaling",kind:"local",type:n.tblFikBetaling}],[]),AddEditProjekt:i(o,[{name:"tblProjekt",kind:"local",type:n.tblProjekt}],[]),BrowseProjekts:i(s,[{name:"tblProjekts",kind:"collection",elementType:n.tblProjekt,createQuery:function(){return this.dataWorkspace.ApplicationData.tblProjekts}}],[]),ViewProjekt:i(h,[{name:"tblProjekt",kind:"local",type:n.tblProjekt}],[]),Medlemmer:i(c,[{name:"tblMedlems",kind:"collection",elementType:n.tblMedlem,createQuery:function(){return this.dataWorkspace.dbPuls3060MedlemData.tblMedlems.filter("(Status ne null) and (Status gt 0)").orderBy("Kaldenavn")}}],[]),showAddEditBetalinger:r(function(t,i){var r=Array.prototype.slice.call(arguments,0,1);return n.showScreen("AddEditBetalinger",r,i)}),showBrowseBetalingers:r(function(t){var i=Array.prototype.slice.call(arguments,0,0);return n.showScreen("BrowseBetalingers",i,t)}),showViewBetalinger:r(function(t,i){var r=Array.prototype.slice.call(arguments,0,1);return n.showScreen("ViewBetalinger",r,i)}),showAddEditProjekt:r(function(t,i){var r=Array.prototype.slice.call(arguments,0,1);return n.showScreen("AddEditProjekt",r,i)}),showBrowseProjekts:r(function(t){var i=Array.prototype.slice.call(arguments,0,0);return n.showScreen("BrowseProjekts",i,t)}),showViewProjekt:r(function(t,i){var r=Array.prototype.slice.call(arguments,0,1);return n.showScreen("ViewProjekt",r,i)}),showMedlemmer:r(function(t){var i=Array.prototype.slice.call(arguments,0,0);return n.showScreen("Medlemmer",i,t)})})}(msls.application),myapp.AddEditBetalinger.beforeApplyChanges=function(n){var t=n.tblFikBetaling.BetalingsDato,i=n.tblFikBetaling.tblMedlem_Nr,r=n.tblFikBetaling.tblProjekt.Projektnr;n.tblFikBetaling.FIKnr=Mod10(t,i,r)},myapp.Medlemmer.created=function(){$.getJSON("../Perms/UserPermissions/",function(n){myapp.permissions=n})},myapp.BrowseProjekts.created=function(n){n.findContentItem("AddProjekt").isEnabled=myapp.permissions["LightSwitchApplication:CanInsertProjekt"]?!0:!1},myapp.ViewProjekt.Details_postRender=function(n,t){var i=t.screen.tblProjekt.details.getModel()[":@SummaryProperty"].property.name;t.dataBind("screen.tblProjekt."+i,function(n){t.screen.details.displayName=n})},myapp.ViewProjekt.created=function(n){n.findContentItem("EditProjekt").isEnabled=myapp.permissions["LightSwitchApplication:CanUpdateProjekt"]?!0:!1};