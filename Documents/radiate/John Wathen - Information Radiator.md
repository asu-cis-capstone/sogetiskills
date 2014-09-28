John Wathen

CIS 440 Fall 2014

Information Radiator

##Supply and Demand for IT Skills

The scope of this application is small but the data captured relates to Sogeti's core business activity - providing custom IT solutions by pairing consultants with customers.  I believe it would be useful for Sogeti management to have a view into two metrics captured by this project.  The first would be the supply side where we ask questions about which consultants are available (ie "on the beach").  The second metric would be the demand side where we use searches by account executives as a proxy for customer demand for various IT solutions.   

###How much time are consultants spending "on the beach"?

I propose adding a report to the application that would show time spent on the beach either by consultant (why is Bill spending three times more time on the beach than the average consultant?) or by skill (why do we constantly have a beach full of FoxPro developers?).  Implementing a report like this would be as simple as changing the OnBeach bit field on the Users table to a 1:N table that tracks datetimes of when a consultant enters and leaves the beach and then using SQL Server Reporting Services on top of the data.

###How often are account executives searching for skills?

I propose adding a report to the application that would show which skills account executives are searching for.  Additionally, it would be useful to see what kinds of results those searches are yielding.  For example, are account executives searching for consultants with Java experience and repeatedly finding that there are no consultants on the beach with those skills?  This information may allow Sogeti management to get a better idea of when demand for skills is going unfulfilled.  To enable this report we would need to add a table to represent both the query and answer side of a search performed by account executives and insert into it each time the search form is used.  Again, once we have the data then SQL Server Reporting Service could be used to shape it into something useful for management.