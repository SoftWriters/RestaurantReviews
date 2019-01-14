create table users (
  id int identity not null PRIMARY KEY
  ,username nvarchar(25) not null UNIQUE
)