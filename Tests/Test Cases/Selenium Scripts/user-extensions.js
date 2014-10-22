/*
* By default, Selenium looks for a file called "user-extensions.js", and loads and javascript
* code found in that file. This file is a sample of what that file could look like.
*
* user-extensions.js provides a convenient location for adding extensions to Selenium, like
* new actions, checks and locator-strategies.
* By default, this file does not exist. Users can create this file and place their extension code
* in this common location, removing the need to modify the Selenium sources, and hopefully assisting
* with the upgrade process.
*
* You can find contributed extensions at http://wiki.openqa.org/display/SEL/Contributed%20User-Extensions
*/

// The following examples try to give an indication of how Selenium can be extended with javascript.

// All do* methods on the Selenium prototype are added as actions.
// E.g.add a typeRepeated action to Selenium, which types the text twice into a text box.
// The typeTwiceAndWait command will be available automatically


// Create the text to type


// All assert* methods on the Selenium prototype are added as checks.
// Eg add a assertValueRepeated check, that makes sure that the element value
// consists of the supplied text repeated.
// The verify version will be available automatically.



// All get* methods on the Selenium prototype result in
// store, assert, assertNot, verify, verifyNot, waitFor, and waitForNot commands.
// E.g. add a getTextLength method that returns the length of the text
// of a specified element.
// Will result in support for storeTextLength, assertTextLength, etc.



// All locateElementBy* methods are added as locator-strategies.
// Eg add a "valuerepeated=" locator, that finds the first element with the supplied value, repeated.
// The "inDocument" is a the document you are searching.

Selenium.prototype.doLogin = function(value,varName){
var paraPart= [];
paraPart =value.match(/(.*)\/(.*)/);
var LOGON = paraPart[1];
var PASSWORD = paraPart[2];
page =selenium.browserbot.getCurrentWindow();
page.document.getElementById("Login1_UserName").value=LOGON;
page.document.getElementById("Login1_Password").value=PASSWORD;
page.document.getElementsByName("Login1$LoginButton").click();

return;
} 