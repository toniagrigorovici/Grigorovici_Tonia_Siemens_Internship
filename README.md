
How to run the project:

	1. Clone the repository
	2. Open Package Manager Console and run the following command: Update-Database
	3. Check the connection string in the appsettings.json file:
	4. Run the project



Implemented Features:

	1. Books
	- implemented sorting of books by title and author
	- view only the books that are currently available

	2. Authors and Categories
	- when selecting an author/category, all books asociated with that author or category are displayed
	- search functionality is available for both authors and categories
	
	4. Borrowings
	- there is an option to view only the books that are currently borrowed
	- if a book is not returned on time, the label "Overdue!" is displayed to indicate de delay
	- when creating a borrow, the estimated return date is automatically set to one month ahead, but it can be manually adjusted
	- borrowings can also be searched by book title, author and the name of the person who borrowed the book

Additional ideas:

	1. Add a login system for users, to allow them to manage their own borrowings
	2. Separate roles for administrators and regular users to ensure better security and limit access to sensitive management features
	3. Add email notifications:
		- for borrowing details
		- for overdue books
		- a reminder for the return date