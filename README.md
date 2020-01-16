# Contact_Lists_App
This apllication is implemented by C# which used WPF built in function in Visual Studion to build GUI. The Application is about contact list management which allow a user to create, update, remove, and search contact details from database.

## Database structure
The database that implement in this application is based on MariaDB.
```
+---------+--------------+------+-----+---------+----------------+
| Field   | Type         | Null | Key | Default | Extra          |
+---------+--------------+------+-----+---------+----------------+
| id      | int(11)      | NO   | PRI | NULL    | auto_increment |
| name    | varchar(150) | YES  |     | NULL    |                |
| surname | varchar(150) | YES  |     | NULL    |                |
| phone   | int(11)      | YES  |     | NULL    |                |
| email   | varchar(150) | YES  |     | NULL    |                |
+---------+--------------+------+-----+---------+----------------+
```
