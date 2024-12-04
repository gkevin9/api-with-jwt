create table user(
id varbinary(32),
iv varbinary(16),
username varchar(255),
password varbinary(16),
primary key (username)
);