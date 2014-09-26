#Testing strategy for Sogeti Skills

Sogeti skills has a suite of automated integration and unit tests using Visual Studio's built in MSTest framework.  As we get more of the UI implemented, we'll be adding UI tests using Selenium WebDriver for some select functionality.

### Running the tests
To run the tests you'll need to 
 - install Visual Studio 2013 Express for Web (http://www.microsoft.com/en-us/download/details.aspx?id=43722) or any of the higher 2013 SKUs
 - optionally the Web Essentials extension for Visual Studio (http://visualstudiogallery.msdn.microsoft.com/56633663-6799-41d7-9df7-0f2a504ca361)
 - clone the Sogeti Skills repository and open up the solution file at /Source/SogetiSkills.sln
 - after building the solution, Visual Studio should automatically pick up the tests and make them availble to run in the Test Explorer window.  If the Test Explorer window is not open, it can be opened from Test -> Windows -> Test Explorer in the Visual Studio toolbar.