Feature: AddAsync

Scenario: Add event to queue
	Given an IEvent
    When EventsQueue.AddAsync(IEvent) is called
    Then the event is added to the queue
