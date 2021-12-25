# Database table design

## PostInfo

| ID | Name | Data Type | Memo |
| ---- | ---- | ---- | ----|
| 1 | ID | int |
| 2 | Title | varchar(50)
| 3 | Content | text 
| 4 | CategoryID | int
| 5 | ReadCount | int
| 6 | PraiseCount | int
| 7 | CreateTime | bigint
| 8 | CreateUserID | int
| 9 | UpdateTime | bitint
| 10 | UpdateUserID | int

## CategoryInfo

| ID | Name | Data Type | Memo |
| ---- | ---- | ---- | ----|
| 1 | ID | int |
| 2 | Title | varchar(50)
| 3 | Content | text 
| 4 | CreateTime | bigint
| 5 | CreateUserID | int
| 6 | UpdateTime | bitint
| 7 | UpdateUserID | int

## UserInfo

| ID | Name | Data Type | Memo |
| ---- | ---- | ---- | ----|
| 1 | ID | int |
| 2 | Name | varchar(50)
| 3 | Memo | varchar(500)
| 4 | Account | varchar(50) 
| 5 | Password | varchar(50) | md5
| 6 | CreateTime | bigint
| 7 | CreateUserID | int
| 8 | UpdateTime | bitint
| 9 | UpdateUserID | int