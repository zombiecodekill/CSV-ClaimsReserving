# CSV-ClaimsReserving
A .NET Core MVC app for processing claims figures in CSV format

Written in C# using .NET Core 3.1

In the appsettings.json file there are two file paths which are specify a location on your local machine.
These are "InputFilesDirectory" and "OutputFilesDirectory"

The application reads in any CSV files in the Input directory and displays the filename on a button

The CSV files should contain incremental payment data

Clicking on it will process the file. If there are any critical problems with the file it will report them back to the user.
Otherwise it will accumulate the data and write the translated file to the output files directory.

Included in this repository are some examples of both clean and unclean payment data.
