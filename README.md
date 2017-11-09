# Band Tracker
##### by James Osborn

### Description

This is a C#/MVC application for a Band Tracker. The user will be able to see a list of venues, then add bands to those venues. Bands can be listed at many different venues, and venues can list many different bands. The user can also view a list of bands, and from there view an individual band with a list of all venues they have played at.

### Installation Instructions

-Download GitHub  
-Clone the GitHub repository at <https://github.com/jamescosborn/band-tracker.git>  
-Download Mono  
-Navigate to project directory  
-In Mono Command Prompt, type `dotnet restore`  
-In Mono Command Prompt, type `dotnet run`  
-In a internet browser (Chrome recommended) go to the URL: `localhost:5000`  
-Try adding a few bands and venues  

### Database Reconstruction

To recreate this project's database, use the following commands in SQL:

CREATE DATABASE james_osborn_test;
USE james_osborn_test;
CREATE TABLE venues (id serial PRIMARY KEY, name VARCHAR(255), band_id INT);
CREATE TABLE bands (id serial PRIMARY KEY, name VARCHAR(255), venue_id INT);
CREATE TABLE bands_venues (id serial PRIMARY KEY, band_id INT, venue_id INT);

### Specs
|Description|Input|Output|
|-|-|-|
|The user can add a venue to the site, displayed in a list.|Add: Echo|Venue Added!|
|The user can view a single venue on the site.|View: Echo|Bands: None|
|The user can view all venues on the site.|View All|Venues: Echo, The Echoplex, pehrspace, Iam8bit, Sunset Lounge, The Troubadour|
|The user can update a venue on the site.|Update Echo>The Echo|Venue Updated!|
|The user can delete a venue from the site.|Delete: Sunset Lounge|Venue Deleted!|
|The user can add a band to a venue.|Add: The Melvins to The Echo|Band Added!|
|When viewing a venue, the user sees all bands that are playing there.|View: The Echo|Bands: The Melvins, The Weirdos, Man or Astro Man|
|When viewing a band, the user sees all venues that bands are playing at.|View: The Melvins|Venus: The Echo, The Troubadour|

### Known Bugs
No known bugs at this time.  

### Contact Me
Email <jamescarlosborn@gmail.com> with any bug reports or feedback.  

#### License
This application uses the MIT license.
