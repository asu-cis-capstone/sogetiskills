Database Initial Study
Enterprise level database (MS SQL Server) 
Interaction with internal application and possibly web services

**Roles**

	DBA: 			access to maintain/optimize
	Front-end dev: 	application-tier accesses DB securely (password hashing)
	End-users: 		2 types (consultants/account executives) with no access to DB

**Compliance**

Important to achieve data security/ database compliance this will include:

	•	appropriate governance
	•	secure platform
	•	strict identity/access control
	•	DB encryption
	•	Auditing

**Functions**		(create, read, update and delete)

	1.	user registry/ create login-id (unique)	
	2.	user login
	3.	user authentication (*levels)	
	4.	user logout
	5.	read login-id	
	6.	CRU password
	7.	CRU first name	
	8.	CRU last name
	9.	CRU email	
	10.	CRU user-type
	11.	CRUD keyword	
	12.	CRUD keyword associations
	13.	CRUD phone number	
	14.	CRUD résumé 
	15.	CRU beach status	
	16.	list all active consultants on beach (view)
	17.	list all available metadata tags (view)	
	18.	18.	filter by tags (select query)
	19.	filter by search (select query)	
	20.	acct execs can R consultant data (select query)
	21.	access rights	

**Assumptions/Rules/Limitations**

	•	all end-users can only create/modify their own information
	•	all users can read their own info
	•	all account execs can read all pertinent consultant data
	•	DB is encrypted
	•	DB has back-ups and transaction logs (stored procedures/triggers)
