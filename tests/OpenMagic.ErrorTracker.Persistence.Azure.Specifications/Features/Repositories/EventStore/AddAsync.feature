Feature: AddAsync

Scenario: Add event to EventStore for a new aggregate
	Given aggregateType is 'ErrorAggregate'
	And aggregateId is '46cfb833-1525-427a-8179-1378be142c3c'
	And a 'RaygunMessageReceived' event
    When EventStore.AddAsync(aggregateType, aggregateId, event) is called
    Then a row is added to the 'Events' table with values
		| Column Name  | Value                                      |
		| PartitionKey | error/46cfb833-1525-427a-8179-1378be142c3c |
		| RowKey       | 0000000000000000001                        |
		| EventType    | RaygunMessageReceived                      |
		| Event        | RaygunMessageReceived event as JSON        |
	## todo: and the event is published

Scenario: todo
	Given todo
