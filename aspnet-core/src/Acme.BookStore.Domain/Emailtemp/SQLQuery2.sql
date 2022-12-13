create database Test
use Test
create table Employee (id int,employee nvarchar(200),salary int)

insert into Employee values(1,'jon',250)
insert into Employee values(5,'max',150)
insert into Employee values(3,'tom',660)
insert into Employee values(2,'harry',600)
insert into Employee values(9,'ninja',300)

select * from Employee

create Clustered Index ix_employee on
Employee (id Asc)

