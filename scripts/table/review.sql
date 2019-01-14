create table review (
  id int identity not null PRIMARY KEY
  ,user_id int not null FOREIGN KEY REFERENCES users(id)
  ,restaurant_id int not null FOREIGN KEY REFERENCES dbo.restaurant(id)
  ,heading nvarchar(100) null
  ,content nvarchar(max) null
  ,rating tinyint not null
)