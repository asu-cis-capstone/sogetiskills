sogetiskills
============
Repository for project sogeti skills inventory

###Sogeti Skills Inventory/Hub
Project "Beach"- For Client Sogeti USA, an IT Consulting Firm. Creation of record keeping software to track consultants and their skills; a summary website; and small web application that will allow users at Sogeti USA to find consultants who are on "the beach."  Master branch builds are automatically pushed to http://dev-sogetiskills.azurewebsites.net/.

###TO-DO
####In progress (v0.7)
	
  * Centralize authorization code
  * Account Executive dashboard
    * List all skills available on the left side

####Product Backlog
  * Account Executive dashboard
    * Link to e-mail a specific consultant
    * Link to e-mail all consultants on the beach  
  * User maintenance
  * User type change
  * Roles- Management of logins/users and permissions
  

  * Skill search No / blank / blank / blank = no results
  * Skill search Yes / blank / blank / blank = no results
  * Limit size of ALL user entry fields
	- 	  Limit size of FirstName field
	- 	  Limit size of LastName field
	- 	  Limit size of EmailAddress field
	- 	  Limit size of newSkillText field
  * Change canonical skills name field to Skill
  * Change “Beach Only” to “Beach Status”
  * Add beach status = “Any” to BeachOnly / Beach Status” search field
  * Remove email Address search field from AE screen
When AE logs on, the home screen should display all consultants with all skills.  Search = status = all, name = blank.  All registered consultants, and all skills should be displayed
-   Change skill entry screen
-   fix skill entry screen 
  

####Complete
v0.1

  * Create project structure
  * Database design
  * User login
  * User authentication persists across all pages
  * Without authentication, all web pages redirect to user login
  * User logout

v0.2

  * Allow users to register
  * Users may only maintain their own details
  * Edit contact info
  * Upload, edit, and download resumes
  * View current beach status

v0.3

  * Added documentation of the SogetiSkills.Core assembly at https://github.com/asu-cis-capstone/sogetiskills/tree/master/Source/Documentation/
  * Add constraint to Password

v0.4

  * User maintenance
  * Edit consultant skills
  * Data migrations

v0.5

  * Consultants can change their current beach status
  * Added proficiency to consultant skill

v0.6

  * Account executives can search for consultants by name, email address, skills, and whether or not they are on the beach.

###Team & Client
- Project Management
  John M: jsmonje@gmail.com 
    git: jsmonje
    [jmonje project](https://github.com/jsmonje/Personal-Project)
    phone: 480-231-7552
- Frontend Development
  John W: john.s.wathen@gmail.com
    git: jwathen
    skype: live:john.s.wathen
    phone: 602-448-3306
- Client- Sogeti USA 
    website: https://us.sogeti.com/
    contact: James Moran, MCP MCTS
      email: james.moran@us.sogeti.com
      phone: 630-337-1569
- Links:
  Dropbox- https://www.dropbox.com/l/lPGIbSkrGrd0sHM8JoC9Cp/
