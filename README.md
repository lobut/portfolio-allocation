# Portfolio Allocation

## How to Run

```
using Refinitiv.PortfolioAllocation.Domain.Services;

// IPortfolioRepository designates how to retrieve the data
var allocationService = new AllocationService(/* IPortfolioRepository */);

// if you wanted to raise/lower your security named "STOCK" to 25 in each portfolio
allocationService.Calculate("STOCK", 25);
```

Best place to start looking at code is in the `Refinitiv.PortfolioAllocation.Domain.Tests` project as it's the main entry point used for testing.

## Technical Choices / Design / Tests

### Preamble

Here are various thoughts or technical decisions made while creating this code base.

#### Solution/Project layout

I chose for a layout closer to Onion/Hexagonal architecture type approach as with the .Domain namespace I think this is atypical if this were to be bundled as a NuGet package.  
However, I made the decision to have a folder structure closer to a solution that I would deploy knowing that there is no UI.

#### Separate Data Fetching Repository / Data.Csv Assembly

I wanted to test the CSV repository separate and run tests faster, so I chose to constructor inject the dependency.

I also chose to use an external CSV library.  I understand I could write a simple CSV and have it be adequate.  However, in the real world, I would not write a CSV reader for my own purposes.

#### Domain / Entities

I chose to have a Portfolio encapsulate Securities (aka parent-child) and put up the extra effort for the repository to establish that for me (otherwise I'd be operating on a row-by-row basis which would have felt clunky for calculations).

I also preferred to have my objects not be anemic if I can help it.  So certain entities would perform calculations on their own.  There is a risk in that the calculation may be duplicated, but I felt it helped for code clarity.

#### Testing

`NUnit` is my preferred testing suite, I could have used `MSTest`, but wasn't bothered either way.

I chose to use the Test Data Builder Pattern for readability for added code complexity when writing.

I have foregone writing tests for the Domain object calculations as they were trivial and were testing by the higher-level tests.

`Moq` just a testing framework I was familiar with as well.  It had no specific advantage over others I required.  I did initially write my own test doubles but found the readability waned.

If there were a UI I would have written an end-to-end test.  As there wasn't, the closest I could do would be the `Refinitiv.PortfolioAllocation.Domain.Tests` tests.

`_sut` is a standard I used to use which means system under test.  You can derive the name by the assembly and name of the class.

#### More

If I had more time, I would probably:
* comment a few of the interfaces 
* extract the calculations out a bit?  I'm not sure if it as readable as I think
* put cleaner error validation, currently every portfolio is required to have that stock for what's required.  It shouldn't be the case.