# CheckoutWebService
This is a web API solution that provides endpoints to manage a shopping list:

The endpoints are defined as follows:
· HTTP ​POST request ​for adding a drink to ​the shopping list with quantity, e.g. name of drink (Pepsi) and quantity (1).
· HTTP ​PUT request for updating a drink's quantity.
· HTTP ​DELETE request for removing a drink from the shopping list.
· HTTP ​GET request for retrieving a drink by its name and displaying its quantity so we can see how many have been ordered.
· HTTP ​GET request for retrieving what we have in the shopping list.

Notes
· This service does not use a database - an in-memory cache is used to hold the shopping list
· The shopping list does not contain duplicate drink names, they are all store case insensitive
· There is a request that require a basci authentication: it has been implemented in a custom mode
· The service to retrive the whole shopping list provide paging
· There is a validation only fo the quantity (it should be between 0 and 999
