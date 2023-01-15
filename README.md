# Demo-API Endpoints

## example request 
<br/>

# V1


### Edit profile
api/v1/ApplicationUser/edit/{username}
```json 
{
    "id": "<id>",
    "firstName": "<firstName>",
    "lastName": "<lastName>",
    "userName": "<username>",
    "email": "<email>",
    "phoneNumber": null
}
```

### Refresh Token
api/v1/authenticate/refresh-token
```json 
{
        "accessToken": "<token>",
        "refreshToken": "<refreshToken>"
}
```

### Register (User && Admin)
api/v1/authenticate/register-admin (Admin)
api/v1/authenticate/register        (User)
```json
{
     "firstName": "<firstName>",
    "lastName": "<lastName>",
    "userName": "<username>",
    "email": "<email>"
    "password": "<password>"
}
```

### Login
api/v1/authenticate/login
```json
{
    "userName": "<username>",
    "password": "<Password>"
}
```

### Revoke
api/v1/authenticate/revoke/{username}
```json
{
    "accessToken": "<token>",
    "refreshToken": "<refreshToken>",
    "expiration":"<expirationDate>"
}
```