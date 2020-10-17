Sprint 1 overview	

This design approach has to be viewed in the context of having limited time and information. However it is intended as a
1st sprint where the overall architecture is established and detail, such as how encapsulation and scope is enforced in 
the collaborating assemblies was not addressed in order to avoid analysis paralysis. Dependency injection was not addressed
because of time constraints. This issue would be best put off until after the port service factory was put in place.
Under this paradigm, the controllers would rely on a PortServiceFactory instance in order to get the PortService they would need for their
operation. (In many cases this would be a specialized 'Unit of Work' implementing Port Service that would Orchestrate the requested operations
of "Single Service" port services in a "Transactional" orchestrated manner.
The rest of the injection would take place in the various PortServiceFactory derivations within this particular bounded context.

This is written with reference to the DDD paradigm with the understanding that we are developing highly supple code that can be manipulated 
easily throughout the iterations leading to each significant milestone as new insight is aquired.

Major overlays would  follow such as business logic (strategies including, composition for validation, translation, calculation), 
logging, security, implementing asynchronous REST operations, database design and ORM related strategies based on technology decisions

Architecture used

https://blog.octo.com/en/hexagonal-architecture-three-principles-and-an-implementation-example/
https://spin.atomicobject.com/2013/02/23/ports-adapters-software-architecture/


Design principles used

https://www.goodreads.com/book/show/85038.Design_Patterns_in_C_
https://scotch.io/bar-talk/s-o-l-i-d-the-first-five-principles-of-object-oriented-design
https://www.amazon.com/Domain-Driven-Design-Tackling-Complexity-Software/dp/0321125215

Articles referred for variations on past implementations
https://www.c-sharpcorner.com/article/generic-repository-pattern-with-mvc/