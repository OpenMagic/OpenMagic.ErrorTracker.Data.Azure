Feature: AddAsync

Scenario: Add event to EventStore for a new aggregate
	Given aggregateType is 'ErrorAggregate'
	And aggregateId is '46cfb833-1525-427a-8179-1378be142c3c'
	And a 'RaygunMessageReceived' event
    When EventStore.SaveEventsAsync(aggregateType, aggregateId, events) is called
    Then an event stream should be created
	And the event stream should contain the event
	## todo: and the event is published

Scenario: todo
	Given todo
