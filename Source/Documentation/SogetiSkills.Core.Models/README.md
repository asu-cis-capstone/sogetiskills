SogetiSkills.Core.Models Namespace
==================================
 


Classes
-------

                | Class                 | Description                                                                                                                                                                                   
--------------- | --------------------- | --------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- 
![Public class] | [AccountExecutive][1] | An account executive. Has access to search for consultants.                                                                                                                                   
![Public class] | [AccountTypes][2]     | Helper class to holder strongly typed versions of the two possible values for the UserType discriminator column in the user table.                                                            
![Public class] | [Consultant][3]       | A consultant that may be on "the beach" and have tags.                                                                                                                                        
![Public class] | [HashedPassword][4]   | A hashed and salted password stored in the database.                                                                                                                                          
![Public class] | [PhoneNumber][5]      | Wraps a string the represents a phone number.                                                                                                                                                 
![Public class] | [Resume][6]           | A resume uploaded by a consultant.                                                                                                                                                            
![Public class] | [ResumeMetadata][7]   | Metadata for a resume uploaded by a consultant.                                                                                                                                               
![Public class] | [Tag][8]              | A tag that has been applied to a consultant. Represents a skill that the consultant has.                                                                                                      
![Public class] | [User][9]             | Abstract base class for a user of the system. Users are either consultants or account executives. The user class holds contact and login information which are applicable to both user types. 

[1]: AccountExecutive/README.md
[2]: AccountTypes/README.md
[3]: Consultant/README.md
[4]: HashedPassword/README.md
[5]: PhoneNumber/README.md
[6]: Resume/README.md
[7]: ResumeMetadata/README.md
[8]: Tag/README.md
[9]: User/README.md
[Public class]: ../_icons/pubclass.gif "Public class"