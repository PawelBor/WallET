
# *Mobile Application Development 3*
# *WallET*
## Universal Windows Platform Project


**Name:** Pawel Borzym </br>
**College:** Galway-Mayo Institute of Technology </br>
**Course:** Software Development </br>
**Module:** Mobile Application Development 3 </br>
**Lecturer:** Martin Kenirons </br>


#Project Overview
WallET is an application developed in C# on Universal Windows Platform. </br>
It allows for Registration / Login for security reasons, once the user Logs in then he will be transferred to his WallET where balance to spend will be displayed. </br>
EVerytime the user will buy something he will be allowed to enter Details of the product such as Name or Price. This can be saved for future reference or picture may be taken to keep the receipt. </br>
The amount spent will be deducted from the wallet, main goal of this part is to help the user manage his/her expenditure. 


## ***Architecture***

### ***Storage***
* SQLite + IsolatedStorage - </br>
SQLite used for keeping the Price, Name and Quantity of the Product. </br>
IsolatedStorage is used for keeping the account details + Receipts

### ***Functionality***
* User is first greeted by the Login/Registration Page which can be navigated by swiping left/right or simply clicking.</br>
Then the user will be displayed choice between two items, either user will access map with few big store branches or </br>
access his own WallET where he can add items and then save the payslip, then from there the user can access the page with Receipts saved.</br>
The user also can set his own "budget" and anything that is Added/Removed from the database will update the Users Set Balance.
Item Count + Price is kept and updated with every operation.


### ***Display***
* All Data is displayed in a List where user can see all items added, the user has the access to all CRUD ( Create-Read-Update-Delete) Operations</br>
Not only the user can delete All items but the user can also delete selected item from the list. 


### ***MVVM***
Model View View-Model (MVVM) - Certain parts (map functionality) use the MVVM, rest of the application would not be fully based upon it. </br>
There's place for improvement.

### ***General***
The Application is Localized meaning it works with multiple languages such as Polish, Spanish and English. (Strings Folder)
