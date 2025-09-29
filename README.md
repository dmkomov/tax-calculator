The Tax Calculator application allows calculating expected tax amount based on provided gross annual salary.

The application has a single HTTP endpoint:
- POST /api/tax-calculator?salary={value}

The tax calculation rules are stored in the in-memory database through EF.
MediatR library is used to decouple the API and application layers.
