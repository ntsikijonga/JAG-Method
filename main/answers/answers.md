#JAG Method software developer assessment
## Answers

### 1. SEO (5min)

1)Change ViewBag.title "Page tittle" to something meaningful .e.g "Best Insurance Quotes". 
2)Add meaningful content in the description meta tag in the meta section .e.g. content="Don’t pay too much for Insurance Find the best Insurance" etc.
3)Provide meaninigful content in the keywords meta tag .e.g. content="Don’t pay too much for Insurance Find the best Insurance" etc.
4)Add a robots meta tag in the meta section .e.g. (<meta name="robots" content="index" />) so that web crawlers and robots will index this page.

### 2. Responsive (15m)

1)Wrapped texboxes with <div class="form-group"></div>


### 3 - 6 Answers in code

### 7. ADO.Net (40m)
Add any SQL schema changes here
I have included a backup of JAG2019 database in the ansewrs folder

### 8. Poll DB (15m)
Add any SQL schema changes here

### 9. SignalR (40m)
Add any SQL schema changes here

### 10. Data Analysis (30m)

1) Total Profit
-67870.596850635028

SQL
SELECT SUM(Earnings) - SUM(Cost) From [JAG2019].[dbo].[LeadDetail]

2) Total Profit (Earnings less VAT)
**24157.1405161359458**

**SQL**
` SELECT SUM([Earnings]) - (SUM([Cost]) + (SUM([Cost]) * 0.15))   From [JAG2019].[dbo].[LeadDetail]`

3) Profitable campaigns
422 campaigns are making profit

SQL
`SELECT [LeadDetailId], [Cost], [Earnings], SUM([Earnings] - [Cost]) AS Profit  from [JAG2019].[dbo].[LeadDetail]
  Group by [LeadDetailId], [Cost], [Earnings]
  HAVING SUM([Earnings] - [Cost]) > 0

4) Average conversion rate
0.0112767584097859

SQL
with cte([LeadDetailId], [IsSold], [IsAccepted], [ClientId])
  AS
  (
      SELECT DISTINCT [LeadDetailId], [IsSold], [IsAccepted], [ClientId] 
	  FROM [JAG2019].[dbo].[LeadDetail]
  ) 
SELECT SUM(CAST([IsSold] AS float))/ SUM(CAST([IsAccepted] AS float)) FROM cte

5) Pick 2 clients based on Profit & Conversion rate & Why?
Client 205 because their convertion rate is almost consistent and they generated more leads 
Client 215 because they generate more profit from their leads**

SQL
SELECT [ClientId], [Cost], [Earnings], [IsSold], [IsAccepted], SUM([Earnings] - [Cost]) AS PROFIT FROM [JAG2019].[dbo].[LeadDetail]
   GROUP BY [Earnings],[Cost],[ClientId], [IsSold], [IsAccepted]
   ORDER BY PROFIT DESC
