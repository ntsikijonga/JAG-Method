# JAG Method software developer assessment

This developer assessment has been created to test your exposure to the different areas of technology that will make up your day-to-day work at JAG Method. You may not be able to complete all of the questions, but we highly encourage you to at least attempt them all. We have added an estimate time to each question so that you can pace yourself and so you do not get stuck on a question and waste too much time. Please submit all assessments regardless of how far you got.

This assessment has some simple rules
-
1. your code must compile
2. all libraries (nuget or other) used must be accessible from the internet (or locally available)
3. do not commit package, bin or obj folders
4. you can use the internet
5. try every question
6. upload your code to a github or bitbucket repo and send us the link.

## Questions

### 1. SEO (5min)
* Suggest 4 SEO enhancements that could be made to the web page located in JAG.DevTest2019.WebHost/Views/Lead/Index.
* Add your answers to the answers.md file

### 2. Responsive (15m)
* The web page located in JAG.DevTest2019.WebHost/Views/Lead/Index makes use of [Bootstrap](https://getbootstrap.com) to respond to different screen sizes, but has some problems. Fix these responsive issues.
* List the items you have fixed in the answers.md file 

### 3. Validation (15m)
* Currently the form does not validate any input. 
* Implement the following validation rules
	* FirstName is required
	* LastName is required
	* ContactNumber is required
	* Email address should be a value email address
	* ContactNumber should be a valid telephone number

### 4. JavaScript (20m)
* On the lead index page (JAG.DevTest2019.WebHost/Views/Lead/Index) you will notice that one of the "Benefits" is:
	>Over 6,000,000 queries serviced
* Add javascript to this page to **randomly** increase this value while the page is open.
* The number should start at 6,000,000 and then **realistically** tick upwards as "more people are serviced" by the site

*You can use JQuery in this question.*

### 5. Ajax calls (30m)
* Update the lead submission form to make use of an Ajax call
* This should remove the postback when the button is clicked and still display the resulting message
* **Bonus points**
	* Disable the button while processing the Ajax call
	* Display an Ajax loader while the call is running

### 6. Call a REST webservice (25m)
* JAG.DevTest2019.ServiceHost hosts a WebAPI REST service with a single endpoint called `Lead` that accepts a POST. (The service is hosted using Owin selfhost)
* This endpoint expects a `Lead` object that you can find in the JAG.DevTest2019.Model project.
* Add code to the `LeadController` in the `JAG.DevTest2019.WebHost` so that when the form on the lead page is submitted, the necessary information is passed to the WebAPI service.

### 7. ADO.Net write lead DB (40m)
* Create a local database called JAG2019
* Run `schema.sql` in the `sql` solution folder to create the necessary tables
* You should now have a database with the following tables

![schema](https://bitbucket.org/jagmarketing/interviewtests/raw/master/content/LeadDB.PNG)

* Add code to the JAG.DevTest2019.LeadService.Controllers.LeadController.Post method to save the lead object to the database. You should preferably use normal ADO.Net code to achieve this.
* **Note** that the lead table only stores some of the form's information all other fields should be stored into the `LeadParameter` table. You should not need to change the table design.
* **Note** if you make any changes to the table structure please include these in the answers.md file
* Retrieve the ID of the created object and return this to the caller.

* **Bonus points**
	1. Check if the lead already exists in the database (by ContactNumber or Email) and then return 
		* IsDuplicate = true
		* IsSuccessful = false
		* Message = duplicate on [email/ contact number]
		* (save the relevant information)
	2. If the TrackingCode has generated more than 10 leads in the day then return
		* IsCapped = true
		* IsSuccessful = false
		* Meassage = [x] lead recieved today, code [TrackingCode] is capped
		* (save the relevant information)

*If you haven't got your UI to call the WebAPI service you will need to create a unit test to test this action and the saving code.*

### 8. Poll DB for results (15m)
* Either using custom code or a nuget package, create a job that polls the lead table in the database once every minute, to check for new lead records.
* Write the number of new records to the console window each time the poll job runs.
* **Note** if you make any changes to the table structure please include these in the answers.md file

### 9. Use SignalR or equivalent to notify the web-page visitor that their lead was successfully received. (>40m)
* Imagine that a large amount of processing needs to take place once a lead is written to the database. 
* There is no way a web site visitor can wait for this processing to take place or the form will feel like it is "not responding"
* We rather want to communicate back to the visitor in an asynchronous way. SignalR and other frameworks allow you to do this.
* The high level requirements are as follows:

1. Connect each visitor to a SignalR hub and include the visitor's sessionid in the lead object
2. Create a duplicate job to the one you created in question 8 to poll for new leads
3. When a new lead record is found in the DB, fire a message via SignalR using the visitor's sessionId so that a "toast" message is displayed to the visitor with the results of their lead submission (Success, Duplicate, Capped) .
* **Note** if you make any changes to the table structure please include these in the answers.md file

### 10. Data Analysis (<30m)
* On a local database run the script file called `question10.sql` in the `sql` folder
* This script will create 3 tables and populate them with fictitious data
	* **Client**
	* **Campaign**
	* **LeadDetail**
* Client & Campaign are dimension tables and only contain the name of the client and the name of the campaign, with their primary keys.
* Lead detail is a fact table and contains financial and lead event metrics.
* This question requires you to pull a set of information from these table and draw some conclusions on it.
* You will need to write SQL to calculate your results for each question. Add this sql into the answers.md file in the solution. Write the answers as a comment above each question.

1) Knowing that `Profit = Earnings - Costs`, calculate the total profit for all records in the entire data set

2) Assume that Earnings already include VAT of 15% and Costs do not. Recalculate profit based on this information.

3) For the dataset which campaigns are making a profit

4) Knowing the following `Conversion rate % = SUM(Sales)/ SUM(Accepted)` and that **leads appear multiple times** in the data set. Calculate the average conversion rate for the entire data set.

5) If you were an account manager focusing on Profit and conversion rate, which two clients would you focus on and why?
