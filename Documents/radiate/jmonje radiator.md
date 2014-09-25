

# Information Radiator Write-Up        Sogeti Skills        john monje

# Problem

The effectiveness of the Sogeti Skills application is directly dependent upon participation of each consultant and the accuracy of the data provided to the application. Implementation of this feature could lead to improved participation and increased accuracy.

# Solution

The proposed new feature (output) would be one additional report. This report would allow Sogeti to view ALL Con users (and their associated skills) known to the Sogeti Skills app.

# Cost

The implementation of this feature would be very simple (per my fantastic development team).

Initial estimates are as follows:

One simple SQL query

Add one field to a user screen

Add code to associate the new button with the new query

A re-design or re-architecture of the program would NOT be required

Time estimate = 3 hours to define, implement, and test.

# Benefit

1. Management could compare the consultants registered with the (HR) consultant universe. Each consultant should be registered. This report could be used to approach a 100% compliance level.

1. Management could compare the actual consultants that are currently billing to admin (non client) codes from the payroll system with the data in Sogeti Skills. Each consultant billing to admin codes should have indicated an "on the beach" status. This report could be used to approach a 100% accuracy level.

1. Management could review the skills declared by each consultant. This information needs to be complete and accurate. If a proficiency level is indicated, management could ensure that it is realistic. This report could be used to approach a 100% accuracy level.

1. By analyzing this report, it may become obvious that there is a lack of a particular skill or set of skills. Consequently, this may trigger the need for either some training to develop the skill or recruiting new resources with such talent, or both.

# Background

Sogeti Skills is a web application that will allow Account Executive (Sales) users at Sogeti USA to find consultant (Con) skills that are currently unutilized (not billing to a client).

# Current functionality is as follows:

1. Skills has NO interface to any other Sogeti app / data

1. Con will:
  1. register with Skills
  2. indicate their status (beach or engaged)
  3. indicate their skills
  4. submit a resume

1. If con indicates a status of "on the beach", AE can search the Con / skills available. This knowledge assists AE when selling new work.

# Assumptions:

100 % Con participation is required for accurate reporting

100 % Con accuracy is required for accurate reporting

Con universe = approx. 70

Beach universe = approx. 10 % = 7

Con has 3 (might not be unique) skills

Con = human and may not register with Skills (75%)

Con = human and even if registered, may not indicate availability to Skills (75%)

Con = human and could possibly omit or inflate skills / proficiencies.

