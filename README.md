# Contact_Lists_App

## Database structure
#### The database that implement in this application is based on MariaDB.
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
