# CodeTest (Plexure)


Exercise 1:

* Created a Console application which shows length of Http content for given URLs.


Exercise 2:

Coupon Management – Data Model

Overview
We are using MS SQL database for storing & reporting the Coupon details.

Table Overview
Table Name	Description
UserDetails	This table contains user details like Name, Address etc.
CouponDetails	This table contains Coupon details.
CouponTransaction	The Transaction table where each redemption is recorded.
•	TransacitonCode column to store the GUID from application.
•	UserId is a FK of UserDetails
•	CouponId is a FK of Coupon
CouponTransactionArchive	All the records from CouponTransaction table will be moved to CouponTransactionArchive on daily/weekly basis to keep the CouponTransaction table with minimum data and to improve the performance.


 


Data Model Diagram

  
Optimizations

Indexes
Table Name	Additional Indexing
UserDetails	MaxCoupon
CouponDetails	MaxCoupon, Title
CouponTransaction	TransactionCode, TransactionDate
CouponTransactionArchive	TransactionCode, TransactionDate

