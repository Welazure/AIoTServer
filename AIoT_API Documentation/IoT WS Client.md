## Flows

### Data transmission

- Client should send any events using a Type-0 packet which is pooled using an array or similar.
- After sending, client should wait for a response packet and respond accordingly.
	- Type-1 packet received: No app clients are connected. Drop the event from the pool and send through backup methods (e.g. SMS notifications)
	- Type 2 packet received: ID Collision error, which means that an event with the same ID already exists on the server. Resend event with a different ID.
	- Type 3 packet received: Event acknowledged and has been forwarded to at least one app client

Notes:
- Remember to authenticate to the API endpoint before doing any operations to avoid having to reconnect which is an expensive operation.
- Remember to check database design for event types and data specification.
- Client should be running asynchronously

**Sample** **packet**:
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

Type-1/2/3 packet:
```json
{
	"Type": 1, // Refer to Packet Type table
	"Data": "12345678" // Id of event to process
}
```