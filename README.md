# Speck.TemporalBatching

A generic implementation of the "Temporal Batching" design pattern that integrates with Microsoft's Dependency Injection.

## Purpose

The purpose of this library is to provide generic tools for solving the problem that occurs when an application requires
an "individual request" API but batching these requests under the hood is preferred for performance reasons. An example
of a library utilizing this design pattern is Confluent's [librdkafka](https://github.com/confluentinc/librdkafka).
This library implements an in-memory buffer for messages being produced that allows application developers to design
batching-agnostic applications while still taking advantage of the IO performance benefits of batching messages sent to
Kafka. At the time of writing, the property used to configure the buffer timeout window is "queue.buffering.max.ms".

## Temporal Batching Design Pattern

The "Temporal Batching" design pattern is a self-coined term used by the library author after failing to identify a 
well-known design pattern that matches the implementation. As such the "Temporal Batching" design pattern is intended
to convey the act of abstracting batching of requests that occur relatively closely in time through a request handler.  
