Communication diagram


| Type              | ID  |
| ----------------- | --- |
| Data Transmission | 0   |
| No apps connected | 1   |
| ID Collision      | 2   |
| ACK               | 3   |
| Request All Data  | 4   |
| All Data Response | 5   |
| Auth Request      | 6   |
| Auth Response     | 7   |
| Token Renew       | 8   |


```json
{
	"Type": 1,
	"Data": {
		"Id": "12345678",
		"EventType": 1,
		"Time":12345678901234
	}
}
```

### Authentication
- Client connects to "/auth" websocket endpoint
- Client sends authentication request packet
- Wait for authentication response packet
- Process authentication response packet
	- If successful: Store response token in a secure location
	- If unsuccessful response: Re-Authenticate
- During connection to "/iot" or "/app" endpoint, client must send an authentication request packet with the token and wait for an authentication successful response packet. Otherwise, no operations will be available.
- JWT Tokens live for 6 hours. Clients should be ready for a Type-8 (Token renew) packet to replace stored token. (Listen in onMessage)

Notes:
- JWT Token renewal should be running async if possible to avoid blocking the events handling thread on the client.
**Sample** **packet**:
Authentication request packet: 
```json
{
	"Type": 6,
	"Data": 
	{
		"Username": "username",
		"Password": "password"
	}
}
```
Authentication successful response:
```json
{
	"Type": 7,
	"Data":
	{
		"Response": 1,
		"Token": "token" // jwt token
	}
}
```
Authentication unsuccessful response:
```json
{
	"Type": 7,
	"Data":
	{
		"Response": 0
	}
}
```
API Endpoint authentication request packet:
```json
{
    "Type": 6,
    "Data":
    {
        "Token": "token"
    }
}
```

Token Refresh packet:
```json
{
	"Type": 8,
	"Data":
	{
		"Token": "token" // jwt token
	}
}
```
