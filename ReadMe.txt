On Startup of app
Login
Register
Login with Google as well
Created two tables Equipment and EquipemntTypes and mapped the types table to Equipment Table
Created two controllers and two services for Equipment and EquipmentTypes
I used Equipment status as an Enum
The Index view for all equipments handles pagination, search(either full name or partial name or description or status.)
The Index view for all equipmentsTypes handles pagination, search(either full name or partial name or description.)
Responsive tables 
collapses on smaller screens
Used Datatables for view.
Used Moment for datetime formatting
Implemented SoftDelete with a bool IsDeleted so user data is not totally deleted at the database unless the database admin wants to


