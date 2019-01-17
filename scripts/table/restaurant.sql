create table restaurant (
  id int identity not null PRIMARY KEY
  ,name nvarchar(max) not null
  ,address nvarchar(max) null
  ,city nvarchar(100) not null
)