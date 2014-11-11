/*
* By default, Selenium looks for a file called "user-extensions.js", and loads and javascript
* code found in that file. This file is a sample of what that file could look like.
** user-extensions.js provides a convenient location for adding extensions to Selenium, like
* new actions, checks and locator-strategies.
* By default, this file does not exist. Users can create this file and place their extension code
* in this common location, removing the need to modify the Selenium sources, and hopefully assisting
* with the upgrade process.
* You can find contributed extensions at http://wiki.openqa.org/display/SEL/Contributed%20User-Extensions
*/

Selenium.prototype.doStorePassword = function(variableName){
    password =  'password11' ;	
    storedVars[variableName] = password;
}

Selenium.prototype.doStorefname= function(variableName){
    fname = 'first_name';
	storedVars[variableName] = fname; 
}

Selenium.prototype.doStorelname = function(variableName){
      lname = 'last_name';
	  storedVars[variableName] = lname; 
}
	  
	  Selenium.prototype.doStoretel = function(variableName){
      tel = '111-111-1111';
	  storedVars[variableName] = tel; 
}





