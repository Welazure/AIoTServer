## Flows

Notes:
- Remember to authenticate to the API endpoint before doing any operations to avoid having to reconnect which is an expensive operation.

Client should mainly listen for a Type-0 event transmission packet forwarded by the server to be processed.
Event transmission packet:
```json
{
	"Type": 0,
	"Data":
	{
		"Id": "12345678",
		"EventType": 1,
		"Time": 12345678910
	}
}
```

If the clients wishes to request all data from the server, the client should send a Type-4 Data request packet in the following format:
```json
{
	"Type": 4,
	"Data": 3 // Optional field, number of days to filter (e.g. 3 = 3 days counting backwards from today)
}
```
and expect a Type-5 All Data response packet in the following format:
```json
{
	"Type": 5,
	"Data":
	[
		{
			"Id": "12345678",
			"EventType": 1,
			"Time": 12345678910
		},
		{
			"Id": "12345679",
	  		"EventType": 2,
	  		"Time": 1234567891011
		}
	]
}
```