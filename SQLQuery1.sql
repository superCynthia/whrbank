alter table Costomer
add constraint c1 check (性别 in ('男','女'))

alter table Staff
add constraint s1 check (密码 like '[0-9][0-9][0-9][0-9][0-9][0-9]')

alter table Costomer
add constraint c2 check (密码 like '[0-9][0-9][0-9][0-9][0-9][0-9]')

alter table Staff
add constraint s2 check (性别 in ('男','女'))

alter table Costomer
add constraint c3 check (身份证号码 like '[0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9]')