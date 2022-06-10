# sqlite-locking

Reproduces the file locking behaviour of EntityFramework Core when using a Sqlite database.

Running the project will:

- Create a Sqlite DB on your desktop.
- Save something to that DB.
- Dispose of EF `DbContext`.
- Report whether anything is locking the DB file.
