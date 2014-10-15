sogetiskills
============
Repository for project sogeti skills inventory

###Sogeti Skills Inventory/Hub
Project "Beach"- For Client Sogeti USA, an IT Consulting Firm. Creation of record keeping software to track consultants and their skills; a summary website; and small web application that will allow users at Sogeti USA to find consultants who are on "the beach."  Master branch builds are automatically pushed to http://dev-sogetiskills.azurewebsites.net/.

###TO-DO
####In progress (v0.5)
  * Change current beach status
  * Added proficiency to consultant skill
  * Database
    * Roles- Management of logins/users and permissions
    * DB Schema/ Dictionary

####Product Backlog
  * Database
  * Indexing / Query Tuning
  * Account Executive dashboard
    * List all consultants currently on the beach on the right side
    * List all skills available on the left side
    * List all available tags
    * Filter by tags
    * Filter by search
    * Link to download a specific consultant's resume
    * Link to e-mail a specific consultant
    * Link to e-mail all consultants on the beach
  * Database Encryption (TDES)
  * DB Auditing
    * Security Event Logging (application/security/file logging)
    * Policy-based management
  * DB Backup Strategy/ Recovery Model
    * Full/Differential backups
  * Transactional log backups
  * Correct AE dashboard spec with only skills (left) and consultants (right)
  * User maintenance
  * User type change

####Complete
##### v0.1
  * Create project structure
  * Database design
  * User login
  * User authentication persists across all pages
  * Without authentication, all web pages redirect to user login
  * User logout
##### v0.2
  * Allow users to register
  * Users may only maintain their own details
  * Edit contact info
  * Upload, edit, and download resumes
  * View current beach status
##### v0.3
  * Added documentation of the SogetiSkills.Core assembly at https://github.com/asu-cis-capstone/sogetiskills/tree/master/Source/Documentation/
  * Add constraint to Password
##### v0.4
  * User maintenance
  * Edit consultant skills
  * Data migrations

###Team & Client
- Project Management
  John M: jsmonje@gmail.com 
    git: jsmonje
    skype: j.monje11
    phone: 480-231-7552
- Frontend Development
  John W: john.s.wathen@gmail.com
    git: jwathen
    skype: live:john.s.wathen
    phone: 602-448-3306
- Backend Development
  Olivia S: olivia.sin@gmail.com
    git: olivia-sin
    skype: oliviasin
    phone: 213-447-0953
- Client- Sogeti USA 
    website: https://us.sogeti.com/
    contact: James Moran, MCP MCTS
      email: james.moran@us.sogeti.com
      phone: 630-337-1569
- Links:
  Dropbox- https://www.dropbox.com/l/lPGIbSkrGrd0sHM8JoC9Cp/
