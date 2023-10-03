
# Full Stack Development Home Assignment

### [Assignment.pdf](Assignment.pdf)


## Calculator Implementation

### Introduction:
In this project, I implemented a calculator program that takes an arithmetic expression as user input and calculates its result. 
The implementation relies on a stack data structure to efficiently handle both numbers and operators within the expression. 
This document outlines the principles and reasons behind my calculator implementation.

###  Stack Data Structure:
#### Purpose: The primary data structure used in this implementation is a stack. A stack is chosen because it follows the Last-In-First-Out (LIFO) principle, which aligns with the nature of arithmetic expressions, where operators must be applied to the most recently encountered numbers.
#### Number Stack: I maintain a separate stack for numbers encountered in the expression. When digits are parsed from the input, they are pushed onto the number stack. This stack allows for efficient handling of multi-digit numbers.
#### Operator Stack: Another stack is used for operators. Operators encountered in the expression are pushed onto this stack. Using an operator stack ensures that we can perform operations in the correct order of precedence.

#### Calculating the Result:
Operator Precedence: To ensure the correct order of operations, the operator stack is consulted to determine if the operator at the top the of the stack has higher precedence than current operator.
If so, the operator at the top of the stack is popped and applied to the top two numbers on the number stack.


### Applying SOLID principles:
The program's modular design, adherence to abstraction, and open-closed architecture ensure that it can evolve over time to accommodate new operators and features with minimal disruption to existing functionality.
1. Ensuring that each operator has its own dedicated class. Each operator class has a single responsibility: to execute a specific arithmetic operation.
2. The program is open to adding new operators without altering existing code. IOperator, defines a common interface for all operators.
3. Any operator can be substituted for another without affecting the correctness of the program.
4. The Operator interface includes only the essential methods necessary for performing the calculation. 
5. nstead of hardcoding the instantiation of operator objects within the calculator, I made use of dependency injection or a factory pattern.


### Unit Tests and Integration Tests


## Run using docker
```
docker build -t calc-service .
docker run -p 5191:5191 calc-service
```

[http://localhost:5191](http://localhost:5191)